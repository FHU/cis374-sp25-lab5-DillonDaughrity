namespace Lab5;

class Program
{
    static void Main(string[] args)
    {
        UndirectedWeightedGraph weighted_graph = new UndirectedWeightedGraph("../../../graphs/graph1-weighted.txt");
        List<Node> list = new List<Node>();
        var dictionary = new Dictionary<Node, (Node pred, int dist)>();

        Console.WriteLine(weighted_graph.DijkstraPathBetween("a", "c", out list));
    }
}

