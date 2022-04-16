using System;
using System.Collections.Generic;
using System.Linq;

public class Graph
{
	public List<Node> Nodes;
	public List<Edge> Edges;

    public Graph()
	{
		Nodes = new List<Node>();
		Edges = new List<Edge>();
	}

	public void AddEdge(Node left, Node right)
	{
		if( BiDirectionalEdgeExists(left, right) == true)
			return;
		
		Edge edge = new Edge(left, right);
		Edges.Add(edge);
	}

    public bool BiDirectionalEdgeExists(Node left, Node right)
    {
		return Edges.Any( a =>
			a.L == left && a.R == right ||
			a.R == left && a.L == right );
    }

    public void AddEdge(NodeType left, NodeType right)
	{
		List<Node> lefts = Nodes.Where( a => a.Type == left).ToList();
		List<Node> rights = Nodes.Where( a => a.Type == right).ToList();

		for (int l = 0; l < lefts.Count; l++)
		{
			for (int r = 0; r < rights.Count; r++)
			{
				AddEdge(lefts[l], rights[r]);
			}
		}
	}

    internal void AddNode(NodeType type)
    {
        Nodes.Add(new Node(type));
    }

	internal void AddNodes(NodeType type, int count)
    {
		for (int i = 0; i < count; i++)
		{
	        Nodes.Add(new Node(type));
		}
    }


	
}	