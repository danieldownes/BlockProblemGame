using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;

// ── Block ──────────────────────────────────────────────

[TestFixture]
class BlockTests
{
    [Test]
    public void Constructor_AssignsProperties()
    {
        var b = new Block(Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow, true);
        Assert.AreEqual(Colour.Red, b.T);
        Assert.AreEqual(Colour.Green, b.R);
        Assert.AreEqual(Colour.Blue, b.B);
        Assert.AreEqual(Colour.Yellow, b.L);
        Assert.IsTrue(b.M);
    }

    [Test]
    public void DirectionIndexer_AccessesEdges()
    {
        var b = new Block(Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow, false);
        Assert.AreEqual(Colour.Red, b[Direction.Top]);
        Assert.AreEqual(Colour.Green, b[Direction.Right]);
        Assert.AreEqual(Colour.Blue, b[Direction.Bottom]);
        Assert.AreEqual(Colour.Yellow, b[Direction.Left]);
    }

    [Test]
    public void TurnClockwise_RotatesEdges()
    {
        var b = new Block(Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow, false);
        b.TurnClockwise();
        // L→T, T→R, R→B, B→L
        Assert.AreEqual(Colour.Yellow, b.T);
        Assert.AreEqual(Colour.Red, b.R);
        Assert.AreEqual(Colour.Green, b.B);
        Assert.AreEqual(Colour.Blue, b.L);
    }

    [Test]
    public void TurnAntiClockwise_RotatesEdges()
    {
        var b = new Block(Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow, false);
        b.TurnAntiClockwise();
        // R→T, B→R, L→B, T→L
        Assert.AreEqual(Colour.Green, b.T);
        Assert.AreEqual(Colour.Blue, b.R);
        Assert.AreEqual(Colour.Yellow, b.B);
        Assert.AreEqual(Colour.Red, b.L);
    }

    [Test]
    public void ClockwiseThenAntiClockwise_RestoresOriginal()
    {
        var b = new Block(Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow, true);
        b.TurnClockwise();
        b.TurnAntiClockwise();
        Assert.AreEqual(Colour.Red, b.T);
        Assert.AreEqual(Colour.Green, b.R);
        Assert.AreEqual(Colour.Blue, b.B);
        Assert.AreEqual(Colour.Yellow, b.L);
    }

    [Test]
    public void FourClockwiseRotations_RestoresOriginal()
    {
        var b = new Block(Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow, false);
        b.TurnClockwise();
        b.TurnClockwise();
        b.TurnClockwise();
        b.TurnClockwise();
        Assert.AreEqual(Colour.Red, b.T);
        Assert.AreEqual(Colour.Green, b.R);
        Assert.AreEqual(Colour.Blue, b.B);
        Assert.AreEqual(Colour.Yellow, b.L);
    }

    [Test]
    public void Rotation_DoesNotAffectMiddle()
    {
        var b = new Block(Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow, true);
        b.TurnClockwise();
        Assert.IsTrue(b.M);
        b.TurnAntiClockwise();
        Assert.IsTrue(b.M);
    }
}

// ── BlockTypes ─────────────────────────────────────────

[TestFixture]
class BlockTypesTests
{
    [Test]
    public void Data_Has7Types()
    {
        Assert.AreEqual(7, BlockTypes.Data.Length);
    }

    [Test]
    public void CountColumn_SumsTo16()
    {
        int sum = BlockTypes.Data.Sum(t => t.Count);
        Assert.AreEqual(16, sum);
    }

    [Test]
    public void DiamondTypes_HaveDiamondFlag()
    {
        // First 4 types are diamond types
        for (int i = 0; i < 4; i++)
            Assert.IsTrue(BlockTypes.Data[i].HasDiamond, $"Type {i} should have diamond");
    }

    [Test]
    public void NonDiamondTypes_HaveNoDiamondFlag()
    {
        // Last 3 types are non-diamond types
        for (int i = 4; i < 7; i++)
            Assert.IsFalse(BlockTypes.Data[i].HasDiamond, $"Type {i} should not have diamond");
    }
}

