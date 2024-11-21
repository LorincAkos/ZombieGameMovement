using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MesInt
{
    internal class GraphSearch
    {
        public static List<string> BFS(Dictionary<string, List<string>> graph, string start, string goal)
        {
            // Initialize the queue
            Queue<List<string>> queue = new();
            // Add the first element
            queue.Enqueue([start]);

            while (queue.Count > 0)
            {
                // Take the first element
                List<string> path = queue.Dequeue();
                // Last node of the path (to be extended)
                string endNode = path[^1]; // ^1 is equivalent to path[path.Count - 1]

                // Did we reach our goal?
                if (endNode == goal)
                    return path;

                // Extend (leave out nodes already in the path)
                if (graph.TryGetValue(endNode, out List<string>?adjacents))
                {
                    foreach (string adjacent in adjacents)
                    {
                        if (!path.Contains(adjacent))
                        {
                            // Create a new path and append it to the queue
                            List<string> newPath = new(path) { adjacent };
                            queue.Enqueue(newPath);
                        }
                    }
                }
            }
            return null;
        }

        public static List<string> DFS(Dictionary<string, List<string>> graph, string start, string goal)
        {
            List<string> path = []; // To track the traversal path
            Stack<string> stack = new(); // Stack for DFS

            stack.Push(start);

            while (stack.Count > 0)
            {
                string current = stack.Pop();

                // Add the current node to the path if not already visited
                if (!path.Contains(current))
                {
                    path.Add(current);

                    // Stop if the goal is reached
                    if (current == goal)
                        break;

                    // Add neighbors to the stack in reverse order for correct traversal
                    if (graph.TryGetValue(current, out List<string>? neighbors))
                    {
                        for (int i = neighbors.Count - 1; i >= 0; i--)
                        {
                            stack.Push(neighbors[i]);
                        }
                    }
                }
            }

            // Combine the path into a single string
            return path;
        }

        public static List<string> HillClimbingWithHeuristic(Dictionary<string, List<string>> graph, Dictionary<string, Dictionary<string, int>> heuristic, string start, string goal)
        {
            //NOTE: without visited it loops between D and H
            List<string> path = []; // To track the path
            List<string> visited = [];
            string currentNode = start;
            path.Add(currentNode);

            while (currentNode != goal)
            {
                // Get the neighbors of the current node
                if (!graph.TryGetValue(currentNode, out List<string>? neighbors) || neighbors.Count == 0)
                {
                    // If no neighbors exist or we are stuck, return failure
                    return null;
                }

                // Select the best neighbor based on the heuristic (lowest value is preferred)
                string nextNode = null;
                int bestHeuristicValue = int.MaxValue;

                foreach (string neighbor in neighbors)
                {
                    // Check if the neighbor has not been visited yet
                    if (visited.Contains(neighbor))
                        continue;

                    // Check if the heuristic value for this neighbor is better (lower)
                    if (heuristic[currentNode].TryGetValue(neighbor, out var value) && value < bestHeuristicValue)
                    {
                        nextNode = neighbor;
                        bestHeuristicValue = value;
                    }
                }

                // If we can't find a better neighbor, exit
                if (nextNode == null)
                {
                    return null;
                }

                // Add the selected neighbor to the path
                path.Add(nextNode);
                visited.Add(nextNode);
                currentNode = nextNode;

                // If we reach the goal, stop
                if (currentNode == goal)
                {
                    return path; // Return the path found
                }
            }

            return null; // If we exit the loop without finding the goal
        }

        // Best-First Search Algorithm with Heuristic to Find the Path
        public static List<string> BestFirstSearch(Dictionary<string, List<string>> graph, Dictionary<string, Dictionary<string, int>> heuristic, string start, string goal)
        {
            var path = new List<string>(); // To track the path
            var visited = new HashSet<string>(); // To track visited nodes and avoid cycles
            var priorityQueue = new SortedDictionary<int, List<string>>(); // Dictionary to store nodes grouped by heuristic values
            var nodeParent = new Dictionary<string, string>(); // To track the parent of each node for path reconstruction

            // Add the starting node with its heuristic value
            if (!priorityQueue.ContainsKey(heuristic[start][goal]))
            {
                priorityQueue[heuristic[start][goal]] = new List<string>();
            }
            priorityQueue[heuristic[start][goal]].Add(start);

            nodeParent[start] = null; // Start node has no parent
            visited.Add(start);

            while (priorityQueue.Count > 0)
            {
                // Get the node with the lowest heuristic value (first element in sorted order)
                var currentKey = priorityQueue.Keys.First(); // This returns the smallest key
                var currentNode = priorityQueue[currentKey][0]; // The first node at this key
                priorityQueue[currentKey].RemoveAt(0); // Remove the first node in the list

                if (priorityQueue[currentKey].Count == 0)
                {
                    priorityQueue.Remove(currentKey); // Remove the key if no more nodes with this heuristic value
                }

                // If the current node is the goal, reconstruct the path and return
                if (currentNode == goal)
                {
                    var resultPath = new List<string>();
                    while (currentNode != null)
                    {
                        resultPath.Insert(0, currentNode);
                        currentNode = nodeParent[currentNode];
                    }
                    return resultPath;
                }

                // Expand neighbors
                foreach (var neighbor in graph[currentNode])
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        nodeParent[neighbor] = currentNode;

                        // Add the neighbor to the priority queue with its heuristic value
                        if (!priorityQueue.ContainsKey(heuristic[neighbor][goal]))
                        {
                            priorityQueue[heuristic[neighbor][goal]] = new List<string>();
                        }
                        priorityQueue[heuristic[neighbor][goal]].Add(neighbor);
                    }
                }
            }

            return null; // If no path is found
        }

        public static List<string> BeamSearch(Dictionary<string, List<Tuple<string, int>>> graph, string start, string goal, int beamWidth)
        {
            var path = new List<string>(); // To track the path
            var visited = new HashSet<string>(); // To track visited nodes
            var currentNodes = new List<string> { start }; // List of nodes at the current level
            var nodeParent = new Dictionary<string, string>(); // To track the parent of each node for path reconstruction
            var costs = new Dictionary<string, int>(); // To track the cost to reach each node

            // Initialize costs for all nodes as infinity (except start node)
            foreach (var node in graph.Keys)
            {
                costs[node] = int.MaxValue;
            }
            costs[start] = 0;
            nodeParent[start] = null; // Start node has no parent

            while (currentNodes.Count > 0)
            {
                var nextLevelNodes = new List<string>(); // To store the nodes for the next level
                var candidateNodes = new List<Tuple<string, int>>(); // Candidate nodes for expansion

                // Expand all current nodes
                foreach (var node in currentNodes)
                {
                    foreach (var neighbor in graph[node])
                    {
                        string neighborNode = neighbor.Item1;
                        int edgeCost = neighbor.Item2;
                        int newCost = costs[node] + edgeCost;

                        if (newCost < costs[neighborNode])
                        {
                            costs[neighborNode] = newCost;
                            nodeParent[neighborNode] = node;
                            candidateNodes.Add(Tuple.Create(neighborNode, newCost));
                        }
                    }
                }

                // Sort candidates by cost and take the best 'beamWidth' number of nodes
                var bestNodes = candidateNodes
                    .OrderBy(x => x.Item2) // Sort by cost
                    .Take(beamWidth) // Take only the top 'beamWidth' candidates
                    .ToList();

                // Add the best nodes to the next level
                nextLevelNodes.AddRange(bestNodes.Select(x => x.Item1));

                // If the goal is in the current set of best nodes, reconstruct and return the path
                if (nextLevelNodes.Contains(goal))
                {
                    var resultPath = new List<string>();
                    string currentNode = goal;
                    while (currentNode != null)
                    {
                        resultPath.Insert(0, currentNode);
                        currentNode = nodeParent[currentNode];
                    }
                    return resultPath;
                }

                // Move to the next level
                currentNodes = nextLevelNodes.Distinct().ToList(); // Remove duplicates
            }

            return null; // If no path is found
        }
    }
}
