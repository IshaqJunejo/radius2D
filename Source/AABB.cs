using System.Numerics;
using Raylib_cs;

namespace Radius2D
{
    public class AABB
    {
        public Vector2 pos;
        public float width;
        public float height;

        public float getEdgeXMin()
        {
            return this.pos.X;
        }

        public float getEdgeXMax()
        {
            return this.pos.X + this.width;
        }

        public float getEdgeYMin()
        {
            return this.pos.Y;
        }

        public float getEdgeYMax()
        {
            return this.pos.Y + this.height;
        }
    }
}