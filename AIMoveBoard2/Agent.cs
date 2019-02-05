using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMoveBoard2
{
    class Agent
    {
        public List<Node> open = new List<Node>();
        List<Node> closed = new List<Node>();
        int GridRows = 10;
        int GridColumns = 10;
        Node start = new Node(0, 0);
        Node current;
        public Node end = new Node(9, 9);

        public Agent()
        {
            //add starting position
            open.Add(start);
            
            //Add blocked off spaces
            closed.Add(new Node(1, 2));
            closed.Add(new Node(2, 5));
            closed.Add(new Node(2, 6));
            closed.Add(new Node(2, 7));
            closed.Add(new Node(3, 1));
            closed.Add(new Node(3, 2));
            closed.Add(new Node(3, 3));
            closed.Add(new Node(4, 7));
            closed.Add(new Node(4, 8));
            closed.Add(new Node(4, 9));
            closed.Add(new Node(5, 0));
            closed.Add(new Node(5, 1));
            closed.Add(new Node(5, 2));
            closed.Add(new Node(6, 5));
            closed.Add(new Node(6, 6));
            closed.Add(new Node(8, 1));
            closed.Add(new Node(8, 2));
            closed.Add(new Node(8, 3));
            closed.Add(new Node(8, 4));
            closed.Add(new Node(8, 7));
            closed.Add(new Node(8, 8));
            closed.Add(new Node(8, 9));

           
        }

        public void MoveAgent()
        {
            while(open.Count>0 && !closed.Exists(node=>node==end))
            {
                current = open[0];
                open.Remove(current);
                closed.Add(current);
            }
        }

        public void PopulateGrid()
        {

        }


    }
}
