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
        Node start = new Node(0, 0);
        public Node end = new Node(9,9);

        public Agent()
        {
            //add starting position
            open.Add(start);
            
            //Add blocked off spaces
            closed.Add(new Node(1,2));
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
            Node current = open[0];
            open.Remove(current);
            closed.Add(current);
            List<Node> adjacent = FindAdjacent(current);

            bool moveable = false;

            int lowestCost = adjacent[0].fValue;
            Node temp=adjacent[0];
            foreach (Node n in adjacent)
            {
                foreach (Node nope in closed)
                {
                    if (nope.xCoord != n.xCoord || nope.yCoord!=n.yCoord)
                    {
                        moveable = true;
                    }
                    else
                    {
                        moveable = false;
                        break;
                    }
                }
                if (moveable)
                {
                    if (n.fValue < lowestCost)
                    {
                        lowestCost = n.fValue;
                        temp = n;
                    }
                    else if (n.fValue == lowestCost)
                    {
                        if (n.hValue < temp.hValue)
                        {
                            temp = n;
                        }
                        else if (n.hValue == temp.hValue)
                        {
                            if (n.gValue > temp.gValue)
                            {
                                temp = n;
                            }
                        }
                    }
                }
            }
            temp.parent = current;
            open.Add(temp);
        }

        public List<Node> FindAdjacent(Node n)
        {
            List<Node> temp = new List<Node>();
            //left
            if (n.yCoord - 1 > 0 && n.yCoord - 1 < 9)
            {
                Node tempN = new Node(n.xCoord, n.yCoord - 1);
                tempN.parent = n;
                CalculateValues(tempN);
                temp.Add(tempN);
            }
            //right
            if (n.yCoord + 1 > 0 && n.yCoord + 1 < 9)
            {
                Node tempN = new Node(n.xCoord, n.yCoord + 1);
                tempN.parent = n;
                CalculateValues(tempN);
                temp.Add(tempN);
            }
            //up
            if (n.xCoord - 1 > 0 && n.xCoord - 1 < 9)
            {
                Node tempN = new Node(n.xCoord - 1, n.yCoord);
                tempN.parent = n;
                CalculateValues(tempN);
                temp.Add(tempN);
            }
            //down
            if (n.xCoord + 1 > 0 && n.xCoord + 1 < 9)
            {
                Node tempN = new Node(n.xCoord + 1, n.yCoord);
                tempN.parent = n;
                CalculateValues(tempN);
                temp.Add(tempN);
            }
            //left-up
            if (n.xCoord - 1 > 0 && n.xCoord - 1 < 9 && n.yCoord - 1 > 0 && n.yCoord - 1 < 9)
            {
                Node tempN = new Node(n.xCoord - 1, n.yCoord-1);
                tempN.parent = n;
                CalculateValues(tempN);
                temp.Add(tempN);
            }
            //left-down
            if (n.xCoord - 1 > 0 && n.xCoord - 1 < 9 && n.yCoord + 1 > 0 && n.yCoord + 1 < 9)
            {
                Node tempN = new Node(n.xCoord - 1, n.yCoord+1);
                tempN.parent = n;
                CalculateValues(tempN);
                temp.Add(tempN);
            }
            //right up
            if (n.xCoord + 1 > 0 && n.xCoord + 1 < 9 && n.yCoord - 1 > 0 && n.yCoord - 1 < 9)
            {
                Node tempN = new Node(n.xCoord + 1, n.yCoord-1);
                tempN.parent = n;
                CalculateValues(tempN);
                temp.Add(tempN);
            }
            //right down
            if (n.xCoord + 1 > 0 && n.xCoord + 1 < 9 && n.yCoord + 1 > 0 && n.yCoord + 1 < 9)
            {
                Node tempN = new Node(n.xCoord + 1, n.yCoord+1);
                tempN.parent = n;
                CalculateValues(tempN);
                temp.Add(tempN);
            }

            //return list
            return temp;
        }

        //calculate distance values
        public void CalculateValues(Node n)
        {
            n.gValue = Math.Abs(n.xCoord - start.xCoord) + Math.Abs(n.yCoord - start.yCoord);
            n.hValue = Math.Abs(n.xCoord - end.xCoord) + Math.Abs(n.yCoord - end.yCoord);
            n.fValue = n.gValue + n.hValue;
        }
    }
}
