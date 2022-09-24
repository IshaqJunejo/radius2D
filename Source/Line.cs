using System.Numerics;
using Raylib_cs;

namespace Radius2D
{
    // Class for Lines
    public class Line
    {
        // Public Variables
        public Vector2 p;
        public Vector2 q;
        public float length;
        public float angle;

        // Method to update Values of the Line, it will be used right after initializing (or Updating) the Line
        public Line(float posX1, float posY1, float posX2, float posY2)
        {
            this.p = new Vector2(posX1, posY1);
            this.q = new Vector2(posX2, posY2);

            this.length = (float) Math.Sqrt((this.p.X - this.q.X) * (this.p.X - this.q.X) + (this.p.Y - this.q.Y) * (this.p.Y - this.q.Y));
            this.angle = (float) Math.Atan2(this.p.Y - this.q.Y, this.p.X - this.q.X);
        }

        // Method to Draw the Line
        public void DrawLine()
        {
            Raylib.DrawLineV(this.p, this.q, Color.RAYWHITE);
        }
    }
}