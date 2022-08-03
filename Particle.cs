using System.Numerics;
using Raylib_cs;

namespace Radius2D
{
    public class Particle
    {
        public Vector2 pos;
        public Vector2 vel;
        public Vector2 force;
        public float radius;
        public float mass;
        public float elasticity;

        public void Draw()
        {
            Raylib.DrawCircleV(pos, radius, Color.RAYWHITE);
        }
    }
}