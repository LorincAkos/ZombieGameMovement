using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesInt
{
    public class Edge
    {
        public string Node1 { get; }
        public string Node2 { get; }
        public double Weight { get; }

        public Edge(string node1, string node2, double weight = 0)
        {
            Node1 = node1;
            Node2 = node2;
            Weight = weight;
        }
    }
}
