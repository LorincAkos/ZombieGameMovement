using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesInt
{
    public class Edge
    {
        public Node From { get; set; }
        public Node To { get; set; }
        public double? Weight { get; set; }

        public Edge(Node from, Node to, double? weight = null)
        {
            From = from;
            To = to;
            Weight = weight;
        }
    }
}
