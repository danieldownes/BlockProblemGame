using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// A (blockIndex, rotation) pair representing a block in a specific orientation.
/// </summary>
public struct BlockPlacement
{
	public int BlockIndex;
	public int Rotation; // 0..3 clockwise turns

	public BlockPlacement(int blockIndex, int rotation)
	{
		BlockIndex = blockIndex;
		Rotation = rotation;
	}

	/// <summary>
	/// Returns a new Block with the rotation applied.
	/// </summary>
	public Block GetOriented(List<Block> blocks)
	{
		var src = blocks[BlockIndex];
		var b = new Block(src.T, src.R, src.B, src.L, src.M);
		for (int i = 0; i < Rotation; i++)
			b.TurnClockwise();
		return b;
	}
}

/// <summary>
/// Graph-based puzzle solver using a compatibility graph and constraint propagation.
///
/// The solver builds a compatibility graph where nodes are (block, rotation) pairs
/// and edges connect pairs that can be placed adjacent. It then uses backtracking
/// with constraint propagation (forward checking + domain filtering) to find a
/// valid placement.
/// </summary>
public class Solver
{
	private List<Block> blocks = new List<Block>();
	private Block[] orientedCache = Array.Empty<Block>();
	private int totalBlocks;

	// Compatibility graph adjacency lists.
	// Indexed by encoded placement (blockIndex*4 + rotation).
	// Each entry contains the set of encoded placements compatible as a neighbor.
	private HashSet<int>[] horizontalCompatible = Array.Empty<HashSet<int>>(); // A.R == B.L
	private HashSet<int>[] verticalCompatible = Array.Empty<HashSet<int>>();   // A.B == B.T

	private void BuildCompatibilityGraph(List<Block> sourceBlocks)
	{
		blocks = sourceBlocks;
		totalBlocks = blocks.Count;
		int totalPlacements = totalBlocks * 4;

		// Pre-compute all oriented blocks
		orientedCache = new Block[totalPlacements];
		for (int bi = 0; bi < totalBlocks; bi++)
		{
			for (int rot = 0; rot < 4; rot++)
			{
				var bp = new BlockPlacement(bi, rot);
				orientedCache[bi * 4 + rot] = bp.GetOriented(blocks);
			}
		}

		// Build compatibility adjacency lists
		horizontalCompatible = new HashSet<int>[totalPlacements];
		verticalCompatible = new HashSet<int>[totalPlacements];

		for (int i = 0; i < totalPlacements; i++)
		{
			horizontalCompatible[i] = new HashSet<int>();
			verticalCompatible[i] = new HashSet<int>();
		}

		for (int a = 0; a < totalPlacements; a++)
		{
			var blockA = orientedCache[a];
			for (int b = 0; b < totalPlacements; b++)
			{
				var blockB = orientedCache[b];

				if (blockA.R == blockB.L)
					horizontalCompatible[a].Add(b);

				if (blockA.B == blockB.T)
					verticalCompatible[a].Add(b);
			}
		}
	}

	private int DecodeBlockIndex(int encoded) => encoded / 4;

	/// <summary>
	/// Solve Level 1: adjacent blocks must have matching edge colours.
	/// </summary>
	public Block[,]? SolveLevel1(List<Block> sourceBlocks)
	{
		return Solve(sourceBlocks, level2: false);
	}

	/// <summary>
	/// Solve Level 2: Level 1 + no two adjacent blocks can both have diamonds.
	/// </summary>
	public Block[,]? SolveLevel2(List<Block> sourceBlocks)
	{
		return Solve(sourceBlocks, level2: true);
	}

	private Block[,]? Solve(List<Block> sourceBlocks, bool level2)
	{
		BuildCompatibilityGraph(sourceBlocks);

		if (level2)
		{
			// The diamond constraint forces a checkerboard pattern.
			// Try both parities: diamonds on even or odd (r+c)%2 positions.
			for (int parity = 0; parity <= 1; parity++)
			{
				var result = SolveWithDomains(level2, parity);
				if (result != null)
					return result;
			}
			return null;
		}

		return SolveWithDomains(level2, -1);
	}

