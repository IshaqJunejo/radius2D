using System.Numerics;
using Raylib_cs;

namespace Radius2D
{
    public class Line
    {
        public Vector2 p;
        public Vector2 q;
        public float length;
        public float angle;

        public void UpdateValues()
        {
            this.length = (float) Math.Sqrt((p.X - q.X) * (p.X - q.X) + (p.Y - q.Y) * (p.Y - q.Y));
            this.angle = (float) Math.Atan2(p.Y - q.Y, p.X - q.X) * 180.0f / 3.14f;
        }

        public void DrawLine()
        {
            Raylib.DrawLineV(this.p, this.q, Color.RAYWHITE);
        }
    }
    
}