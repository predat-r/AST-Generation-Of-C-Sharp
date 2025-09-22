# Graph Traversal and AST Viewer

This project contains a simple graph traversal implementation (BFS) and an AST viewer using Roslyn.

## Prerequisites

- .NET 8.0 or later SDK installed. Download from [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)

## Setup and Running

1. Clone or pull the repository.

2. Navigate to the project directory.

3. Restore NuGet packages:
   ```
   dotnet restore
   ```

4. Run the application:
   ```
   dotnet run
   ```

5. Choose an option from the menu:
   - 1: Run BFS Traversal
   - 2: View AST

## Project Structure

- `Program.cs`: Main menu
- `GraphTraversal.cs`: Graph and BFS implementation
- `AstViewer.cs`: AST parsing and printing
- `.gitignore`: Git ignore file