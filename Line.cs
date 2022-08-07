using System.Numerics;
using Raylib_cs;

namespace Radius2D
{
    class Line
    {
        public Vector2 p;
        public Vector2 q;

        public void DrawLine()
        {
            Raylib.DrawLineV(p, q, Color.RAYWHITE);
        }
    }
    
}