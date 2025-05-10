using System.Text.RegularExpressions;
using System.Xml;

namespace Lab5;

public class UndirectedWeightedGraph
{
    public List<Node> Nodes { get; set; }

    public UndirectedWeightedGraph()
    {
        Nodes = new List<Node>();
    }

    public UndirectedWeightedGraph(string path)
    {
        Nodes = new List<Node>();

        List<string> lines = new List<string>();

        try
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (line == "")
                    {
                        continue;
                    }
                    if (line[0] == '#')
                    {
                        continue;
                    }

                    lines.Add(line);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        // process the lines
        if (lines.Count < 1)
        {
            // empty file
            Console.WriteLine("Graph file was empty");
            return;
        }

        // Add nodes
        string[] nodeNames = Regex.Split(lines[0], @"\W+");

        foreach (var name in nodeNames)
        {
            Nodes.Add(new Node(name));
        }

        string[] nodeNamesAndWeight;
        // Add edges
        for (int i = 1; i < lines.Count; i++)
        {
            // extract node names
            nodeNamesAndWeight = Regex.Split(lines[i], @"\W+");
            if (nodeNamesAndWeight.Length < 3)
            {
                throw new Exception("Two nodes and a weight are required for each edge.");
            }

            // add edge between those nodes
            AddEdge(nodeNamesAndWeight[0], nodeNamesAndWeight[1], int.Parse(nodeNamesAndWeight[2]));
        }
    }

    public void AddEdge(string node1Name, string node2Name, int weight)
    {
        Node node1 = GetNodeByName(node1Name);
        Node node2 = GetNodeByName(node2Name);

        if (node1 == null || node2 == null)
        {
            throw new Exception("Invalid node name");
        }

        node1.Neighbors.Add(new Neighbor() { Node = node2, Weight = weight });
        node2.Neighbors.Add(new Neighbor() { Node = node1, Weight = weight });
    }

    private Node GetNodeByName(string nodeName)
    {
        var node = Nodes.Find(node => node.Name == nodeName);

        return node;
    }

    public int ConnectedComponents
    {
        get
        {
            int numConnectedComponents = 0;

            foreach (var node in Nodes)
                {
                    if (node.Color == Color.White)
                    {
                        DFS(node, false);
                        numConnectedComponents++;
                    }
                }

            return numConnectedComponents;
        }
    }


    public bool IsReachable(string node1name, string node2name)
    {
        Node node1 = GetNodeByName(node1name);
        Node node2 = GetNodeByName(node2name);

        if (node1 == null || node2 == null)
        {
            throw new Exception($"{node1name} or {node2name} does not exist.)");
        }

        // Do a DFS
        var pred = DFS(node1);

        // Was a pred for node2 found?
        return pred[node2] != null;
    }


    /// <summary>
    /// Searches the graph in a depth-first manner, creating a
    /// dictionary of the Node to Predessor Node links discovered by starting at the given node.
    /// Neighbors are visited in alphabetical order. 
    /// </summary>
    /// <param name="startingNode">The starting node of the depth first search</param>
    /// <returns>A dictionary of the Node to Predecessor Node 
    /// for each node in the graph reachable from the starting node
    /// as discovered by a DFS.</returns>
    public Dictionary<Node, Node> DFS(Node startingNode, bool reset = true)
    {
        Dictionary<Node, Node> pred = new Dictionary<Node, Node>();

        if (reset)
        {
            // setup DFS
            foreach (Node node in Nodes)
            {
                pred[node] = null;
                node.Color = Color.White;
            }
        }

        // call the recursive method
        DFSVisit(startingNode, pred);

        return pred;
    }

    //TODO
    /// <summary>
    /// Find the first path between the given nodes in a DFS manner 
    /// and return its total cost. Choices/ties are made in alphabetical order. 
    /// </summary>
    /// <param name="node1name">The starting node's name</param>
    /// <param name="node2name">The ending node's name</param>
    /// <param name="pathList">A list of the nodes in the path from the starting node to the ending node</param>
    /// <returns>The total cost of the weights in the path</returns>
    public int DFSPathBetween(string node1name, string node2name, out List<Node> pathList)
    {
        var nodes = DFS(GetNodeByName(node1name));

        pathList = new List<Node>();
        int cost = 0;

        var node1 = GetNodeByName(node1name);
        var node2 = GetNodeByName(node2name);
        pathList.Add(node2);

        foreach (Neighbor n in node2.Neighbors)
        {
            if (n.Node == nodes[node2]) {cost += n.Weight;}
        }
        
        var node = nodes[node2];

        while (node != node1)
        {
            pathList.Insert(0, node);

            foreach (Neighbor n in node.Neighbors)
            {
                if (n.Node == nodes[node]) {cost += n.Weight;}
            }

            node = nodes[node];
            
        }
        
        pathList.Insert(0, node);

        return cost;
    }

    private void DFSVisit(Node node, Dictionary<Node, Node> pred)
    {
        // color node gray
        node.Color = Color.Gray;

        // sort the neighbors so that we visit them in alpha order
        node.Neighbors.Sort();

        // visit every neighbor 
        foreach (var neighbor in node.Neighbors)
        {
            if (neighbor.Node.Color == Color.White)
            {
                pred[neighbor.Node] = node;
                DFSVisit(neighbor.Node, pred);
            }
        }

        // color the node black
        node.Color = Color.Black;
    }

