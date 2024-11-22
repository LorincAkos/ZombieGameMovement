using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesInt
{
    public class Graph
    {
        public List<string> Nodes { get; set; } = new List<string>();
        public List<Edge> Edges { get; set; } = new List<Edge>();
        public Dictionary<string, Dictionary<string, double>> Heuristics { get; set; } = new Dictionary<string, Dictionary<string, double>>();
    }

}
