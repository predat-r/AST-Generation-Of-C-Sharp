using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Choose an option:");
        Console.WriteLine("1. Run BFS Traversal");
        Console.WriteLine("2. View AST");
        string choice = Console.ReadLine();

        if (choice == "1")
        {
            GraphTraversalApp.Run();
        }
        else if (choice == "2")
        {
            AstViewer.Run();
        }
        else
        {
            Console.WriteLine("Invalid choice.");
        }
    }
}
