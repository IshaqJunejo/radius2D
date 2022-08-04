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

        public void Update(int W, int H, int offset)
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

            if (pos.Y >= H - offset - radius)
            {
                pos.Y = H - offset - radius;
                vel.Y *= elasticity * -1;
            };
            if (pos.X < offset + radius)
            {
                pos.X = offset + radius;
                vel.X *= -1;
            }else if (pos.X >= W - offset - radius)
            {
                pos.X = W - offset - radius;
                vel.X *= -1;
            };

            pos += vel;
            force = new Vector2(0, 0);
        }
        
        public void Draw()
        {
            Raylib.DrawCircleV(pos, radius, Color.RAYWHITE);
        }
    }
}