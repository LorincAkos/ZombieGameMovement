namespace MesInt
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    class Program
    {
        static void Main()

        {
            //var graphtmp = new Dictionary<string, List<string>>
            //{
            //    { "S", new List<string> { "A", "B" } },
            //    { "A", new List<string> { "C", "D" } },
            //    { "B", new List<string> { "C", "X", "Y" } },
            //    { "C", new List<string> { "D", "E", "Y" } },
            //    { "D", new List<string> { "E", "H" } },
            //    { "E", new List<string> { "F", "G" } },
            //    { "F", new List<string> { "G", "H" } }
            //};

            var heuristic = new Dictionary<string, Dictionary<string, int>>
        {
            { "S", new Dictionary<string, int> { { "S", 25 }, { "A", 20 }, { "B", 22 }, { "C", 14 }, { "D", 8 }, { "E", 3 }, { "F", 9 }, { "G", 0 }, { "H", 2 }, { "X", 0 }, { "Y", 0 } } },
            { "A", new Dictionary<string, int> { { "S", 25 }, { "A", 20 }, { "B", 22 }, { "C", 14 }, { "D", 8 }, { "E", 3 }, { "F", 9 }, { "G", 0 }, { "H", 2 }, { "X", 0 }, { "Y", 0 } } },
            { "B", new Dictionary<string, int> { { "S", 25 }, { "A", 20 }, { "B", 22 }, { "C", 14 }, { "D", 8 }, { "E", 3 }, { "F", 9 }, { "G", 0 }, { "H", 2 }, { "X", 0 }, { "Y", 0 } } },
            { "C", new Dictionary<string, int> { { "S", 25 }, { "A", 20 }, { "B", 22 }, { "C", 14 }, { "D", 8 }, { "E", 3 }, { "F", 9 }, { "G", 0 }, { "H", 2 }, { "X", 0 }, { "Y", 0 } } },
            { "D", new Dictionary<string, int> { { "S", 25 }, { "A", 20 }, { "B", 22 }, { "C", 14 }, { "D", 8 }, { "E", 3 }, { "F", 9 }, { "G", 0 }, { "H", 2 }, { "X", 0 }, { "Y", 0 } } },
            { "E", new Dictionary<string, int> { { "S", 25 }, { "A", 20 }, { "B", 22 }, { "C", 14 }, { "D", 8 }, { "E", 3 }, { "F", 9 }, { "G", 0 }, { "H", 2 }, { "X", 0 }, { "Y", 0 } } },
            { "F", new Dictionary<string, int> { { "S", 25 }, { "A", 20 }, { "B", 22 }, { "C", 14 }, { "D", 8 }, { "E", 3 }, { "F", 9 }, { "G", 0 }, { "H", 2 }, { "X", 0 }, { "Y", 0 } } },
            { "G", new Dictionary<string, int> { { "S", 25 }, { "A", 20 }, { "B", 22 }, { "C", 14 }, { "D", 8 }, { "E", 3 }, { "F", 9 }, { "G", 0 }, { "H", 2 }, { "X", 0 }, { "Y", 0 } } },
            { "H", new Dictionary<string, int> { { "S", 25 }, { "A", 20 }, { "B", 22 }, { "C", 14 }, { "D", 8 }, { "E", 3 }, { "F", 9 }, { "G", 0 }, { "H", 2 }, { "X", 0 }, { "Y", 0 } } },
            { "X", new Dictionary<string, int> { { "S", 25 }, { "A", 20 }, { "B", 22 }, { "C", 14 }, { "D", 8 }, { "E", 3 }, { "F", 9 }, { "G", 0 }, { "H", 2 }, { "X", 0 }, { "Y", 0 } } },
            { "Y", new Dictionary<string, int> { { "S", 25 }, { "A", 20 }, { "B", 22 }, { "C", 14 }, { "D", 8 }, { "E", 3 }, { "F", 9 }, { "G", 0 }, { "H", 2 }, { "X", 0 }, { "Y", 0 } } }
        };

            string filename = "graph.txt";
            List<Graph> graphs = GraphParser.ParseFile(filename);

            Dictionary<string, List<string>> currentGraph = GraphBuilder("GRAPH_2", graphs);

            Console.WriteLine("////////////////////////////////////////////////////////////");

            List<string> result = GraphSearch.BFS(currentGraph, "S", "G");
            Console.WriteLine("BFS");
            if (result != null)
            {
                Console.WriteLine("Path found: " + string.Join(" -> ", result));
            }
            else
            {
                Console.WriteLine("No path found.");
            }

            Console.WriteLine("////////////////////////////////////////////////////////////");

            result = GraphSearch.DFS(currentGraph, "S", "G");
            Console.WriteLine("DFS");
            if (result != null)
            {
                Console.WriteLine("Path found: " + string.Join(" -> ", result));
            }
            else
            {
                Console.WriteLine("No path found.");
            }

            Console.WriteLine("////////////////////////////////////////////////////////////");

            result = GraphSearch.HillClimbingWithHeuristic(currentGraph, heuristic,"S", "G");
            Console.WriteLine("Hill CLimb");
            if (result != null)
            {
                Console.WriteLine("Path found: " + string.Join(" -> ", result));
            }
            else
            {
                Console.WriteLine("No path found.");
            }

            Console.WriteLine("////////////////////////////////////////////////////////////");

            result = GraphSearch.BestFirstSearch(currentGraph, heuristic, "S", "G");
            Console.WriteLine("Best First");
            if (result != null)
            {
                Console.WriteLine("Path found: " + string.Join(" -> ", result));
            }
            else
            {
                Console.WriteLine("No path found.");
            }

            Console.WriteLine("////////////////////////////////////////////////////////////");

            result = GraphSearch.BestFirstSearch(currentGraph, heuristic, "S", "G");
            Console.WriteLine("Beam");
            if (result != null)
            {
                Console.WriteLine("Path found: " + string.Join(" -> ", result));
            }
            else
            {
                Console.WriteLine("No path found."); 
            }
        }

        private static Dictionary<string, List<string>> GraphBuilder(string graphName, List<Graph> graphs)
        {
            //Graph tmp = graphs[2];
            //Dictionary<string, List<string>> result = [];

            //foreach (Edge edge in tmp.Edges)
            //{
            //    string from = edge.From.Name;
            //    string to = edge.To.Name;

            //    if (!result.ContainsKey(from))
            //    {
            //        result[from] = new List<string>();
            //    }
            //    result[from].Add(to);
            //}

            //return result;

            Graph tmp = graphs[2];
            Dictionary<string, List<string>> result = [];

            foreach (Edge edge in tmp.Edges)
            {
                string from = edge.From.Name;
                string to = edge.To.Name;

                if (!result.ContainsKey(from))
                {
                    result[from] = new List<string>();
                }
                result[from].Add(to);
            }

            foreach (Edge edge in tmp.Edges)
            {
                string from = edge.From.Name;
                string to = edge.To.Name;

                if (!result.ContainsKey(to))
                {
                    result[to] = new List<string>();
                }
                if (!result[to].Contains(from))
                {
                    result[to].Add(from);
                }
            }
            return result;
        }
    }
}