	private Block[,]? SolveWithDomains(bool level2, int diamondParity)
	{
		int totalPlacements = totalBlocks * 4;

		int[,] grid = new int[4, 4];
		for (int r = 0; r < 4; r++)
			for (int c = 0; c < 4; c++)
				grid[r, c] = -1;

		bool[] usedBlock = new bool[totalBlocks];

		// Build initial domains, pre-filtered by diamond parity for Level 2
		var domains = new List<int>[4, 4];
		for (int r = 0; r < 4; r++)
		{
			for (int c = 0; c < 4; c++)
			{
				domains[r, c] = new List<int>();
				bool needsDiamond = diamondParity >= 0 && (r + c) % 2 == diamondParity;
				bool needsNonDiamond = diamondParity >= 0 && (r + c) % 2 != diamondParity;

				for (int enc = 0; enc < totalPlacements; enc++)
				{
					bool hasDiamond = orientedCache[enc].M;
					if (needsDiamond && !hasDiamond) continue;
					if (needsNonDiamond && hasDiamond) continue;
					domains[r, c].Add(enc);
				}
			}
		}

		// Deduplicate: map each unique oriented signature to a canonical block index.
		// This avoids trying identical blocks in the same position.
		var canonicalBlock = BuildCanonicalBlockMap();

		if (Backtrack(grid, usedBlock, domains, 0, 0, level2, canonicalBlock))
		{
			var result = new Block[4, 4];
			for (int r = 0; r < 4; r++)
				for (int c = 0; c < 4; c++)
					result[r, c] = orientedCache[grid[r, c]];
			return result;
		}

		return null;
	}

	/// <summary>
	/// For each block, find the lowest-index block with identical (T,R,B,L,M) values.
	/// When searching, if we've already tried a block with the same signature at this
	/// position, we skip duplicates.
	/// </summary>
	private int[] BuildCanonicalBlockMap()
	{
		var canonical = new int[totalBlocks];
		for (int i = 0; i < totalBlocks; i++)
		{
			canonical[i] = i;
			for (int j = 0; j < i; j++)
			{
				if (blocks[j].T == blocks[i].T && blocks[j].R == blocks[i].R &&
				    blocks[j].B == blocks[i].B && blocks[j].L == blocks[i].L &&
				    blocks[j].M == blocks[i].M)
				{
					canonical[i] = j;
					break;
				}
			}
		}
		return canonical;
	}

	private bool Backtrack(int[,] grid, bool[] usedBlock, List<int>[,] domains,
		int row, int col, bool level2, int[] canonicalBlock)
	{
		if (row == 4)
			return true;

		int nextRow = col == 3 ? row + 1 : row;
		int nextCol = col == 3 ? 0 : col + 1;

		var candidates = GetCandidates(grid, usedBlock, domains[row, col], row, col, level2);

		// Track which canonical (block, rotation-signature) combos we've tried at this position
		var triedSignatures = new HashSet<long>();

		foreach (int encoded in candidates)
		{
			int bi = DecodeBlockIndex(encoded);
			int rot = encoded % 4;

			// Build a signature from the canonical block index + the oriented block values
			// Two identical blocks at the same rotation produce the same result
			int cbi = canonicalBlock[bi];
			long sig = ((long)cbi << 32) | (uint)rot;
			if (!triedSignatures.Add(sig))
				continue;

			grid[row, col] = encoded;
			usedBlock[bi] = true;

			// Forward check: verify unassigned neighbors still have candidates
			if (ForwardCheck(grid, usedBlock, domains, row, col, level2))
			{
				if (Backtrack(grid, usedBlock, domains, nextRow, nextCol, level2, canonicalBlock))
					return true;
			}

			grid[row, col] = -1;
			usedBlock[bi] = false;
		}

		return false;
	}

