using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMoveBoard2
{
    class Agent
    {
        //open spaces list
        public List<Node> open = new List<Node>();
        //closed spaces list
        List<Node> closed = new List<Node>();

        //number of grids and rows
        int GridRows = 10;
        int GridColumns = 10;
        //Start node
        Node start = new Node(0, 0);
        //end node
        public Node end = new Node(9, 9);
        //The current space the ai is on
        Node current;

        //list of adjacent nodes
        List<Node> adjacent = new List<Node>();
        //list of the path the ai takes
        public List<Node> path = new List<Node>();

        public Agent()
        {

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

            //add starting position
            CalculateValuesHorizontalVertical(start);
            open.Add(start);
            //add all the open spaces
            PopulateGrid();
            //add the end node
            open.Add(end);

        }

        //Move the agent around the board
        public void MoveAgent()
        {
            //the intial current occupied space is the start node
            current = open[0];
            //while there are open spaces and the end has not been moved to
            while(open.Count>0 && !closed.Exists(node=>node==end))
            {
                //remove the current occupied space from the open list
                open.Remove(current);
                //Add it to the closed list so that there are no repeats
                closed.Add(current);
                //Find the adjecent spaces
                FindSurrounding(current);
                //Make sure this space hasn't previously been moved to
                if (!path.Contains(current))
                    path.Add(current);

                //if there are adjacent spaces
                if (adjacent.Count > 0)
                {

                   //If one of the adjecents is the end then move there
                    if (adjacent.Contains(end))
                    {
                        path.Add(end);
                        break;
                    }
                    //otherwise find the next space in the path
                    else
                    {
                        //for each item in the list compare
                        for (int i=0; i<adjacent.Count-1;i++)
                        {
                            //if the lower index has a lower f value then it is the current space
                            if (adjacent[i].fValue < adjacent[i+1].fValue)
                            {
                                current= adjacent[i];
                            }
                            //if the higher index has a lower f value then it is the current space
                            else if (adjacent[i].fValue > adjacent[i + 1].fValue)
                            {
                                current = adjacent[i+1];
                            }
                            //if they both have the same f value then check the h values
                            else if (adjacent[i].fValue == adjacent[i + 1].fValue)
                            {
                                //if the lower index has a lower h value then it is the current space
                                if (adjacent[i].hValue > adjacent[i + 1].hValue)
                                {
                                    current = adjacent[i];
                                }
                                //if the higher index has a lower h value then it is the current space
                                else if (adjacent[i].hValue < adjacent[i + 1].hValue)
                                {
                                    current = adjacent[i + 1];

                                }
                                //if they both have the same h value then check the x values
                                else if (adjacent[i].hValue == adjacent[i + 1].hValue)
                                {
                                    //if the lower index has a higher x value then it is the current space
                                    if (adjacent[i].xCoord < adjacent[i + 1].xCoord)
                                    {
                                        current = adjacent[i+1];
                                    }
                                    //if the higher index has a higher x value then it is the current space
                                    else if (adjacent[i].xCoord > adjacent[i + 1].xCoord)
                                    {
                                        current = adjacent[i];
                                    }
                                    //if they both have the same x value then check the y values
                                    else if (adjacent[i].xCoord == adjacent[i + 1].xCoord)
                                    {
                                        //if the lower index has a higher y value then it is the current space
                                        if (adjacent[i].yCoord < adjacent[i + 1].yCoord)
                                        {
                                            current = adjacent[i + 1];
                                        }
                                        //if the higher index has a higher y value then it is the current space
                                        else if (adjacent[i].yCoord > adjacent[i + 1].yCoord)
                                        {
                                            current = adjacent[i];
                                        }
                                    }
                                }
                            }
                        }
                    }

                }

            }
        }

        //Add all open spaces
        public void PopulateGrid()
        {
            //Iterate for each column
            for (int x = 0; x < GridColumns; x++)
            {
                //Iterate for each row
                for (int y = 0; y < GridRows; y++)
                {
                    bool isWall = false;
                    //make sure that the space is not the start or end
                    if (x != 0 || y != 0 && x != 9 || y != 9)
                    {
                        //make sure that the space is not closed off
                        foreach (Node n in closed)
                        {
                            if (n.xCoord == x && n.yCoord == y)
                            {
                                isWall = true;
                                break;
                            }
                        }
                        //if not closed off then add to grid
                        if (!isWall)
                        {
                            if (x == 0 && y == 0 || x == 9 && y == 9)
                                continue;

                            open.Add(new Node(x, y));
                        }
                    }
                }
            }
        }


        //get the adjacent nodes to the current one
        public void FindSurrounding(Node n)
        {
            //clear the list
            adjacent.Clear();
            foreach (Node nd in open)
            {
                //Right
                if (n.xCoord + 1 == nd.xCoord && n.yCoord == nd.yCoord)
                {
                    //the current is the new parent
                    nd.parent = n;
                    //calculate the g,h,f values for the cardinal directions
                    CalculateValuesHorizontalVertical(nd);
                    //Add the space to the adjacent list
                    adjacent.Add(nd);
                }
                //Left
                if (n.xCoord - 1 == nd.xCoord && n.yCoord == nd.yCoord)
                {
                    //the current is the new parent
                    nd.parent = n;
                    //calculate the g,h,f values for the cardinal directions
                    CalculateValuesHorizontalVertical(nd);
                    //Add the space to the adjacent list
                    adjacent.Add(nd);
                }
                //Up
                if (n.xCoord == nd.xCoord && n.yCoord - 1 == nd.yCoord)
                {
                    //the current is the new parent
                    nd.parent = n;
                    //calculate the g,h,f values for the cardinal directions
                    CalculateValuesHorizontalVertical(nd);
                    //Add the space to the adjacent list
                    adjacent.Add(nd);
                }
                //Down
                if (n.xCoord == nd.xCoord && n.yCoord + 1 == nd.yCoord)
                {
                    //the current is the new parent
                    nd.parent = n;
                    //calculate the g,h,f values for the cardinal directions
                    CalculateValuesHorizontalVertical(nd);
                    //Add the space to the adjacent list
                    adjacent.Add(nd);
                }
                //Up-Left
                if (n.xCoord - 1 == nd.xCoord && n.yCoord - 1 == nd.yCoord)
                {
                    //the current is the new parent
                    nd.parent = n;
                    //calculate the g,h,f values for the diagonal directions
                    CalculateValuesDiagonal(nd);
                    //Add the space to the adjacent list
                    adjacent.Add(nd);
                }
                //Up-Right
                if (n.xCoord + 1 == nd.xCoord && n.yCoord - 1 == nd.yCoord)
                {
                    //the current is the new parent
                    nd.parent = n;
                    //calculate the g,h,f values for the diagonal directions
                    CalculateValuesDiagonal(nd);
                    //Add the space to the adjacent list
                    adjacent.Add(nd);
                }
                //Down-Left
                if (n.xCoord - 1 == nd.xCoord && n.yCoord + 1 == nd.yCoord)
                {
                    //the current is the new parent
                    nd.parent = n;
                    //calculate the g,h,f values for the diagonal directions
                    CalculateValuesDiagonal(nd);
                    //Add the space to the adjacent list
                    adjacent.Add(nd);
                }
                //Down-Right
                if (n.xCoord + 1 == nd.xCoord && n.yCoord + 1 == nd.yCoord)
                {
                    //the current is the new parent
                    nd.parent = n;
                    //calculate the g,h,f values for the diagonal directions
                    CalculateValuesDiagonal(nd);
                    //Add the space to the adjacent list
                    adjacent.Add(nd);
                }
            }

        }

        //Calculate the values for the cardinal directions
        public void CalculateValuesHorizontalVertical(Node n)
        {

            //get the distance from the end point using the manhattan direction
            n.hValue = Math.Abs(end.xCoord - n.xCoord) + Math.Abs(end.yCoord - n.yCoord);

            //get the distance from the start point
            if (n.parent != null)
                n.gValue = n.parent.gValue + 1;
            else
                n.gValue = 0;

            //calculate the f value by adding g and h
            n.fValue = n.hValue + n.gValue;

        }
        public void CalculateValuesDiagonal(Node n)
        {

            //get the distance from the end point using diagonal direction
            n.hValue = (int)Math.Sqrt(Math.Pow(end.xCoord - n.xCoord, 2) + Math.Pow(end.yCoord - n.yCoord, 2));

            //get the distance from the start point
            n.gValue = n.parent.gValue + 1;

            //calculate the f value by adding g and h
            n.fValue = n.hValue + n.gValue;

        }

    }
}
