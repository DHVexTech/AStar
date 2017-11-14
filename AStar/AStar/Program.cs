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
            int mapLength = 10;
            while (Console.ReadLine() != null)
            {
                Map map = new Map(mapLength);
                Algorithm algo = new Algorithm(map);
                List<Vector> path = new List<Vector>();
                path = algo.FindPath();
                map.ConstructMap(path);

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
