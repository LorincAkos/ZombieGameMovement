using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesInt
{
    public class Graph
    {
        public string Name { get; set; }
        public List<Node> Nodes { get; set; }
        public List<Edge> Edges { get; set; }
        public Dictionary<string, Dictionary<string, double>> Heuristics { get; set; }

        public Graph(string name)
        {
            Name = name;
            Nodes = new List<Node>();
            Edges = new List<Edge>();
            Heuristics = new Dictionary<string, Dictionary<string, double>>();
        }

        
    }
}
