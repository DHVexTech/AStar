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
                Map map = new Map(mapLength);                           // Create map
                Algorithm algo = new Algorithm(map);                    // Start algo

                List<Vector> path = new List<Vector>();                 // Create the list
                path = algo.FindPath();                                 // and get the path

                map.ConstructMap(path);                                 // Show the map in console

                        
                for (int i = 0; i < 7; i++)                             // Separe different map
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
