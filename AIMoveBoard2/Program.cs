using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMoveBoard2
{
    class Program
    {
        static void Main(string[] args)
        {
            Agent a = new Agent();
            a.MoveAgent();
            foreach(Node n in a.path)
            {
                Console.WriteLine("{0},{1}", n.xCoord, n.yCoord);
                Console.WriteLine("G-{0},H-{1},F-{2}\n", n.gValue, n.hValue,n.fValue);
            }
            Console.ReadLine();
        }
    }
}
