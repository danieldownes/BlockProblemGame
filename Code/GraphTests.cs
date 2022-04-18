using NUnit.Framework;

[TestFixture]
class GraphTests
{
    private Graph graph = new Graph();

    [SetUp]
    public void Setup()
    {
        graph.AddNode(NodeType.Block);
        graph.AddNode(NodeType.Block);
    }

    [Test]
    public void HasNodes()
    {
        Assert.NotZero(graph.Nodes.Count);
    }

    [Test]
    public void AddEdge_ByNodeType()
    {
        graph.AddEdge(NodeType.Block, NodeType.Block);
        Assert.AreEqual(1, graph.Edges.Count);
    }
}