// ── Graph ──────────────────────────────────────────────

[TestFixture]
class GraphTests
{
    private Graph graph;

    [SetUp]
    public void Setup()
    {
        graph = new Graph();
    }

    [Test]
    public void AddNode_ReturnsNodeWithCorrectType()
    {
        var node = graph.AddNode(NodeType.Board);
        Assert.AreEqual(NodeType.Board, node.Type);
        Assert.AreEqual("Board", node.Name);
    }

    [Test]
    public void AddNode_AddsToNodesList()
    {
        graph.AddNode(NodeType.Block);
        Assert.AreEqual(1, graph.Nodes.Count);
    }

    [Test]
    public void AddNodes_CreatesCorrectCount()
    {
        graph.AddNodes(NodeType.Block, 5);
        Assert.AreEqual(5, graph.Nodes.Count);
    }

    [Test]
    public void AddEdge_ByNodeReference_CreatesEdge()
    {
        var a = graph.AddNode(NodeType.Board);
        var b = graph.AddNode(NodeType.Block);
        graph.AddEdge(a, b);
        Assert.AreEqual(1, graph.Edges.Count);
    }

    [Test]
    public void AddEdge_SelfEdge_IsIgnored()
    {
        var a = graph.AddNode(NodeType.Block);
        graph.AddEdge(a, a);
        Assert.AreEqual(0, graph.Edges.Count);
    }

    [Test]
    public void AddEdge_Duplicate_IsIgnored()
    {
        var a = graph.AddNode(NodeType.Board);
        var b = graph.AddNode(NodeType.Block);
        graph.AddEdge(a, b);
        graph.AddEdge(a, b);
        Assert.AreEqual(1, graph.Edges.Count);
    }

    [Test]
    public void AddEdge_ReverseDuplicate_IsIgnored()
    {
        var a = graph.AddNode(NodeType.Board);
        var b = graph.AddNode(NodeType.Block);
        graph.AddEdge(a, b);
        graph.AddEdge(b, a);
        Assert.AreEqual(1, graph.Edges.Count);
    }

    [Test]
    public void BiDirectionalEdgeExists_ReturnsTrueForward()
    {
        var a = graph.AddNode(NodeType.Board);
        var b = graph.AddNode(NodeType.Block);
        graph.AddEdge(a, b);
        Assert.IsTrue(graph.BiDirectionalEdgeExists(a, b));
    }

    [Test]
    public void BiDirectionalEdgeExists_ReturnsTrueReverse()
    {
        var a = graph.AddNode(NodeType.Board);
        var b = graph.AddNode(NodeType.Block);
        graph.AddEdge(a, b);
        Assert.IsTrue(graph.BiDirectionalEdgeExists(b, a));
    }

    [Test]
    public void BiDirectionalEdgeExists_ReturnsFalseWhenNoEdge()
    {
        var a = graph.AddNode(NodeType.Board);
        var b = graph.AddNode(NodeType.Block);
        Assert.IsFalse(graph.BiDirectionalEdgeExists(a, b));
    }

    [Test]
    public void AddEdge_ByNodeType_CreatesCartesianEdges()
    {
        graph.AddNode(NodeType.Block);
        graph.AddNode(NodeType.Block);
        graph.AddEdge(NodeType.Block, NodeType.Block);
        // 2 Block nodes: only 1 unique edge (A→B), self-edges excluded
        Assert.AreEqual(1, graph.Edges.Count);
    }

    [Test]
    public void AddEdge_ByNodeType_CrossTypes()
    {
        graph.AddNode(NodeType.Board);
        graph.AddNode(NodeType.Block);
        graph.AddNode(NodeType.Block);
        graph.AddEdge(NodeType.Board, NodeType.Block);
        // 1 Board × 2 Block = 2 edges
        Assert.AreEqual(2, graph.Edges.Count);
    }
}

// ── AdjacencyGraph ─────────────────────────────────────

