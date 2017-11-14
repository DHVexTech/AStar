using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    class Algorithm
    {
        Map map;
        public Algorithm(Map map)
        {
            this.map = map;
        }

        public float Pythagore(Node entryPoint, Node endPoint)
        {
            if (endPoint.Position.X == entryPoint.Position.X)
            {
                return (float)Math.Abs(endPoint.Position.Y - entryPoint.Position.Y);
            }
            else if (endPoint.Position.Y == entryPoint.Position.Y)
            {
                return (float)Math.Abs(endPoint.Position.X - entryPoint.Position.X);
            }
            else
            {
                return (float)Math.Sqrt(Math.Abs(Math.Pow(entryPoint.Position.X - endPoint.Position.X, 2)) + Math.Abs(Math.Pow(entryPoint.Position.Y - endPoint.Position.Y, 2)));
            }
        }
        public List<Node> GetAdjacentNode(Node entryNode)
        {
            List<Node> adjacentNode = new List<Node>();

            adjacentNode = this.map.NodeWallkable.Where(n =>
                ((n.Position.X == entryNode.Position.X && n.Position.Y == entryNode.Position.Y - 1) || (n.Position.X == entryNode.Position.X - 1 && n.Position.Y == entryNode.Position.Y)
                ||
                (n.Position.X == entryNode.Position.X + 1 && n.Position.Y == entryNode.Position.Y) || (n.Position.X == entryNode.Position.X && n.Position.Y == entryNode.Position.Y + 1)) && n.StateNode == StateNode.NotTestedYet).ToList();

            CalculAdjacentNode(adjacentNode, entryNode);
            return adjacentNode;
        }

        public List<Node> GetAdjacentNodeForPath(Node entryNode)
        {
            List<Node> adjacentNode = new List<Node>();

            adjacentNode = this.map.NodeWallkable.Where(n =>
            ((n.Position.X == entryNode.Position.X && n.Position.Y == entryNode.Position.Y - 1) || (n.Position.X == entryNode.Position.X - 1 && n.Position.Y == entryNode.Position.Y)
            ||
            (n.Position.X == entryNode.Position.X + 1 && n.Position.Y == entryNode.Position.Y) || (n.Position.X == entryNode.Position.X && n.Position.Y == entryNode.Position.Y + 1)) && n.StateNode != StateNode.NotTestedYet).ToList();

            return adjacentNode;
        }

        public void CalculAdjacentNode(List<Node> nodesAdjacent, Node parentNode) => nodesAdjacent.ForEach(n => CalculParameters(n, parentNode));

        public void CalculParameters(Node node, Node parentNode)
        {
            node.G = parentNode.G + 1;

            // Manhattan Heuristic
            //node.H = Math.Abs(node.Position.X - this.map.EndPoint.Position.X) + Math.Abs(node.Position.Y - this.map.EndPoint.Position.Y);

            // Euclidean Heuristic
            node.H = (float)(Math.Sqrt(Math.Pow(Math.Abs(node.Position.X - this.map.EndPoint.Position.X), 2) + Math.Pow(Math.Abs(node.Position.Y - this.map.EndPoint.Position.Y),2)));

            node.NodeParent = parentNode;
            node.StateNode = StateNode.Open;
        }

        public bool Search(Node node)
        {
            node.StateNode = StateNode.Closed;
            List<Node> adjacentNode = GetAdjacentNode(node);
            adjacentNode.Sort((node1, node2) => node1.F.CompareTo(node2.F));
            foreach(Node n in adjacentNode)
            {
                if (n.H == 0)
                {
                    return true;
                }
                else
                {
                    if (Search(n))
                        return true;
                }
            }
            return false;
        }

        public List<Vector> FindPath()
        {
            List<Vector> path = new List<Vector>();
            bool success = Search(this.map.StartPoint);
            if (success)
            {
                Node node = this.map.EndPoint;
                while (node.Position.X != this.map.StartPoint.Position.X || node.Position.Y != this.map.StartPoint.Position.Y)
                {
                    List<Node> adjacentNode = GetAdjacentNodeForPath(node);
                    adjacentNode.Sort((n1, n2) => n1.G.CompareTo(n2.G));
                    try
                    {
                        node = adjacentNode[0];
                        path.Add(node.Position);
                        node.StateNode = StateNode.NotTestedYet;
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        Console.WriteLine(e.ToString());
                        break;
                    }
                }
                path.Reverse();
            }
            return path;
        }
    }
}
