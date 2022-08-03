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

        public void Update()
        {
            float gravity = 0.5f;
            float terminalVel = 15.0f;

            force.Y += gravity * mass;

            vel += force / mass;

            if (vel.X >= terminalVel)
            {
                vel.X = terminalVel;
            }else if (vel.X < -terminalVel)
            {
                vel.X = -terminalVel;
            };

            if (vel.Y >= terminalVel)
            {
                vel.Y = terminalVel;
            }else if (vel.Y < -terminalVel)
            {
                vel.Y = -terminalVel;
            };

            pos += vel;
        }
    }
}