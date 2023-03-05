using System.Numerics;
using Raylib_cs;

namespace Radius2D
{
    public class AABB
    {
        public Vector2 pos;
        public float width;
        public float height;
        private Color shade;

        public AABB(float posX, float posY, float Width, float Height, Color color)
        {
            this.pos = new Vector2(posX, posY);

            this.width = Width;
            this.height = Height;

            this.shade = color;
        }

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

        public void drawBox()
        {
            Raylib.DrawRectangle((int)this.pos.X, (int)this.pos.Y, (int)this.width, (int)this.height, this.shade);
        }

        public void drawBoxC()
        {
            Raylib.DrawRectangle((int)this.pos.X, (int)this.pos.Y, (int)this.width, (int)this.height, Color.WHITE);
        }
    }
}