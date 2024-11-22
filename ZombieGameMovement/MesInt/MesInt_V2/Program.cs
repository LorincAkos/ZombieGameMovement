namespace MesInt
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    class Program
    {
        static void Main()
        {
            Dictionary<string, Graph> graphs = GraphParser.ParseGraphFile("graph.txt");
            string graphName = "GRAPH_2";
            string start = "S";
            string goal = "G";
            string heuristicKey = goal;
            int beamWidth = 2;
            double cost = 0;
            int expansionCount = 0;


            List<string> result = [];
            Console.WriteLine("DFS");
            long stopwatch = Stopwatch.GetTimestamp();
            result = GraphSearch.DFS(graphs[graphName], start, goal, out expansionCount);
            double ellapsedTime = Stopwatch.GetElapsedTime(stopwatch).TotalNanoseconds;
            Console.WriteLine("Time: " + ellapsedTime);
            PrintGraph(result);
            Console.WriteLine(expansionCount);

            Console.WriteLine("BFS");
            stopwatch = Stopwatch.GetTimestamp();
            result = GraphSearch.BFS(graphs[graphName], start, goal, out expansionCount);
            ellapsedTime = Stopwatch.GetElapsedTime(stopwatch).TotalNanoseconds;
            Console.WriteLine("Time: " + ellapsedTime);
            PrintGraph(result);
            Console.WriteLine(expansionCount);

            Console.WriteLine("Hill-Climbing");
            stopwatch = Stopwatch.GetTimestamp();
            result = GraphSearch.HillClimbing(graphs[graphName], start, goal, heuristicKey, out expansionCount);
            ellapsedTime = Stopwatch.GetElapsedTime(stopwatch).TotalNanoseconds;
            Console.WriteLine("Time: " + ellapsedTime);
            PrintGraph(result);
            Console.WriteLine(expansionCount);

            Console.WriteLine("Best First");
            stopwatch = Stopwatch.GetTimestamp();
            result = GraphSearch.BestFirstSearchAlgorithm(graphs[graphName], start, goal, heuristicKey, out expansionCount);
            ellapsedTime = Stopwatch.GetElapsedTime(stopwatch).TotalNanoseconds;
            Console.WriteLine("Time: " + ellapsedTime);
            PrintGraph(result);
            Console.WriteLine(expansionCount);

            Console.WriteLine("Beam Search");
            stopwatch = Stopwatch.GetTimestamp();
            result = GraphSearch.BeamSearchAlgorithm(graphs[graphName],start,goal, heuristicKey,beamWidth,out expansionCount);
            ellapsedTime = Stopwatch.GetElapsedTime(stopwatch).TotalNanoseconds;
            Console.WriteLine("Time: " + ellapsedTime);
            PrintGraph(result);
            Console.WriteLine(expansionCount);

            Console.WriteLine("Branch & Bound");
            stopwatch = Stopwatch.GetTimestamp();
            result = GraphSearch.BranchAndBoundAlgorithm(graphs[graphName], start, goal, out expansionCount);
            ellapsedTime = Stopwatch.GetElapsedTime(stopwatch).TotalNanoseconds;
            Console.WriteLine("Time: " + ellapsedTime);
            PrintGraph(result);
            Console.WriteLine(expansionCount);

            Console.WriteLine("Branch & Bound Extended");
            stopwatch = Stopwatch.GetTimestamp();
            result = GraphSearch.BranchAndBoundExtendedAlgorithm(graphs[graphName], start, goal, out cost);
            ellapsedTime = Stopwatch.GetElapsedTime(stopwatch).TotalNanoseconds;
            Console.WriteLine("Time: " + ellapsedTime);
            PrintGraph(result);

            Console.WriteLine("Branc & Bound Heuristic");
            stopwatch = Stopwatch.GetTimestamp();
            result = GraphSearch.BranchAndBoundHeuristicAlgorithm(graphs[graphName], start, goal, heuristicKey, out cost);
            ellapsedTime = Stopwatch.GetElapsedTime(stopwatch).TotalNanoseconds;
            Console.WriteLine("Time: " + ellapsedTime);
            PrintGraph(result);

            Console.WriteLine("A*");
            stopwatch = Stopwatch.GetTimestamp();
            result = GraphSearch.AStarSearchAlgorithm(graphs[graphName], start, goal, heuristicKey, out cost);
            ellapsedTime = Stopwatch.GetElapsedTime(stopwatch).TotalNanoseconds;
            Console.WriteLine("Time: " + ellapsedTime);
            PrintGraph(result);



             graphName = "GRAPH_3";
             start = "s";
             goal = "g";
             heuristicKey = goal;
             beamWidth = 2;
             cost = 0;
             expansionCount = 0;
            Console.WriteLine("//////////////////////////////////////////////////////////////////");
            Console.WriteLine("\n");
            Console.WriteLine("//////////////////////////////////////////////////////////////////");

            Console.WriteLine("DFS");
             stopwatch = Stopwatch.GetTimestamp();
            result = GraphSearch.DFS(graphs[graphName], start, goal, out expansionCount);
             ellapsedTime = Stopwatch.GetElapsedTime(stopwatch).TotalNanoseconds;
            Console.WriteLine("Time: " + ellapsedTime);
            PrintGraph(result);
            Console.WriteLine(expansionCount);

            Console.WriteLine("BFS");
            stopwatch = Stopwatch.GetTimestamp();
            result = GraphSearch.BFS(graphs[graphName], start, goal, out expansionCount);
            ellapsedTime = Stopwatch.GetElapsedTime(stopwatch).TotalNanoseconds;
            Console.WriteLine("Time: " + ellapsedTime);
            PrintGraph(result);
            Console.WriteLine(expansionCount);

            Console.WriteLine("Hill-Climbing");
            stopwatch = Stopwatch.GetTimestamp();
            result = GraphSearch.HillClimbing(graphs[graphName], start, goal, heuristicKey, out expansionCount);
            ellapsedTime = Stopwatch.GetElapsedTime(stopwatch).TotalNanoseconds;
            Console.WriteLine("Time: " + ellapsedTime);
            PrintGraph(result);
            Console.WriteLine(expansionCount);

            Console.WriteLine("Best First");
            stopwatch = Stopwatch.GetTimestamp();
            result = GraphSearch.BestFirstSearchAlgorithm(graphs[graphName], start, goal, heuristicKey,out expansionCount);
            ellapsedTime = Stopwatch.GetElapsedTime(stopwatch).TotalNanoseconds;
            Console.WriteLine("Time: " + ellapsedTime);
            PrintGraph(result);
            Console.WriteLine(expansionCount);

            Console.WriteLine("Beam Search");
            stopwatch = Stopwatch.GetTimestamp();
            result = GraphSearch.BeamSearchAlgorithm(graphs[graphName], start, goal, heuristicKey, beamWidth, out expansionCount);
            ellapsedTime = Stopwatch.GetElapsedTime(stopwatch).TotalNanoseconds;
            Console.WriteLine("Time: " + ellapsedTime);
            PrintGraph(result);
            Console.WriteLine(expansionCount);

            Console.WriteLine("Branch & Bound");
            stopwatch = Stopwatch.GetTimestamp();
            result = GraphSearch.BranchAndBoundAlgorithm(graphs[graphName], start, goal, out expansionCount);
            ellapsedTime = Stopwatch.GetElapsedTime(stopwatch).TotalNanoseconds;
            Console.WriteLine("Time: " + ellapsedTime);
            PrintGraph(result);
            Console.WriteLine(expansionCount);

            Console.WriteLine("Branch & Bound Extended");
            stopwatch = Stopwatch.GetTimestamp();
            result = GraphSearch.BranchAndBoundExtendedAlgorithm(graphs[graphName], start, goal, out cost);
            ellapsedTime = Stopwatch.GetElapsedTime(stopwatch).TotalNanoseconds;
            Console.WriteLine("Time: " + ellapsedTime);
            PrintGraph(result);

            Console.WriteLine("Branc & Bound Heuristic");
            stopwatch = Stopwatch.GetTimestamp();
            result = GraphSearch.BranchAndBoundHeuristicAlgorithm(graphs[graphName], start, goal, heuristicKey, out cost);
            ellapsedTime = Stopwatch.GetElapsedTime(stopwatch).TotalNanoseconds;
            Console.WriteLine("Time: " + ellapsedTime);
            PrintGraph(result);

            Console.WriteLine("A*");
            stopwatch = Stopwatch.GetTimestamp();
            result = GraphSearch.AStarSearchAlgorithm(graphs[graphName], start, goal, heuristicKey, out cost);
            ellapsedTime = Stopwatch.GetElapsedTime(stopwatch).TotalNanoseconds;
            Console.WriteLine("Time: " + ellapsedTime);
            PrintGraph(result);
            Console.WriteLine("ada");
        }

        static void PrintGraph(List<string> result)
        {
            if (result != null)
            {
                Console.WriteLine("Path found: " + string.Join(" -> ", result));
                Console.WriteLine("\n");
            }
            else
            {
                Console.WriteLine("No path found.");
                Console.WriteLine("\n");
            }
        }
    }
}