[TestFixture]
class AdjacencyGraphTests
{
    [Test]
    public void Init_CreatesBoardNode()
    {
        var ag = new AdjacencyGraph();
        ag.Init();
        Assert.AreEqual(1, ag.Data.Nodes.Count);
        Assert.AreEqual(NodeType.Board, ag.Data.Nodes[0].Type);
    }

    [Test]
    public void AddBlock_Creates6Nodes()
    {
        var ag = new AdjacencyGraph();
        ag.Init();
        ag.AddBlock(NodeType.BlockCentreDiamond);
        // 1 board + 1 block + 4 colours + 1 centre = 7
        Assert.AreEqual(7, ag.Data.Nodes.Count);
    }

    [Test]
    public void AddBlock_Creates6Edges()
    {
        var ag = new AdjacencyGraph();
        ag.Init();
        ag.AddBlock(NodeType.BlockCentreDiamond);
        // board→block + block→N + block→S + block→E + block→W + block→centre = 6
        Assert.AreEqual(6, ag.Data.Edges.Count);
    }

    [Test]
    public void AddBlock_CentreTypeMatchesArgument()
    {
        var ag = new AdjacencyGraph();
        ag.Init();
        ag.AddBlock(NodeType.BlockCentreArrow);
        var centreNode = ag.Data.Nodes.Find(n => n.Type == NodeType.BlockCentreArrow);
        Assert.IsNotNull(centreNode);
    }

    [Test]
    public void AddMultipleBlocks_AccumulatesCorrectly()
    {
        var ag = new AdjacencyGraph();
        ag.Init();
        ag.AddBlock(NodeType.BlockCentreDiamond);
        ag.AddBlock(NodeType.BlockCentreArrow);
        // 1 board + 2×(1 block + 4 colours + 1 centre) = 13 nodes
        Assert.AreEqual(13, ag.Data.Nodes.Count);
        // 2×(board→block + block→4colours + block→centre) = 12 edges
        Assert.AreEqual(12, ag.Data.Edges.Count);
    }
}

// ── BlockGame ──────────────────────────────────────────

[TestFixture]
class BlockGameTests
{
    [Test]
    public void Init_PopulatesBlocksList()
    {
        var game = new BlockGame();
        game.Init();
        Assert.IsNotNull(game.blocks);
        Assert.IsNotEmpty(game.blocks);
    }

    [Test]
    public void Init_Creates16Blocks()
    {
        // Count column expands types: 3+3+1+1+1+2+5 = 16 blocks for 4x4 grid
        var game = new BlockGame();
        game.Init();
        Assert.AreEqual(16, game.blocks.Count);
    }

    [Test]
    public void Init_FirstBlock_HasCorrectData()
    {
        var game = new BlockGame();
        game.Init();
        var b = game.blocks[0];
        // Row 0: Count=3, T=Green, B=Yellow, L=Blue, R=Red, HasDiamond=1
        Assert.AreEqual(Colour.Green, b.T);
        Assert.AreEqual(Colour.Red, b.R);
        Assert.AreEqual(Colour.Yellow, b.B);
        Assert.AreEqual(Colour.Blue, b.L);
        Assert.IsTrue(b.M);
    }

    [Test]
    public void Init_LastBlock_HasCorrectData()
    {
        var game = new BlockGame();
        game.Init();
        var b = game.blocks[15];
        // Last block is from row 6 (type count=5): T=Blue, B=Green, L=Yellow, R=Red, HasDiamond=0
        Assert.AreEqual(Colour.Blue, b.T);
        Assert.AreEqual(Colour.Red, b.R);
        Assert.AreEqual(Colour.Green, b.B);
        Assert.AreEqual(Colour.Yellow, b.L);
        Assert.IsFalse(b.M);
    }

    [Test]
    public void Init_Has8DiamondAnd8NonDiamond()
    {
        var game = new BlockGame();
        game.Init();
        int diamonds = game.blocks.Count(b => b.M);
        Assert.AreEqual(8, diamonds);
        Assert.AreEqual(8, game.blocks.Count - diamonds);
    }
}

// ── Solver ─────────────────────────────────────────────

