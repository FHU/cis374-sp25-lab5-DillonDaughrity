namespace Lab5;

class Program
{
    static void Main(string[] args)
    {
        UndirectedWeightedGraph weighted_graph = new UndirectedWeightedGraph("../../../graphs/graph1-weighted.txt");
        List<Node> list = new List<Node>();

        Console.WriteLine(weighted_graph.BFSPathBetween("a", "e", out list));
        
        foreach (Node node in list)
        {
            Console.WriteLine(node.Name);
        }
    }
}

