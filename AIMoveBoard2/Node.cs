using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMoveBoard2
{
    class Node
    {
        public int fValue { get; set; }
        public int gValue { get; set; }
        public int hValue { get; set; }
        public int xCoord { get; set; }
        public int yCoord { get; set; }

        public Node parent { get; set; }

        public bool blockade;

        public Node(int x, int y, bool wall)
        {
            xCoord = x;
            yCoord = y;
            blockade = wall;
        }
    }
}
