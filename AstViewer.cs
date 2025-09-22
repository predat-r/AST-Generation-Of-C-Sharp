using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

public class AstViewer
{
    static XElement BuildXmlTree(SyntaxNode node)
    {
        if (node == null) return null;
        var element = new XElement(node.GetType().Name);
        foreach (var child in node.ChildNodesAndTokens())
        {
            if (child.IsNode)
            {
                var childNode = child.AsNode();
                if (childNode != null)
                {
                    var childElement = BuildXmlTree(childNode);
                    if (childElement != null)
                        element.Add(childElement);
                }
            }
            else
            {
                element.Add(new XElement("Token", child.ToString()));
            }
        }
        return element;
    }

    static string dfsCode = @"
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
                Console.WriteLine($""Visited: {node}"");

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

        Console.WriteLine(""DFS Traversal:"");
        g.DFS(0);
    }
}";

    static string bfsCode = @"
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

    public static void GenerateDFSAST()
    {
        GenerateAST(dfsCode, "DFS_AST.xml");
    }

    public static void GenerateBFSAST()
    {
        GenerateAST(bfsCode, "BFS_AST.xml");
    }

    private static void GenerateAST(string code, string fileName)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        SyntaxTree tree = CSharpSyntaxTree.ParseText(code);
        var root = tree.GetRoot();
        var xmlTree = BuildXmlTree(root);
        stopwatch.Stop();
        string xmlContent = $"<!-- AST generation execution time: {stopwatch.ElapsedMilliseconds} ms -->\n{xmlTree.ToString()}";
        File.WriteAllText(fileName, xmlContent);
        Console.WriteLine($"AST generated and saved to {fileName}");
        Console.WriteLine($"AST generation execution time: {stopwatch.ElapsedMilliseconds} ms");
    }
}