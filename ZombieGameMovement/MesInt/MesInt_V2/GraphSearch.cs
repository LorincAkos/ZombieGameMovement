using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesInt
{
    public class GraphSearch
    {
        private static int expansionCount = 0;
        public static List<string> DFS(Graph graphData, string startNode, string goalNode, out int e)
        {
            Console.WriteLine(expansionCount);
            // Create an adjacency list for the graph
            var adjacencyList = new Dictionary<string, List<string>>();
            var nodes = (List<string>)graphData.Nodes;
            var edges = new List<Tuple<string, string, double>>();

            foreach (var edge in graphData.Edges)
            {
                string source = edge.Node1;
                string destination = edge.Node2;
                double weight = edge.Weight;

                edges.Add(Tuple.Create(source, destination, weight));
            }

            foreach (var node in nodes)
            {
                adjacencyList[node] = new List<string>();
            }

            foreach (var edge in edges)
            {
                var (node1, node2, _) = edge; // Ignore edge weights
                adjacencyList[node1].Add(node2);
                //adjacencyList[node2].Add(node1); // Assuming undirected graph
            }

            var visited = new HashSet<string>();
            var path = new List<string>();
            expansionCount = 0; // Initialize expansion count

            bool DFSRecursive(string node)
            {
                if (visited.Contains(node))
                    return false;

                // Mark node as visited and increment expansion count
                visited.Add(node);
                expansionCount++;
                path.Add(node);

                // If the goal is reached, stop search
                if (node == goalNode)
                    return true;

                // Process neighbors in adjacency list
                foreach (var neighbor in adjacencyList[node])
                {
                    if (DFSRecursive(neighbor))
                        return true;
                }

                // Backtrack if no solution found via this path
                path.RemoveAt(path.Count - 1);
                return false;
            }

            // Start DFS from the start node
            DFSRecursive(startNode);

            e = expansionCount;
            expansionCount = 0;
            return path;
        }

        public static List<string> BFS(Graph graphData, string startNode, string goalNode, out int e)
        {
            Console.WriteLine(expansionCount);
            // Create an adjacency list for the graph
            var adjacencyList = new Dictionary<string, List<string>>();
            var nodes = (List<string>)graphData.Nodes;
            var edges = new List<Tuple<string, string, double?>>();

            foreach (var edge in graphData.Edges)
            {
                string source = edge.Node1;
                string destination = edge.Node2;
                double? weight = edge.Weight;

                edges.Add(Tuple.Create(source, destination, weight));
            }

            foreach (var node in nodes)
            {
                adjacencyList[node] = new List<string>();
            }

            foreach (var edge in edges)
            {
                var (node1, node2, _) = edge; // Ignore edge weights
                adjacencyList[node1].Add(node2);
                //adjacencyList[node2].Add(node1); // Assuming undirected graph
            }

            var visited = new HashSet<string>();
            var queue = new Queue<Tuple<string, List<string>>>();
            queue.Enqueue(Tuple.Create(startNode, new List<string> { startNode }));

            while (queue.Count > 0)
            {
                var (currentNode, path) = queue.Dequeue();

                if (visited.Contains(currentNode))
                    continue;

                visited.Add(currentNode);

                expansionCount++;

                // If the goal is reached, return the path
                if (!string.IsNullOrEmpty(goalNode) && currentNode == goalNode)
                {
                    e = expansionCount;
                    expansionCount = 0;
                    return path;
                }

                // Add neighbors to the queue
                foreach (var neighbor in adjacencyList[currentNode])
                {
                    if (!visited.Contains(neighbor))
                    {
                        var newPath = new List<string>(path) { neighbor };
                        queue.Enqueue(Tuple.Create(neighbor, newPath));
                    }
                }
            }
            e = expansionCount;
            expansionCount = 0;
            return []; // Return an empty path if the goal is not reachable
        }

        public static List<string> HillClimbing(Graph graphData, string startNode, string goalNode, string heuristicKey, out int e)
        {
            Console.WriteLine(expansionCount);
            // Create an adjacency list for the graph
            var adjacencyList = new Dictionary<string, List<string>>();
            var nodes = (List<string>)graphData.Nodes;
            var edges = new List<Tuple<string, string, double?>>();

            foreach (var edge in graphData.Edges)
            {
                string source = edge.Node1;
                string destination = edge.Node2;
                double? weight = edge.Weight;

                edges.Add(Tuple.Create(source, destination, weight));
            }
            foreach (var node in nodes)
            {
                adjacencyList[node] = new List<string>();
            }

            foreach (var edge in edges)
            {
                var (node1, node2, _) = edge; // Ignore edge weights
                adjacencyList[node1].Add(node2);
                adjacencyList[node2].Add(node1); // Assuming undirected graph
            }

            var path = new List<string> { startNode };
            var visited = new HashSet<string>();
            var currentNode = startNode;

            while (currentNode != goalNode)
            {
                if (!adjacencyList.ContainsKey(currentNode))
                    break;

                var neighbors = adjacencyList[currentNode];

                if (neighbors.Count == 0)
                {
                    e = expansionCount;
                    expansionCount = 0;
                    return path; // Dead-end reached; terminate search
                }

                // Access heuristics for the goal node
                var heuristics = (Dictionary<string, Dictionary<string, double>>)graphData.Heuristics;
                if (!heuristics.TryGetValue(goalNode, out var goalHeuristics))
                {
                    e = expansionCount;
                    expansionCount = 0;
                    return path; // No heuristic data available for the goal node
                }

                // Filter neighbors with valid heuristic values and not visited
                var validNeighbors = neighbors
                    .Where(neighbor => goalHeuristics.ContainsKey(neighbor) && !visited.Contains(neighbor))
                    .ToList();

                if (validNeighbors.Count == 0)
                    break; // No valid neighbors to proceed


                // Increment expansion count for the current node's neighbors
                expansionCount++;

                // Select the neighbor with the best (lowest) heuristic value
                var nextNode = validNeighbors
                    .OrderBy(neighbor => goalHeuristics[neighbor])
                    .First();

                // Add the current node to the visited set
                visited.Add(currentNode);

                // Append the next node to the path
                path.Add(nextNode);
                currentNode = nextNode;
            }

            e = expansionCount;
            expansionCount = 0;
            return path;
        }

        public static List<string> BestFirstSearchAlgorithm(Graph graphData, string startNode, string goalNode, string heuristicKey, out int e)
        {
            Console.WriteLine(expansionCount);
            // Create an adjacency list for the graph
            var adjacencyList = new Dictionary<string, List<string>>();
            var nodes = (List<string>)graphData.Nodes;
            var edges = new List<Tuple<string, string, double?>>();

            foreach (var edge in graphData.Edges)
            {
                string source = edge.Node1;
                string destination = edge.Node2;
                double? weight = edge.Weight;

                edges.Add(Tuple.Create(source, destination, weight));
            }

            foreach (var node in nodes)
            {
                adjacencyList[node] = new List<string>();
            }

            foreach (var edge in edges)
            {
                var (node1, node2, _) = edge; // Ignore edge weights
                adjacencyList[node1].Add(node2);
                adjacencyList[node2].Add(node1); // Assuming undirected graph
            }

            // Min-heap (priority queue) to expand the node with the lowest heuristic value
            var frontier = new SortedSet<(double priority, string node)>(
                Comparer<(double priority, string node)>.Create((a, b) =>
                    a.priority != b.priority ? a.priority.CompareTo(b.priority) : string.Compare(a.node, b.node, StringComparison.Ordinal)
                )
            );
            var heuristics = (Dictionary<string, Dictionary<string, double>>)graphData.Heuristics;

            // Add the start node to the frontier
            frontier.Add((heuristics[goalNode][startNode], startNode));

            // Dictionary to store the path taken
            var cameFrom = new Dictionary<string, string> { { startNode, null } };

            while (frontier.Count > 0)
            {
                // Remove the node with the lowest heuristic value
                var (currentPriority, currentNode) = frontier.Min;
                frontier.Remove(frontier.Min);
                expansionCount++;

                // If the goal node is reached, reconstruct the path
                if (currentNode == goalNode)
                {
                    var path = new List<string>();
                    while (currentNode != null)
                    {
                        path.Add(currentNode);
                        currentNode = cameFrom[currentNode];
                    }
                    path.Reverse();
                    e = expansionCount;
                    expansionCount = 0;
                    return path;
                }

                // Explore neighbors
                foreach (var neighbor in adjacencyList[currentNode])
                {
                    if (!cameFrom.ContainsKey(neighbor))
                    {
                        // Calculate the heuristic value for the neighbor
                        var priority = heuristics[goalNode].ContainsKey(neighbor)
                            ? heuristics[goalNode][neighbor]
                            : double.PositiveInfinity;

                        // Add neighbor to the frontier
                        frontier.Add((priority, neighbor));
                        cameFrom[neighbor] = currentNode;
                    }
                }
            }

            // Return an empty path if no solution is found
            e = expansionCount;
            expansionCount = 0;
            return [];
        }

        public static List<string> BeamSearchAlgorithm(Graph graphData, string startNode, string goalNode, string heuristicKey, int beamWidth, out int e)
        {
            Console.WriteLine(expansionCount);
            // Extract graph details
            var edges = new List<Tuple<string, string, double>>();

            foreach (var edge in graphData.Edges)
            {
                string source = edge.Node1;
                string destination = edge.Node2;
                double weight = edge.Weight;

                edges.Add(Tuple.Create(source, destination, weight));
            }
            var heuristics = (Dictionary<string, double>)graphData.Heuristics.GetValueOrDefault(goalNode, new Dictionary<string, double>());

            // Convert edges to adjacency list
            var adjacencyList = new Dictionary<string, List<(string Neighbor, double Cost)>>();
            foreach (var edge in edges)
            {
                var (node1, node2, cost) = edge;

                if (!adjacencyList.ContainsKey(node1))
                    adjacencyList[node1] = new List<(string, double)>();
                if (!adjacencyList.ContainsKey(node2))
                    adjacencyList[node2] = new List<(string, double)>();

                adjacencyList[node1].Add((node2, cost));
                adjacencyList[node2].Add((node1, cost)); // Assuming undirected graph
            }

            // Priority queue for beam search
            var beam = new List<(double PathCost, List<string> Path)>
    {
        (heuristics.GetValueOrDefault(startNode, double.PositiveInfinity), new List<string> { startNode })
    };


            while (beam.Any())
            {
                var nextBeam = new List<(double PathCost, List<string> Path)>();

                foreach (var (gCost, currentPath) in beam)
                {
                    var currentNode = currentPath.Last();

                    // If the goal node is reached, return the path and cost
                    if (currentNode == goalNode)
                    {

                        e = expansionCount;
                        expansionCount = 0;
                        return currentPath;
                    }

                    // Increment expansion count for each expanded node
                    expansionCount++;

                    // Explore neighbors
                    foreach (var (neighbor, edgeCost) in adjacencyList.GetValueOrDefault(currentNode, new List<(string, double)>()))
                    {
                        if (!currentPath.Contains(neighbor)) // Avoid revisiting nodes
                        {
                            var newGCost = gCost + edgeCost;
                            var hCost = heuristics.GetValueOrDefault(neighbor, double.PositiveInfinity);
                            nextBeam.Add((hCost + newGCost, new List<string>(currentPath) { neighbor }));
                        }
                    }
                }

                // Keep the best `beamWidth` nodes based on the heuristic
                nextBeam = nextBeam.OrderBy(x => x.PathCost).Take(beamWidth).ToList();
                beam = nextBeam;
            }


            e = expansionCount;
            expansionCount = 0;
            return null; // No path found
        }

        public static List<string> BranchAndBoundAlgorithm(Graph graphData, string startNode, string goalNode, out int e)
        {
            Console.WriteLine(expansionCount);
            // Convert edges to adjacency list with costs
            var edges = new List<Tuple<string, string, double>>();

            foreach (var edge in graphData.Edges)
            {
                string source = edge.Node1;
                string destination = edge.Node2;
                double weight = edge.Weight;

                edges.Add(Tuple.Create(source, destination, weight));
            }

            var adjacencyList = new Dictionary<string, List<(string Neighbor, double Cost)>>();

            foreach (var edge in edges)
            {
                var (node1, node2, cost) = edge;

                if (!adjacencyList.ContainsKey(node1))
                    adjacencyList[node1] = new List<(string, double)>();
                if (!adjacencyList.ContainsKey(node2))
                    adjacencyList[node2] = new List<(string, double)>();

                adjacencyList[node1].Add((node2, cost));
                adjacencyList[node2].Add((node1, cost)); // Assuming undirected graph
            }

            // Priority queue: (pathCost, currentPath)
            var priorityQueue = new SortedSet<(double PathCost, List<string> Path)>(Comparer<(double PathCost, List<string> Path)>.Create(
                (a, b) => a.PathCost != b.PathCost ? a.PathCost.CompareTo(b.PathCost) : string.Compare(string.Join(",", a.Path), string.Join(",", b.Path), StringComparison.Ordinal)
            ));

            priorityQueue.Add((0, new List<string> { startNode }));
            var visited = new HashSet<string>();

            while (priorityQueue.Any())
            {
                // Get the node with the least path cost
                var (currentCost, currentPath) = priorityQueue.Min;
                priorityQueue.Remove(priorityQueue.Min);

                var currentNode = currentPath.Last();

                // If we've already visited this node, skip it
                if (visited.Contains(currentNode))
                    continue;

                // Mark the node as visited
                visited.Add(currentNode);

                // Goal check
                if (currentNode == goalNode)
                {

                    e = expansionCount;
                    expansionCount = 0;
                    return currentPath;
                }

                foreach (var (neighbor, edgeCost) in adjacencyList.GetValueOrDefault(currentNode, new List<(string, double)>()))
                {
                    if (!visited.Contains(neighbor))
                    {
                        var totalCost = currentCost + edgeCost;
                        var newPath = new List<string>(currentPath) { neighbor };
                        priorityQueue.Add((totalCost, newPath));

                        // Increment the expansion count and log the expansion
                        expansionCount++;
                        //Debug
                        //Console.WriteLine($"Expanding node: {currentNode} -> {neighbor} with path: {string.Join(" -> ", newPath)}");
                    }
                }
            }

            e = expansionCount;
            expansionCount = 0;
            return null; // No path found
        }


        public static List<string> BranchAndBoundExtendedAlgorithm(Graph graphData, string startNode, string goalNode, out double graphCost)
        {
            // Extract edges and convert to adjacency list with costs
            var edges = new List<Tuple<string, string, double>>();

            foreach (var edge in graphData.Edges)
            {
                string source = edge.Node1;
                string destination = edge.Node2;
                double weight = edge.Weight;

                edges.Add(Tuple.Create(source, destination, weight));
            }
            var adjacencyList = new Dictionary<string, List<(string Neighbor, double Cost)>>();

            foreach (var edge in edges)
            {
                var (node1, node2, cost) = edge;

                if (!adjacencyList.ContainsKey(node1))
                    adjacencyList[node1] = new List<(string, double)>();
                if (!adjacencyList.ContainsKey(node2))
                    adjacencyList[node2] = new List<(string, double)>();

                adjacencyList[node1].Add((node2, cost));
                adjacencyList[node2].Add((node1, cost)); // Assuming undirected graph
            }

            // Priority queue: (pathCost, currentPath)
            var priorityQueue = new SortedSet<(double PathCost, List<string> Path)>(
                Comparer<(double PathCost, List<string> Path)>.Create((a, b) =>
                    a.PathCost != b.PathCost ? a.PathCost.CompareTo(b.PathCost) : string.Compare(string.Join(",", a.Path), string.Join(",", b.Path), StringComparison.Ordinal)
                )
            );

            priorityQueue.Add((0, new List<string> { startNode }));

            // Extended set to store visited nodes with the minimum cost to reach them
            var extendedSet = new Dictionary<string, double>();

            while (priorityQueue.Any())
            {
                // Get the node with the least path cost
                var (currentCost, currentPath) = priorityQueue.Min;
                priorityQueue.Remove(priorityQueue.Min);

                var currentNode = currentPath.Last();

                // If we've reached the goal, return the path and its cost
                if (currentNode == goalNode)
                {
                    graphCost = currentCost;
                    return currentPath;
                }

                // If the current node is already visited with a lower cost, skip it
                if (extendedSet.ContainsKey(currentNode) && extendedSet[currentNode] <= currentCost)
                    continue;

                // Update the extended set with the current node and its cost
                extendedSet[currentNode] = currentCost;

                // Expand neighbors
                foreach (var (neighbor, edgeCost) in adjacencyList.GetValueOrDefault(currentNode, new List<(string, double)>()))
                {
                    var totalCost = currentCost + edgeCost;
                    var newPath = new List<string>(currentPath) { neighbor };
                    // Add to the priority queue for further exploration
                    priorityQueue.Add((totalCost, newPath));
                }
            }

            graphCost = double.PositiveInfinity;
            return null; // No path found
        }

        public static List<string> BranchAndBoundHeuristicAlgorithm(Graph graphData, string startNode, string goalNode, string heuristicKey, out double graphCost)
        {
            // Extract graph details
            var edges = new List<Tuple<string, string, double>>();

            foreach (var edge in graphData.Edges)
            {
                string source = edge.Node1;
                string destination = edge.Node2;
                double weight = edge.Weight;

                edges.Add(Tuple.Create(source, destination, weight));
            }
            var heuristics = ((Dictionary<string, double>)graphData.Heuristics.GetValueOrDefault(goalNode, new Dictionary<string, double>()));

            // Convert edges to an adjacency list
            var adjacencyList = new Dictionary<string, List<(string Neighbor, double Cost)>>();

            foreach (var edge in edges)
            {
                var (node1, node2, cost) = edge;

                if (!adjacencyList.ContainsKey(node1))
                    adjacencyList[node1] = new List<(string, double)>();
                if (!adjacencyList.ContainsKey(node2))
                    adjacencyList[node2] = new List<(string, double)>();

                adjacencyList[node1].Add((node2, cost));
                adjacencyList[node2].Add((node1, cost)); // Assuming undirected graph
            }

            // Priority queue: (estimatedTotalCost, pathCost, currentPath)
            var priorityQueue = new SortedSet<(double EstimatedTotalCost, double PathCost, List<string> Path)>(
                Comparer<(double EstimatedTotalCost, double PathCost, List<string> Path)>.Create((a, b) =>
                    a.EstimatedTotalCost != b.EstimatedTotalCost ? a.EstimatedTotalCost.CompareTo(b.EstimatedTotalCost) : string.Compare(string.Join(",", a.Path), string.Join(",", b.Path), StringComparison.Ordinal)
                )
            );

            priorityQueue.Add((heuristics.GetValueOrDefault(startNode, double.PositiveInfinity), 0, new List<string> { startNode }));

            // Extended set for visited nodes
            var extendedSet = new Dictionary<string, double>();

            while (priorityQueue.Any())
            {
                // Get the path with the smallest estimated total cost
                var (estimatedTotalCost, pathCost, currentPath) = priorityQueue.Min;
                priorityQueue.Remove(priorityQueue.Min);

                var currentNode = currentPath.Last();

                // If the goal is reached, return the path and its cost
                if (currentNode == goalNode)
                {
                    graphCost = pathCost;
                    return currentPath;
                }

                // Skip if already visited with a cheaper cost
                if (extendedSet.ContainsKey(currentNode) && extendedSet[currentNode] <= pathCost)
                    continue;

                // Mark the current node with the current cost
                extendedSet[currentNode] = pathCost;

                // Expand neighbors
                foreach (var (neighbor, edgeCost) in adjacencyList.GetValueOrDefault(currentNode, new List<(string, double)>()))
                {
                    var newCost = pathCost + edgeCost;
                    var heuristicCost = heuristics.GetValueOrDefault(neighbor, double.PositiveInfinity);
                    var estimatedCost = newCost + heuristicCost;
                    var newPath = new List<string>(currentPath) { neighbor };
                    // Push into the priority queue
                    priorityQueue.Add((estimatedCost, newCost, newPath));
                }
            }

            graphCost = double.PositiveInfinity;
            return null; // No path found
        }

        public static List<string> AStarSearchAlgorithm(Graph graphData, string startNode, string goalNode, string heuristicKey, out double graphCost)
        {
            // Extract graph details
            var edges = new List<Tuple<string, string, double>>();

            foreach (var edge in graphData.Edges)
            {
                string source = edge.Node1;
                string destination = edge.Node2;
                double weight = edge.Weight;

                edges.Add(Tuple.Create(source, destination, weight));
            }
            var heuristics = ((Dictionary<string, double>)graphData.Heuristics.GetValueOrDefault(goalNode, new Dictionary<string, double>()));

            // Convert edges to an adjacency list
            var adjacencyList = new Dictionary<string, List<(string Neighbor, double Cost)>>();

            foreach (var edge in edges)
            {
                var (node1, node2, cost) = edge;

                if (!adjacencyList.ContainsKey(node1))
                    adjacencyList[node1] = new List<(string, double)>();
                if (!adjacencyList.ContainsKey(node2))
                    adjacencyList[node2] = new List<(string, double)>();

                adjacencyList[node1].Add((node2, cost));
                adjacencyList[node2].Add((node1, cost)); // Assuming undirected graph
            }

            // Priority queue: (fCost, gCost, currentPath)
            var openList = new SortedSet<(double fCost, double gCost, List<string> Path)>(
                Comparer<(double fCost, double gCost, List<string> Path)>.Create((a, b) =>
                    a.fCost != b.fCost ? a.fCost.CompareTo(b.fCost) : string.Compare(string.Join(",", a.Path), string.Join(",", b.Path), StringComparison.Ordinal)
                )
            );

            openList.Add((heuristics.GetValueOrDefault(startNode, double.PositiveInfinity), 0, new List<string> { startNode }));

            // Closed set for visited nodes
            var closedSet = new Dictionary<string, double>();

            while (openList.Any())
            {
                // Get the node with the smallest fCost
                var (fCost, gCost, currentPath) = openList.Min;
                openList.Remove(openList.Min);

                var currentNode = currentPath.Last();

                // If the goal is reached, return the path and cost
                if (currentNode == goalNode)
                {
                    graphCost = gCost;
                   return currentPath;
                }

                // Skip if already visited with a smaller cost
                if (closedSet.ContainsKey(currentNode) && closedSet[currentNode] <= gCost)
                    continue;

                // Mark node as visited
                closedSet[currentNode] = gCost;

                // Explore neighbors
                foreach (var (neighbor, edgeCost) in adjacencyList.GetValueOrDefault(currentNode, new List<(string, double)>()))
                {
                    var newGCost = fCost + edgeCost;
                    var hCost = heuristics.GetValueOrDefault(neighbor, double.PositiveInfinity);
                    var fCostNew = newGCost + hCost;
                    var newPath = new List<string>(currentPath) { neighbor };
                    // Push into the priority queue
                    openList.Add((fCostNew, newGCost, newPath));
                }
            }

            graphCost = double.PositiveInfinity;
            return null; // No path found
        }
    }
}
