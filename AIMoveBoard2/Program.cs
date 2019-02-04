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
            while(a.open[0].xCoord!=a.end.xCoord || a.open[0].yCoord != a.end.yCoord)
            {
                a.MoveAgent();
                Console.WriteLine("{0},{1}", a.open[0].xCoord, a.open[0].yCoord);
            }
            Console.ReadLine();
        }
    }
}
