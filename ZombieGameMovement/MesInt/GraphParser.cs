using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesInt
{
    class GraphParser
    {
        public static List<Graph> ParseFile(string filename)
        {
            var graphs = new List<Graph>();
            Graph currentGraph = null;
            bool parsingEdges = false;
            bool parsingHeuristics = false;

            foreach (var line in File.ReadLines(filename))
            {
                var trimmedLine = line.Trim();
                if (string.IsNullOrEmpty(trimmedLine) || trimmedLine.StartsWith("#"))
                    continue;

                if (trimmedLine.StartsWith("graph"))
                {
                    if (currentGraph != null)
                    {
                        graphs.Add(currentGraph);
                    }
                    currentGraph = new Graph(trimmedLine.Split()[1]);
                    parsingEdges = false;
                    parsingHeuristics = false;
                }
                else if (trimmedLine.StartsWith("nodes"))
                {
                    var nodeNames = trimmedLine.Split().Skip(1);
                    foreach (var nodeName in nodeNames)
                    {
                        currentGraph.Nodes.Add(new Node(nodeName));
                    }
                }
                else if (trimmedLine == "edges")
                {
                    parsingEdges = true;
                    parsingHeuristics = false;
                }
                else if (trimmedLine == "heuristic-start")
                {
                    parsingEdges = false;
                    parsingHeuristics = true;
                }
                else if (trimmedLine == "heuristic-end")
                {
                    parsingHeuristics = false;
                }
                else if (parsingEdges)
                {
                    var edgeInfo = trimmedLine.Split();
                    var fromNode = currentGraph.Nodes.Find(n => n.Name == edgeInfo[0]);
                    var toNode = currentGraph.Nodes.Find(n => n.Name == edgeInfo[1]);

                    double? weight = edgeInfo.Length == 3 ? double.Parse(edgeInfo[2]) : (double?)null;
                    currentGraph.Edges.Add(new Edge(fromNode, toNode, weight));
                }
                else if (parsingHeuristics)
                {
                    var heuristicParts = trimmedLine.Split();
                    var fromNodeName = heuristicParts[0];

                    if (!currentGraph.Heuristics.ContainsKey(fromNodeName))
                        currentGraph.Heuristics[fromNodeName] = new Dictionary<string, double>();

                    for (int i = 1; i < heuristicParts.Length; i++)
                    {
                        var targetAndValue = heuristicParts[i].Split('-');
                        var toNodeName = targetAndValue[0];
                        var heuristicValue = double.Parse(targetAndValue[1], CultureInfo.InvariantCulture);

                        currentGraph.Heuristics[fromNodeName][toNodeName] = heuristicValue;
                    }
                }
            }

            if (currentGraph != null)
            {
                graphs.Add(currentGraph);
            }

            return graphs;
        }
    }
}
