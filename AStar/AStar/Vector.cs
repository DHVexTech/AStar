using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    class Vector
    {
        int x;
        int y;
        TypeOfPoint typePoint;

        public Vector(int x, int y, TypeOfPoint typePoint)
        {
            this.typePoint = typePoint;
            this.x = x;
            this.y = y;
        }

        public Vector(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.typePoint = TypeOfPoint.Null;
        }

        public int X => this.x;

        public int Y => this.y;

        public TypeOfPoint TypePoint => this.typePoint;
    }
}
