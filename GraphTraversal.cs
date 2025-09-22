using System;
using System.Collections.Generic;

public class Graph
{
    private Dictionary<int, List<int>> adjList = new Dictionary<int, List<int>>();

    public void AddEdge(int u, int v)
    {
        if (!adjList.ContainsKey(u)) adjList[u] = new List<int>();
        if (!adjList.ContainsKey(v)) adjList[v] = new List<int>();
        adjList[u].Add(v);
        adjList[v].Add(u); // Undirected
    }

    public void DFS(int start)
    {
        HashSet<int> visited = new HashSet<int>();
        Stack<int> stack = new Stack<int>();
        stack.Push(start);

        while (stack.Count > 0)
        {
            int node = stack.Pop();
            if (!visited.Contains(node))
            {
                visited.Add(node);
                Console.WriteLine($"Visited: {node}");

                if (adjList.ContainsKey(node))
                {
                    foreach (int neighbor in adjList[node])
                    {
                        if (!visited.Contains(neighbor))
                        {
                            stack.Push(neighbor);
                        }
                    }
                }
            }
        }
    }
}

public class GraphTraversalApp
{
    public static void Run()
    {
        Graph g = new Graph();
        g.AddEdge(0, 1);
        g.AddEdge(0, 2);
        g.AddEdge(1, 3);
        g.AddEdge(1, 4);
        g.AddEdge(2, 5);
        g.AddEdge(2, 6);

        Console.WriteLine("DFS Traversal:");
        g.DFS(0);
    }
}