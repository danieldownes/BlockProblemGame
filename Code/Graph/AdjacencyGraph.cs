using System;
using System.Collections.Generic;
using System.Linq;

// Attempt at some form of adjacency graph
public class AdjacencyGraph
{
	public Graph Data = new Graph();

    private Node board;

    // The board node is the graph root
    // A block contains 4 edge nodes types, types: coloured indexes (0 ... 3)
    // 1 centre node type, index (0, 1)
    // 4 adjacency nodes, top, bottom, left, right

    // Constraints:	
    // A board node should only contain 16 blocks 
    // a block centre node can not match the adjacent block, centre node type
    // adjacency nodes, can be a block reference, or empty
    // there can only be 16 empty adjacency nodes
    // all others should reference a block note 
	// A block can only have 0, 1 or 2 empty adjacent nodes

	// Permissible Transforms:
	// A block can be rotated, so adjacent nodes, and colours rotate together
	// A block can be swapped with any other block

	// Permutataion generating
	// ..using each constraint individually, generate a set of permutations 
	
	// Permutataion elliminination, known small groupings, 
	///  are outcast? eg, a set of 4 blocks can not possibly fit

	// Matching (Marching the graph)
	// Using weghted
	

    public void Init()
	{
		board = Data.AddNode(NodeType.Board);
	}

	public void AddBlock(NodeType centre)
	{
		var b = Data.AddNode(NodeType.Block);
		Data.AddEdge(board, b);
		
		// A block contains 4 "side colour"; each side with 1 colour
		var ns = Data.AddNode(NodeType.NorthColour);
		var es = Data.AddNode(NodeType.EastColour);
		var ss = Data.AddNode(NodeType.SouthColour);
		var ws = Data.AddNode(NodeType.WestColour);
		Data.AddEdge(b, ns);
		Data.AddEdge(b, ss);
		Data.AddEdge(b, es);
		Data.AddEdge(b, ws);

		// A block can contain a centre diamond or centre arrow shape 
		var c = Data.AddNode(centre);
		//Data.AddNode(NodeType.BlockCentreArrow);
		Data.AddEdge(b, c);
	}

	public void SwapBlockPosition()
	{
		// 2 blocks can be swapped with each other
		// Their adjacent blocks nodes are swapped

		// select 2 different block nodes
		// a, b
		
		// swap a.AdjacentBlocks with b.AdjacentBlocks 
	}

	public void RotateSideColours()
	{
		// 2 blocks can be swapped with each other
		// Their adjacent blocks nodes are swapped

		// for this block
		// b
		
		// for each .child in .Sides
		// 	  shift the .child.Colour to the next sibling .child 

	}
}

public enum NodeType
{
	Board,

	Block,

	Sides,
		NorthColour,
			//Colour,
		SouthColour,
			//Colour,
		EastColour,
			//Colour,
		WestColour,
			//Colour,

	Colour,
		Red,
		Yellow,
		Blue,
		Green,

	BlockCentreDiamond,
	BlockCentreArrow,

	AdjacentBlocks,
		UpAdjacentBlock,
		BottomAdjacentBlock,
		LeftAdjacentBlock,
		RightAdjacentBlock
}


public class Node
{
	public string Name;
	public NodeType Type;
	
	public Node(NodeType type)
	{
		Name = Enum.GetName(typeof(NodeType), type);
		Type = type;
	}
}	

public class Constraint
{
	
}	


public class Edge
{
	public Node L;
	public Node R;

	public Edge(Node l, Node r)
	{
		L = l;
		R = r;
	}
}
