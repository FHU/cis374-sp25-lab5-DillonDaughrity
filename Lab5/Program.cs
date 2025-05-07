namespace Lab5;

class Program
{
    static void Main(string[] args)
    {
        UndirectedWeightedGraph weighted_graph = new UndirectedWeightedGraph("../../../graphs/graph1-weighted.txt");
        UndirectedUnweightedGraph unweighted_graph = new UndirectedUnweightedGraph("../../../graphs/graph1.txt");
        
        List<Node> list = new List<Node>();

        //unweighted_graph.DFS(unweighted_graph.Nodes[0]);
        weighted_graph.DFS(weighted_graph.Nodes[0]);
    }
}

