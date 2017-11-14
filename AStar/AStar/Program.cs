using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    class Program
    {
        static void Main(string[] args)
        {
            int mapLength = 7;
            while (Console.ReadLine() != null)
            {
                Map map = new Map(mapLength);
                Algorithm algo = new Algorithm(map);
                List<Vector> path = new List<Vector>();
                path = algo.FindPath();
                List<Node> node;
                map.ConstructMap(path);
                //Console.WriteLine("(StartNode) X : " + map.StartPoint.Position.X + " --- Y : " + map.StartPoint.Position.Y);
                //Console.WriteLine("(EndNode) X : " + map.EndPoint.Position.X + " --- Y : " + map.EndPoint.Position.Y);
                //path.ForEach(v => Console.WriteLine("x = " + v.X + " ////// y = " + v.Y));

                //map.NodeWallkable.ForEach(n => Console.WriteLine("F = " + n.F + " --- G = " + n.G + " --- H = " + n.H + " --- StateNode = " + n.StateNode + " --- Position.X = " + n.Position.X + " --- Position.Y = " + n.Position.Y));
                for (int i = 0; i < mapLength; i++)
                {
                    for (int z = 0; z < mapLength; z++)
                    {
                        if ((node = (from c in map.NodeWallkable where c.Position.Y == i && c.Position.X == z select c).ToList()).Count != 0)
                        {
                            Console.Write("    [" + node[0].G + "," + node[0].H + "," + node[0].StateNode + "]    ");
                        }
                        else
                        {
                            Console.Write("    wall     ");
                        }
                    }
                    Console.WriteLine();
                }

                //Console.WriteLine("[G,H,State]");
#if DEBUG
                Console.WriteLine("Mode=Debug");
#else
    Console.WriteLine("Mode=Release"); 
#endif

                for (int i = 0; i < 7; i++)
                {
                    Console.WriteLine();
                    for (int o = 0; o < 70; o++)
                    {
                        Console.Write("XX");
                    }
                }
                Console.WriteLine();
            }

        }
    }
}
