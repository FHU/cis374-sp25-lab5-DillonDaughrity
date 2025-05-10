using Lab5;

namespace UnitTests;

[TestClass]
public class UnitTests
{
    [TestMethod]
    public void Graph1IsReachable()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/graph1.txt");

        Assert.IsTrue(undirectedGraph.IsReachable("a", "c"));
        Assert.IsTrue(undirectedGraph.IsReachable("e", "c"));
        Assert.IsTrue(undirectedGraph.IsReachable("d", "e"));
        Assert.IsTrue(undirectedGraph.IsReachable("d", "c"));
    }

    [TestMethod]
    public void Graph1ConnectedComponents()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/graph1.txt");

        Assert.AreEqual(1, undirectedGraph.ConnectedComponents);
    }


    [TestMethod]
    public void Graph2IsReachable()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/graph2.txt");

        Assert.IsFalse(undirectedGraph.IsReachable("a", "c"));
        Assert.IsFalse(undirectedGraph.IsReachable("e", "c"));
        Assert.IsFalse(undirectedGraph.IsReachable("d", "e"));
        Assert.IsFalse(undirectedGraph.IsReachable("b", "a"));
        Assert.IsFalse(undirectedGraph.IsReachable("d", "b"));

    }

    [TestMethod]
    public void Graph2ConnectedComponents()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/graph2.txt");

        Assert.AreEqual(5, undirectedGraph.ConnectedComponents);
    }


    [TestMethod]
    public void Graph3IsReachable()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/graph3.txt");

        Assert.IsTrue(undirectedGraph.IsReachable("a", "c"));
        Assert.IsTrue(undirectedGraph.IsReachable("e", "d"));
        Assert.IsTrue(undirectedGraph.IsReachable("h", "g"));

        Assert.IsFalse(undirectedGraph.IsReachable("a", "h"));
        Assert.IsFalse(undirectedGraph.IsReachable("c", "i"));
        Assert.IsFalse(undirectedGraph.IsReachable("g", "b"));

    }

    [TestMethod]
    public void Graph3ConnectedComponents()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/graph3.txt");

        Assert.AreEqual(3, undirectedGraph.ConnectedComponents);
    }

    [TestMethod]
    public void Graph4IsReachable()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/graph4.txt");

        Assert.IsTrue(undirectedGraph.IsReachable("a", "c"));
        Assert.IsTrue(undirectedGraph.IsReachable("e", "i"));
        Assert.IsTrue(undirectedGraph.IsReachable("g", "b"));
        Assert.IsTrue(undirectedGraph.IsReachable("c", "f"));
        Assert.IsTrue(undirectedGraph.IsReachable("a", "d"));
        Assert.IsTrue(undirectedGraph.IsReachable("b", "i"));

    }

    [TestMethod]
    public void Graph4ConnectedComponents()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/graph4.txt");

        Assert.AreEqual(1, undirectedGraph.ConnectedComponents);
    }

    [TestMethod]
    public void SavannahIsReachable()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/Savannah.txt");

        Assert.IsTrue(undirectedGraph.IsReachable("a", "c"));
        Assert.IsTrue(undirectedGraph.IsReachable("e", "i"));
        Assert.IsTrue(undirectedGraph.IsReachable("g", "b"));
        Assert.IsTrue(undirectedGraph.IsReachable("c", "f"));
        Assert.IsTrue(undirectedGraph.IsReachable("a", "j"));
        Assert.IsTrue(undirectedGraph.IsReachable("b", "i"));


        Assert.IsFalse(undirectedGraph.IsReachable("a", "d"));
        Assert.IsFalse(undirectedGraph.IsReachable("d", "j"));

    }

    [TestMethod]
    public void SavannahConnectedComponents()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/Savannah.txt");

        Assert.AreEqual(2, undirectedGraph.ConnectedComponents);
    }

    [TestMethod]
    public void DFSPathBetween()
    {
        UndirectedWeightedGraph weighted_graph = new UndirectedWeightedGraph("../../../graphs/graph1-weighted.txt");
        List<Node> list = new List<Node>();

        Assert.AreEqual(2, weighted_graph.DFSPathBetween("a", "b", out list));
        Assert.AreEqual(7, weighted_graph.DFSPathBetween("a", "c", out list));
        Assert.AreEqual(5, weighted_graph.DFSPathBetween("a", "e", out list));
    }

    [TestMethod]
    public void BFSPathBetween()
    {
        UndirectedWeightedGraph weighted_graph = new UndirectedWeightedGraph("../../../graphs/graph1-weighted.txt");
        List<Node> list = new List<Node>();

        Assert.AreEqual(2, weighted_graph.BFSPathBetween("a", "b", out list));
        Assert.AreEqual(7, weighted_graph.BFSPathBetween("a", "c", out list));
        Assert.AreEqual(5, weighted_graph.BFSPathBetween("a", "e", out list));
    }

    [TestMethod]
    public void DijkstraPathBetween()
    {
        UndirectedWeightedGraph weighted_graph = new UndirectedWeightedGraph("../../../graphs/graph1-weighted.txt");
        List<Node> list = new List<Node>();

        Assert.AreEqual(2, weighted_graph.DijkstraPathBetween("a", "b", out list));
        Assert.AreEqual(7, weighted_graph.DijkstraPathBetween("a", "c", out list));
        Assert.AreEqual(5, weighted_graph.DijkstraPathBetween("a", "e", out list));
    }
}