[TestFixture]
class SolverTests
{
    private static List<Block> GetBlocks()
    {
        var game = new BlockGame();
        game.Init();
        return game.blocks;
    }

    // ── Level 1 ────────────────────────────────────────

    [Test]
    public void SolveLevel1_ReturnsSolution()
    {
        var solver = new Solver();
        var result = solver.SolveLevel1(GetBlocks());
        Assert.IsNotNull(result);
    }

    [Test]
    public void SolveLevel1_GridIs4x4()
    {
        var solver = new Solver();
        var result = solver.SolveLevel1(GetBlocks())!;
        Assert.AreEqual(4, result.GetLength(0));
        Assert.AreEqual(4, result.GetLength(1));
    }

    [Test]
    public void SolveLevel1_AllAdjacentEdgesMatch()
    {
        var solver = new Solver();
        var result = solver.SolveLevel1(GetBlocks())!;
        Assert.IsTrue(Solver.IsValidLevel1(result));
    }

    [Test]
    public void SolveLevel1_UsesAll16Positions()
    {
        var solver = new Solver();
        var result = solver.SolveLevel1(GetBlocks())!;
        for (int r = 0; r < 4; r++)
            for (int c = 0; c < 4; c++)
                Assert.IsNotNull(result[r, c], $"Position [{r},{c}] is null");
    }

    // ── Level 2 ────────────────────────────────────────

    [Test]
    public void SolveLevel2_ReturnsSolution()
    {
        var solver = new Solver();
        var result = solver.SolveLevel2(GetBlocks());
        Assert.IsNotNull(result);
    }

    [Test]
    public void SolveLevel2_AllAdjacentEdgesMatch()
    {
        var solver = new Solver();
        var result = solver.SolveLevel2(GetBlocks())!;
        Assert.IsTrue(Solver.IsValidLevel1(result));
    }

    [Test]
    public void SolveLevel2_NoDiamondsAdjacent()
    {
        var solver = new Solver();
        var result = solver.SolveLevel2(GetBlocks())!;
        Assert.IsTrue(Solver.IsValidLevel2(result));
    }

    [Test]
    public void SolveLevel2_UsesAll16Positions()
    {
        var solver = new Solver();
        var result = solver.SolveLevel2(GetBlocks())!;
        for (int r = 0; r < 4; r++)
            for (int c = 0; c < 4; c++)
                Assert.IsNotNull(result[r, c], $"Position [{r},{c}] is null");
    }

    // ── Validation ─────────────────────────────────────

    [Test]
    public void IsValidLevel1_RejectsNonMatchingEdges()
    {
        var grid = new Block[4, 4];
        for (int r = 0; r < 4; r++)
            for (int c = 0; c < 4; c++)
                grid[r, c] = new Block(Colour.Red, Colour.Red, Colour.Red, Colour.Red, false);

        // All Red edges match, so this should be valid
        Assert.IsTrue(Solver.IsValidLevel1(grid));

        // Break one horizontal edge: L=Green != R=Red of [0,0]
        grid[0, 1] = new Block(Colour.Red, Colour.Red, Colour.Red, Colour.Green, false);
        Assert.IsFalse(Solver.IsValidLevel1(grid));
    }

    [Test]
    public void IsValidLevel2_RejectsAdjacentDiamonds()
    {
        var grid = new Block[4, 4];
        for (int r = 0; r < 4; r++)
            for (int c = 0; c < 4; c++)
                grid[r, c] = new Block(Colour.Red, Colour.Red, Colour.Red, Colour.Red, false);

        Assert.IsTrue(Solver.IsValidLevel2(grid));

        // Place two adjacent diamonds
        grid[0, 0] = new Block(Colour.Red, Colour.Red, Colour.Red, Colour.Red, true);
        grid[0, 1] = new Block(Colour.Red, Colour.Red, Colour.Red, Colour.Red, true);
        Assert.IsFalse(Solver.IsValidLevel2(grid));
    }

