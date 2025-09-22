using System;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

public class AstViewer
{
    static void PrintTree(SyntaxNode node, string indent = "")
    {
        if (node == null) return;
        Console.WriteLine($"{indent}{node.GetType().Name}");
        foreach (var child in node.ChildNodesAndTokens())
        {
            if (child.IsNode)
            {
                var childNode = child.AsNode();
                if (childNode != null)
                    PrintTree(childNode, indent + "  ");
            }
            else
            {
                Console.WriteLine($"{indent}  Token: {child.ToString()}");
            }
        }
    }

    public static void Run()
    {
        string code = @"
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
        adjList[v].Add(u);
    }

    public void BFS(int start)
    {
        HashSet<int> visited = new HashSet<int>();
        Queue<int> queue = new Queue<int>();
        queue.Enqueue(start);
        visited.Add(start);

        while (queue.Count > 0)
        {
            int node = queue.Dequeue();
            Console.WriteLine($""Visited: {node}"");

            if (adjList.ContainsKey(node))
            {
                foreach (int neighbor in adjList[node])
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }
    }

}

class Program
{
    static void Main(string[] args)
    {
        Graph g = new Graph();
        g.AddEdge(0, 1);
        g.AddEdge(0, 2);
        g.AddEdge(1, 3);
        g.AddEdge(1, 4);
        g.AddEdge(2, 5);
        g.AddEdge(2, 6);

        Console.WriteLine(""BFS Traversal:"");
        g.BFS(0);
    }
}";

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        SyntaxTree tree = CSharpSyntaxTree.ParseText(code);
        var root = tree.GetRoot();
        Console.WriteLine("\nAST:");
        PrintTree(root);
        stopwatch.Stop();
        Console.WriteLine($"\nAST generation execution time: {stopwatch.ElapsedMilliseconds} ms");
    }
}