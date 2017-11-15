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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="map">Current map</param>
        public Algorithm(Map map)
        {
            this.map = map;
        }

        /// <summary>
        /// Get adjacent node for set her properties.
        /// </summary>
        /// <param name="entryNode">Center node / Actual node</param>
        /// <returns></returns>
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

        /// <summary>
        /// Get adjacent node for get the path. (need to use the method "GetAdjacentNode" before)
        /// </summary>
        /// <param name="entryNode">Center node / Actual node</param>
        /// <returns></returns>
        public List<Node> GetAdjacentNodeForPath(Node entryNode)
        {
            List<Node> adjacentNode = new List<Node>();

            adjacentNode = this.map.NodeWallkable.Where(n =>
            ((n.Position.X == entryNode.Position.X && n.Position.Y == entryNode.Position.Y - 1) || (n.Position.X == entryNode.Position.X - 1 && n.Position.Y == entryNode.Position.Y)
            ||
            (n.Position.X == entryNode.Position.X + 1 && n.Position.Y == entryNode.Position.Y) || (n.Position.X == entryNode.Position.X && n.Position.Y == entryNode.Position.Y + 1)) && n.StateNode != StateNode.NotTestedYet).ToList();

            return adjacentNode;
        }
        /// <summary>
        /// Calcul the properties for all node in the list.
        /// </summary>
        /// <param name="nodesAdjacent"></param>
        /// <param name="parentNode"></param>
        public void CalculAdjacentNode(List<Node> nodesAdjacent, Node parentNode) => nodesAdjacent.ForEach(n => CalculParameters(n, parentNode));

        /// <summary>
        /// Calcul the properties for the node.
        /// </summary>
        /// <param name="node">Actual node</param>
        /// <param name="parentNode">Previous node</param>
        public void CalculParameters(Node node, Node parentNode)
        {
            node.G = parentNode.G + 1;

            // Manhattan Heuristic
            node.H = Math.Abs(node.Position.X - this.map.EndPoint.Position.X) + Math.Abs(node.Position.Y - this.map.EndPoint.Position.Y);

            // Euclidean Heuristic
            //node.H = (float)(Math.Sqrt(Math.Pow(Math.Abs(node.Position.X - this.map.EndPoint.Position.X), 2) + Math.Pow(Math.Abs(node.Position.Y - this.map.EndPoint.Position.Y),2)));

            node.NodeParent = parentNode;
            node.StateNode = StateNode.Open;
        }

        /// <summary>
        /// Find the good path, starting by the Start node and finish by the Final node.
        /// </summary>
        /// <param name="node">Actual node</param>
        /// <returns></returns>
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

        /// <summary>
        /// Get the good path, starting by the Final node and finish by the Starting node.
        /// </summary>
        /// <returns></returns>
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
                   
                    node = adjacentNode[0];
                    path.Add(node.Position);
                    node.StateNode = StateNode.NotTestedYet;
                }
                path.Reverse();
            }
            return path;
        }
    }
}