    [Test]
    public void GenerateSolutionsHtml()
    {
        var solver = new Solver();
        var level1 = solver.SolveLevel1(GetBlocks())!;
        var level2 = solver.SolveLevel2(GetBlocks())!;

        Assert.IsNotNull(level1);
        Assert.IsNotNull(level2);

        var sb = new StringBuilder();
        sb.AppendLine("<!DOCTYPE html>");
        sb.AppendLine("<html lang=\"en\">");
        sb.AppendLine("<head>");
        sb.AppendLine("<meta charset=\"UTF-8\">");
        sb.AppendLine("<title>Block Puzzle - Solutions</title>");
        sb.AppendLine("<style>");
        sb.AppendLine("  body { font-family: sans-serif; background: #1a1a2e; color: #eee; margin: 2rem; }");
        sb.AppendLine("  h1 { text-align: center; margin-bottom: 0.2rem; }");
        sb.AppendLine("  h2 { text-align: center; color: #ccc; margin: 2.5rem 0 0.5rem; }");
        sb.AppendLine("  .subtitle { text-align: center; color: #888; margin-bottom: 2rem; }");
        sb.AppendLine("  .grid { display: grid; grid-template-columns: repeat(4, 130px); gap: 4px; justify-content: center; margin-bottom: 2rem; }");
        sb.AppendLine("  .legend { display: flex; gap: 20px; justify-content: center; margin: 1rem 0 2rem; }");
        sb.AppendLine("  .legend-item { display: flex; align-items: center; gap: 6px; font-size: 13px; }");
        sb.AppendLine("  .legend-swatch { width: 16px; height: 16px; border-radius: 3px; border: 1px solid #555; }");
        sb.AppendLine("  .status { text-align: center; font-size: 14px; color: #2ecc71; margin-bottom: 1rem; }");
        sb.AppendLine("  svg { filter: drop-shadow(0 2px 4px rgba(0,0,0,0.5)); }");
        sb.AppendLine("</style>");
        sb.AppendLine("</head>");
        sb.AppendLine("<body>");
        sb.AppendLine("<h1>Block Puzzle - Solutions</h1>");
        sb.AppendLine("<p class=\"subtitle\">4&times;4 grid &mdash; adjacent edges must match colours</p>");
        sb.AppendLine("<div class=\"legend\">");
        sb.AppendLine("  <div class=\"legend-item\"><div class=\"legend-swatch\" style=\"background:#e74c3c\"></div> Red</div>");
        sb.AppendLine("  <div class=\"legend-item\"><div class=\"legend-swatch\" style=\"background:#2ecc71\"></div> Green</div>");
        sb.AppendLine("  <div class=\"legend-item\"><div class=\"legend-swatch\" style=\"background:#3498db\"></div> Blue</div>");
        sb.AppendLine("  <div class=\"legend-item\"><div class=\"legend-swatch\" style=\"background:#f1c40f\"></div> Yellow</div>");
        sb.AppendLine("  <div class=\"legend-item\"><svg width=\"16\" height=\"16\"><polygon points=\"8,1 15,8 8,15 1,8\" fill=\"#fff\" opacity=\"0.7\"/></svg> Diamond</div>");
        sb.AppendLine("</div>");

        sb.AppendLine("<h2>Level 1 &mdash; Matching Edges</h2>");
        sb.AppendLine("<p class=\"status\">Solution found</p>");
        AppendGrid(sb, level1);

        sb.AppendLine("<h2>Level 2 &mdash; Matching Edges + No Adjacent Diamonds</h2>");
        sb.AppendLine("<p class=\"status\">Solution found</p>");
        AppendGrid(sb, level2);

        sb.AppendLine("</body>");
        sb.AppendLine("</html>");

        string outPath = Path.GetFullPath(Path.Combine(TestContext.CurrentContext.TestDirectory, "..", "..", "..", "..", "doc", "solutions.html"));
        File.WriteAllText(outPath, sb.ToString());
        TestContext.WriteLine($"Solutions written to {outPath}");
    }

    private static readonly string[] SvgColors = { "#e74c3c", "#2ecc71", "#3498db", "#f1c40f" };
    private static readonly string[] ColorLetters = { "R", "G", "B", "Y" };