	/// <summary>
	/// Forward checking: after placing grid[row,col], verify that the right and
	/// bottom neighbors (if unassigned) still have at least one valid candidate.
	/// </summary>
	private bool ForwardCheck(int[,] grid, bool[] usedBlock, List<int>[,] domains,
		int row, int col, bool level2)
	{
		// Check right neighbor
		if (col + 1 < 4 && grid[row, col + 1] == -1)
		{
			if (!HasCandidate(grid, usedBlock, domains[row, col + 1], row, col + 1, level2))
				return false;
		}

		// Check bottom neighbor
		if (row + 1 < 4 && grid[row + 1, col] == -1)
		{
			if (!HasCandidate(grid, usedBlock, domains[row + 1, col], row + 1, col, level2))
				return false;
		}

		return true;
	}

	private bool HasCandidate(int[,] grid, bool[] usedBlock, List<int> domain,
		int row, int col, bool level2)
	{
		int leftEncoded = col > 0 ? grid[row, col - 1] : -1;
		int topEncoded = row > 0 ? grid[row - 1, col] : -1;

		foreach (int encoded in domain)
		{
			int bi = DecodeBlockIndex(encoded);
			if (usedBlock[bi]) continue;

			if (leftEncoded >= 0 && !horizontalCompatible[leftEncoded].Contains(encoded))
				continue;
			if (topEncoded >= 0 && !verticalCompatible[topEncoded].Contains(encoded))
				continue;

			if (level2 && orientedCache[encoded].M)
			{
				if (leftEncoded >= 0 && orientedCache[leftEncoded].M) continue;
				if (topEncoded >= 0 && orientedCache[topEncoded].M) continue;
			}

			return true; // At least one candidate exists
		}
		return false;
	}

	private List<int> GetCandidates(int[,] grid, bool[] usedBlock,
		List<int> domain, int row, int col, bool level2)
	{
		var result = new List<int>();

		int leftEncoded = col > 0 ? grid[row, col - 1] : -1;
		int topEncoded = row > 0 ? grid[row - 1, col] : -1;

		foreach (int encoded in domain)
		{
			int bi = DecodeBlockIndex(encoded);
			if (usedBlock[bi]) continue;

			if (leftEncoded >= 0 && !horizontalCompatible[leftEncoded].Contains(encoded))
				continue;
			if (topEncoded >= 0 && !verticalCompatible[topEncoded].Contains(encoded))
				continue;

			if (level2 && orientedCache[encoded].M)
			{
				if (leftEncoded >= 0 && orientedCache[leftEncoded].M) continue;
				if (topEncoded >= 0 && orientedCache[topEncoded].M) continue;
			}

			result.Add(encoded);
		}

		return result;
	}

	// ── Validation ─────────────────────────────────────────

	/// <summary>
	/// Validates a Level 1 solution: all adjacent edges match.
	/// </summary>
	public static bool IsValidLevel1(Block[,] grid)
	{
		int rows = grid.GetLength(0);
		int cols = grid.GetLength(1);

		for (int r = 0; r < rows; r++)
		{
			for (int c = 0; c < cols; c++)
			{
				if (c + 1 < cols && grid[r, c].R != grid[r, c + 1].L)
					return false;
				if (r + 1 < rows && grid[r, c].B != grid[r + 1, c].T)
					return false;
			}
		}

		return true;
	}

	/// <summary>
	/// Validates a Level 2 solution: Level 1 + no adjacent diamonds.
	/// </summary>
	public static bool IsValidLevel2(Block[,] grid)
	{
		if (!IsValidLevel1(grid))
			return false;

		int rows = grid.GetLength(0);
		int cols = grid.GetLength(1);

		for (int r = 0; r < rows; r++)
		{
			for (int c = 0; c < cols; c++)
			{
				if (!grid[r, c].M) continue;

				if (c + 1 < cols && grid[r, c + 1].M)
					return false;
				if (r + 1 < rows && grid[r + 1, c].M)
					return false;
			}
		}

		return true;
	}
}
