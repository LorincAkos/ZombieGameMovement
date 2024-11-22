using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesInt
{
    public class GraphParser
    {
        public static Dictionary<string, Graph> ParseGraphFile(string filePath)
        {
            var graphs = new Dictionary<string, Graph>();
            string currentGraph = null;
            string mode = null;

            foreach (var line in File.ReadLines(filePath))
            {
                var trimmedLine = line.Trim();

                if (string.IsNullOrEmpty(trimmedLine) || trimmedLine.StartsWith("#"))
                    continue;

                if (trimmedLine.StartsWith("graph"))
                {
                    var parts = trimmedLine.Split();
                    currentGraph = parts[1];
                    graphs[currentGraph] = new Graph();
                    mode = null;
                }
                else if (trimmedLine.StartsWith("nodes"))
                {
                    mode = "nodes";
                    var parts = trimmedLine.Split();
                    graphs[currentGraph].Nodes.AddRange(parts[1..]);
                }
                else if (trimmedLine.StartsWith("edges"))
                {
                    mode = "edges";
                }
                else if (trimmedLine.StartsWith("heuristic-start"))
                {
                    mode = "heuristics";
                }
                else if (trimmedLine.StartsWith("heuristic-end"))
                {
                    mode = null;
                }
                else if (mode == "edges" && currentGraph != null)
                {
                    var parts = trimmedLine.Split();
                    if (parts.Length == 2)
                    {
                        var (node1, node2) = (parts[0], parts[1]);
                        graphs[currentGraph].Edges.Add(new Edge(node1, node2));
                    }
                    else if (parts.Length == 3)
                    {
                        var (node1, node2) = (parts[0], parts[1]);
                        var weight = double.Parse(parts[2]);
                        graphs[currentGraph].Edges.Add(new Edge(node1, node2, weight));
                    }
                }
                else if (mode == "heuristics" && currentGraph != null)
                {
                    var parts = trimmedLine.Split();
                    var node = parts[0];
                    var heuristics = new Dictionary<string, double>();
                    for (int i = 1; i < parts.Length; i++)
                    {
                        var pair = parts[i].Split('-');
                        heuristics[pair[0]] = double.Parse(pair[1], CultureInfo.InvariantCulture);
                    }
                    graphs[currentGraph].Heuristics[node] = heuristics;
                }
            }

            return graphs;
        }
    }
}
