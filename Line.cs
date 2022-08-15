using System.Numerics;
using Raylib_cs;

namespace Radius2D
{
    // Class for Lines (Currently not in Use)
    public class Line
    {
        // Public Variables
        public Vector2 p;
        public Vector2 q;
        public float length;
        public float angle;

        // Method to update Values of the Line, it will be used right after initializing (or Updating) the Line
        public void UpdateValues()
        {
            this.length = (float) Math.Sqrt((p.X - q.X) * (p.X - q.X) + (p.Y - q.Y) * (p.Y - q.Y));
            this.angle = (float) Math.Atan2(p.Y - q.Y, p.X - q.X) * 180.0f / 3.14f;
        }

        // Method to Draw the Line
        public void DrawLine()
        {
            Raylib.DrawLineV(this.p, this.q, Color.RAYWHITE);
        }
    }
}