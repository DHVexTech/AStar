using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    class Map
    {
        int length;
        Random rdm;
        Node startPoint;
        Node endPoint;
        List<Vector> walls;
        List<Node> nodeWallkable;

        /// <summary>
        /// Constructor for the map.
        /// Create the starting, ending node and walls 
        /// </summary>
        /// <param name="length"></param>
        public Map(int length)
        {
            rdm = new Random();
            startPoint = new Node(CreatePoint(TypeOfPoint.Start, length), StateNode.NotTestedYet);
            endPoint = new Node(CreatePoint(TypeOfPoint.End, length), StateNode.NotTestedYet);
            nodeWallkable = new List<Node>();
            walls = new List<Vector>();
            for (int w = 0; w < length * 5; w++)
            {
                walls.Add(CreatePoint(TypeOfPoint.Wall, length));
            }

            this.length = length;
            CreatePoint();
        }

        public List<Node> NodeWallkable => this.nodeWallkable;

        public Node EndPoint => this.endPoint;

        public Node StartPoint => this.startPoint;

        /// <summary>
        /// Create all nodes and add to the list.
        /// </summary>
        public void CreatePoint()
        {
            for (int y = 0; y < length; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    if (CheckPos(startPoint.Position, x, y))
                    {
                        nodeWallkable.Add(startPoint);
                    }
                    else if (CheckPos(endPoint.Position, x, y))
                    {
                        nodeWallkable.Add(endPoint);
                    }
                    else if (!(walls.Where(n => n.X == x && n.Y == y).Any()/*from w in walls where w.X == x && w.Y == y select w).Any()*/))
                    {
                        nodeWallkable.Add(new Node(new Vector(x, y), StateNode.NotTestedYet));
                    }
                }
            }
        }

        /// <summary>
        /// Boucle for create the map.
        /// </summary>
        /// <param name="path">The sortest path</param>
        public void ConstructMap(List<Vector> path)
        {

            for (int y = 0; y < length; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    DrawingMap(x, y, path);
                }
                Console.WriteLine("");
            }
        }

        /// <summary>
        /// Create the Start/End node randomly.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public Vector CreatePoint(TypeOfPoint type, int max) => new Vector(rdm.Next(0, max), rdm.Next(0, max), type);

        public bool CheckPos(Vector subject, int x, int y) => subject.X == x && subject.Y == y; 

        public void DrawingMap(int x, int y, List<Vector> path)
        {
            if (CheckPos(startPoint.Position, x, y))
            {
                Console.Write("S ");
            }
            else if (CheckPos(endPoint.Position, x, y))
            {
                Console.Write("F ");
            }
            else if ((from p in path where p.X == x && p.Y == y select p).Any())
            {
                Console.Write("¤ ");
            }
            else if ((from c in walls where c.X == x && c.Y == y select c).Any())
            {
                Console.Write("  ");
            }
            else
            {
                Console.Write(": ");
            }
        }
    }

    enum TypeOfPoint
    {
        Start,
        End,
        Wall,
        Null
    }
}
