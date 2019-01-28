using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMoveBoard
{
    class Program
    {
        static void Main(string[] args)
        {
            Agent a = new Agent();
            while(a.XLocation!=9 || a.YLocation!=9)
            {
                a.MoveAgent();
                Console.WriteLine(a.XLocation.ToString()+","+ a.YLocation.ToString());

                Console.ReadLine();
            }

            Console.WriteLine("Reached the goal point!");
            Console.ReadLine();
        }
    }
}