    // TODO
    /// <summary>
    /// Searches the graph in a breadth-first manner, creating a
    /// dictionary of the Node to Predecessor and Distance discovered by starting at the given node.
    /// Neighbors are visited in alphabetical order. 
    /// </summary>
    /// <param name="startingNode"></param>
    /// <returns>A dictionary of the Node to Predecessor Node and Distance 
    /// for each node in the graph reachable from the starting node
    /// as discovered by a BFS.</returns>
    public Dictionary<Node, (Node pred, int dist)> BFS(Node startingNode)
    {
        var resultsDictionary = new Dictionary<Node, (Node pred, int dist)>();

        // initialize the dictionary 
        foreach (var node in Nodes)
        {
            node.Color = Color.White;
            resultsDictionary[node] = (null, int.MaxValue);
        }

        // setting up the starting node
        startingNode.Color = Color.Gray;
        resultsDictionary[startingNode] = (null, 0);

        // create a queue
        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(startingNode);

        // iteratively process the graph (neighbors of the nodes in the queue)
        while (queue.Count > 0)
        {
            // get the front of queue 
            var node = queue.Peek();

            // sort the neighbors so that we visit them in alpha order
            node.Neighbors.Sort();

            foreach (var neighbor in node.Neighbors)
            {
                if (neighbor.Node.Color == Color.White)
                {
                    neighbor.Node.Color = Color.Gray;
                    int distance = resultsDictionary[node].dist;
                    resultsDictionary[neighbor.Node] = (node, distance + 1);
                    queue.Enqueue(neighbor.Node);
                }
            }

            queue.Dequeue();
            node.Color = Color.Black;
        }

        return resultsDictionary;
    }

    /// <summary>
    /// Find the first path between the given nodes in a BFS manner 
    /// and return its total cost. Choices/ties are made in alphabetical order. 
    /// </summary>
    /// <param name="node1name">The starting node's name</param>
    /// <param name="node2name">The ending node's name</param>
    /// <param name="pathList">A list of the nodes in the path from the starting node to the ending node</param>
    /// <returns>The total cost of the weights in the path</returns>
    public int BFSPathBetween(string node1name, string node2name, out List<Node> pathList)
    {
        var nodes = BFS(GetNodeByName(node1name));

        pathList = new List<Node>();
        int cost = 0;

        var node1 = GetNodeByName(node1name);
        var node2 = GetNodeByName(node2name);
        pathList.Add(node2);

        foreach (Neighbor n in node2.Neighbors)
        {
            if (n.Node == nodes[node2].pred) {cost += n.Weight;}
        }
        
        var node = nodes[node2].pred;

        while (node != node1)
        {
            pathList.Insert(0, node);

            foreach (Neighbor n in node.Neighbors)
            {
                if (n.Node == nodes[node].pred) {cost += n.Weight;}
            }

            node = nodes[node].pred;
            
        }
        
        pathList.Insert(0, node);

        return cost;
    }


    public Dictionary<Node, (Node pred, int cost)> Dijkstra(Node startingNode)
    {
        var resultsDictionary = new Dictionary<Node, (Node pred, int dist)>();
        PriorityQueue<Neighbor, int> priorityQueue = new PriorityQueue<Neighbor, int>();
        resultsDictionary[startingNode] = (null, 0);

        foreach (var i in Nodes)
        {
            i.Color = Color.White;
        }

        startingNode.Color = Color.Black;

        foreach (Neighbor neighbor in startingNode.Neighbors) 
        {
            priorityQueue.Enqueue(neighbor, neighbor.Weight);
        }

        Node node = startingNode;
        Neighbor n = priorityQueue.Dequeue();
        Neighbor prev_n = default;
        int cost = 0;
        bool all_visited = true;

        while (priorityQueue.Count > 0 || cost == 0)
        {
            all_visited = true;

            if (n.Node.Color != Color.Black)
            {
                resultsDictionary[n.Node] = (node, cost + n.Weight);
                n.Node.Color = Color.Black;
            }

            foreach (var neighbor in n.Node.Neighbors)
            {
                if (neighbor.Node.Color != Color.Black)
                {
                    if (neighbor.Node.Color == Color.White) {priorityQueue.Enqueue(neighbor, neighbor.Weight + cost);}
                    neighbor.Node.Color = Color.Gray;
                    all_visited = false;
                }
            }

            if (all_visited)
            {
                n = prev_n;
                cost = resultsDictionary[n.Node].dist;
            }

            cost = resultsDictionary[n.Node].dist;
            node = n.Node;
            prev_n = n;
            n = priorityQueue.Dequeue();
        }

        resultsDictionary[n.Node] = (node, cost + n.Weight);
        n.Node.Color = Color.Black;

        return resultsDictionary;
    }

    /// <summary>
    /// Find the first path between the given nodes using Dijkstra's algorithm
    /// and return its total cost. Choices/ties are made in alphabetical order. 
    /// </summary>
    /// <param name="node1name">The starting node name</param>
    /// <param name="node2name">The ending node name</param>
    /// <param name="pathList">A list of the nodes in the path from the starting node to the ending node</param>
    /// <returns>The total cost of the weights in the path</returns>
    public int DijkstraPathBetween(string node1name, string node2name, out List<Node> pathList)
    {
        var nodes = Dijkstra(GetNodeByName(node1name));

        pathList = new List<Node>();
        int cost = 0;

        var node1 = GetNodeByName(node1name);
        var node2 = GetNodeByName(node2name);
        pathList.Add(node2);

        foreach (Neighbor n in node2.Neighbors)
        {
            if (n.Node == nodes[node2].pred) {cost += n.Weight;}
        }
        
        var node = nodes[node2].pred;

        while (node != node1)
        {
            pathList.Insert(0, node);

            foreach (Neighbor n in node.Neighbors)
            {
                if (n.Node == nodes[node].pred) {cost += n.Weight;}
            }

            node = nodes[node].pred;
            
        }
        
        pathList.Insert(0, node);

        return cost;
    }

}