    private static void AppendGrid(StringBuilder sb, Block[,] grid)
    {
        sb.AppendLine("<div class=\"grid\">");
        for (int r = 0; r < 4; r++)
            for (int c = 0; c < 4; c++)
                AppendBlockSVG(sb, grid[r, c]);
        sb.AppendLine("</div>");
    }

    private static void AppendBlockSVG(StringBuilder sb, Block b)
    {
        int S = 120, M = 10, cx = 60, cy = 60;
        string cT = SvgColors[(int)b.T], cR = SvgColors[(int)b.R];
        string cB = SvgColors[(int)b.B], cL = SvgColors[(int)b.L];

        sb.AppendLine($"<svg width=\"{S}\" height=\"{S}\" viewBox=\"0 0 {S} {S}\">");
        sb.AppendLine($"<rect x=\"{M}\" y=\"{M}\" width=\"{S-2*M}\" height=\"{S-2*M}\" fill=\"#2c2c44\" rx=\"4\"/>");
        sb.AppendLine($"<polygon points=\"{M},{M} {S-M},{M} {cx},{cy}\" fill=\"{cT}\" opacity=\"0.85\"/>");
        sb.AppendLine($"<polygon points=\"{S-M},{M} {S-M},{S-M} {cx},{cy}\" fill=\"{cR}\" opacity=\"0.85\"/>");
        sb.AppendLine($"<polygon points=\"{S-M},{S-M} {M},{S-M} {cx},{cy}\" fill=\"{cB}\" opacity=\"0.85\"/>");
        sb.AppendLine($"<polygon points=\"{M},{S-M} {M},{M} {cx},{cy}\" fill=\"{cL}\" opacity=\"0.85\"/>");
        sb.AppendLine($"<line x1=\"{cx}\" y1=\"{cy}\" x2=\"{M}\" y2=\"{M}\" stroke=\"#1a1a2e\" stroke-width=\"1.5\"/>");
        sb.AppendLine($"<line x1=\"{cx}\" y1=\"{cy}\" x2=\"{S-M}\" y2=\"{M}\" stroke=\"#1a1a2e\" stroke-width=\"1.5\"/>");
        sb.AppendLine($"<line x1=\"{cx}\" y1=\"{cy}\" x2=\"{S-M}\" y2=\"{S-M}\" stroke=\"#1a1a2e\" stroke-width=\"1.5\"/>");
        sb.AppendLine($"<line x1=\"{cx}\" y1=\"{cy}\" x2=\"{M}\" y2=\"{S-M}\" stroke=\"#1a1a2e\" stroke-width=\"1.5\"/>");
        sb.AppendLine($"<rect x=\"{M}\" y=\"{M}\" width=\"{S-2*M}\" height=\"{S-2*M}\" fill=\"none\" stroke=\"#555\" stroke-width=\"1.5\" rx=\"4\"/>");
        if (b.M)
            sb.AppendLine($"<polygon points=\"{cx},{cy-14} {cx+14},{cy} {cx},{cy+14} {cx-14},{cy}\" fill=\"white\" opacity=\"0.8\" stroke=\"#aaa\" stroke-width=\"0.5\"/>");
        string ls = "font-size=\"10\" fill=\"#fff\" text-anchor=\"middle\" font-family=\"sans-serif\" font-weight=\"bold\"";
        sb.AppendLine($"<text x=\"{cx}\" y=\"{M+14}\" {ls}>{ColorLetters[(int)b.T]}</text>");
        sb.AppendLine($"<text x=\"{S-M-8}\" y=\"{cy+4}\" {ls}>{ColorLetters[(int)b.R]}</text>");
        sb.AppendLine($"<text x=\"{cx}\" y=\"{S-M-6}\" {ls}>{ColorLetters[(int)b.B]}</text>");
        sb.AppendLine($"<text x=\"{M+8}\" y=\"{cy+4}\" {ls}>{ColorLetters[(int)b.L]}</text>");
        sb.AppendLine("</svg>");
    }
}
