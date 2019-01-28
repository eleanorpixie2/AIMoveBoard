using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMoveBoard
{
    public class Agent
    {
        //current location of agent
        public int XLocation { get; set; }

        public int YLocation { get; set; }


        //move attempt success/fail
        public bool moveAttempt;

        //board

        List<Tuple<int, int>> unmovable = new List<Tuple<int, int>>()
        {
            Tuple.Create(1,2),
            Tuple.Create(2,5),
            Tuple.Create(2,6),
            Tuple.Create(2,7),
            Tuple.Create(3,1),
            Tuple.Create(3,2),
            Tuple.Create(3,3),
            Tuple.Create(4,7),
            Tuple.Create(4,8),
            Tuple.Create(4,9),
            Tuple.Create(5,0),
            Tuple.Create(5,1),
            Tuple.Create(5,2),
            Tuple.Create(6,5),
            Tuple.Create(6,6),
            Tuple.Create(8,1),
            Tuple.Create(8,2),
            Tuple.Create(8,3),
            Tuple.Create(8,4),
            Tuple.Create(8,7),
            Tuple.Create(8,8),
            Tuple.Create(8,9)

        };

        public Agent()
        {
            XLocation = 0;
            YLocation = 0;
            moveAttempt = true;
        }


        //pick new move
        public void MoveAgent()
        {
            CheckIfLegalRight();
            if(moveAttempt)
            {
                XLocation++;
            }
            else
            {
                moveAttempt = true;
                CheckIfLegalDown();
                if(moveAttempt)
                {
                    YLocation ++;
                }
                else
                {
                    moveAttempt = true;
                    CheckIfLegalUp();
                    if (moveAttempt)
                    {
                        YLocation --;
                    }
                }
            }
            moveAttempt = true;
           
        }

        //check if move is legal
        public void CheckIfLegalUp()
        {
            if (YLocation == 0)
            {
                moveAttempt = false;
                return;
            }
            else
            {
                foreach (Tuple<int,int> m in unmovable)
                {
                    if(m.Item1==XLocation && m.Item2==YLocation-1)
                    {
                        moveAttempt = false;
                        return;
                    }
                }
            }
        }
        //check if move is legal
        public void CheckIfLegalDown()
        {
            if (YLocation==9)
            {
                moveAttempt = false;
                return;
            }
            else
            {
                foreach (Tuple<int, int> m in unmovable)
                {
                    if (m.Item1 == XLocation && m.Item2 == YLocation+1)
                    {
                        moveAttempt = false;
                        return;
                    }
                }
            }


        }

        //check if legal
        public void CheckIfLegalRight()
        {
            if (XLocation == 9)
            {
                moveAttempt = false;
                return;
            }
            else
            {
                foreach (Tuple<int, int> m in unmovable)
                {
                    if (m.Item1 == XLocation+1 && m.Item2 == YLocation)
                    {
                        moveAttempt = false;
                        return;
                    }
                }
            }

        }

    }
}
