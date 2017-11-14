using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    enum StateNode
    {
        NotTestedYet,
        Open,
        Closed
    }

    class Node
    {
        Vector pos;
        StateNode state;
        float g;
        float h;
        Node nodeParent;

        public Node(Vector pos, StateNode state)
        {
            this.pos = pos;
            this.state = state;
        }

        public Vector Position => this.pos;

        public Node NodeParent
        {
            get
            {
                return this.nodeParent;
            }
            set
            {
                this.nodeParent = value;
            }
        }

        public StateNode StateNode
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;
            }
        }

        public float G
        {
            get
            {
                return this.g;
            }
            set
            {
                this.g = value;
            }
        }

        public float H
        {
            get
            {
                return this.h;
            }
            set
            {
                this.h = value;
            }
        }

        public float F => this.g + this.h;
    }
}
