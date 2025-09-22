using System;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1. Run DFS Traversal");
            Console.WriteLine("2. Run BFS Traversal");
            Console.WriteLine("3. Generate DFS AST");
            Console.WriteLine("4. Generate BFS AST");
            Console.WriteLine("5. Quit");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                GraphTraversalApp.RunDFS();
            }
            else if (choice == "2")
            {
                GraphTraversalApp.RunBFS();
            }
            else if (choice == "3")
            {
                AstViewer.GenerateDFSAST();
            }
            else if (choice == "4")
            {
                AstViewer.GenerateBFSAST();
            }
            else if (choice == "5")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }
    }
}
