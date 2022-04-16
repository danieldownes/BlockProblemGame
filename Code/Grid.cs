using System;
using System.Collections.Generic;
using System.Linq;

// Attempt at some form of adjacency graph
public class AdjacencyGraph
{
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
	
	public void Init()
	{
		Graph game = new Graph();
		
		game.AddNode(NodeType.Board);
		
		game.AddNodes(NodeType.Block, 16);
		game.AddEdge(NodeType.Board, NodeType.Block);
		
		// A block contains 4 side nodes
		game.AddNode(NodeType.NorthSide);
		game.AddNode(NodeType.SouthSide);
		game.AddNode(NodeType.EastSide);
		game.AddNode(NodeType.WestSide);
		game.AddEdge(NodeType.Block, NodeType.NorthSide);
		game.AddEdge(NodeType.Block, NodeType.SouthSide);
		game.AddEdge(NodeType.Block, NodeType.EastSide);
		game.AddEdge(NodeType.Block, NodeType.WestSide);

		// A block can contain a centre diamond or centre arrow shape 
		game.AddNode(NodeType.BlockCentreDiamond);
		game.AddNode(NodeType.BlockCentreArrow);

		
	}
}

public enum NodeType
{
	Board,

	Block,

	NorthSide,
	SouthSide,
	EastSide,
	WestSide,

	Red,
	Yellow,
	Blue,
	Green,

	BlockCentreDiamond,
	BlockCentreArrow,

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
