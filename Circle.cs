using System.Numerics;
using Raylib_cs;

namespace Radius2D
{
    public class Circle
    {
        public Vector2 pos;
        public Vector2 vel;
        public Vector2 force;
        public float radius;
        public float mass;
        public float elasticity;

        public void Update(int W, int H)
        {
            float terminalVel = 100.0f;

            this.vel += this.force / this.mass * Raylib.GetFrameTime() * 120;

            if (this.vel.X >= terminalVel)
            {
                this.vel.X = terminalVel;
            }else if (this.vel.X < -terminalVel)
            {
                this.vel.X = -terminalVel;
            };

            if (this.vel.Y >= terminalVel)
            {
                this.vel.Y = terminalVel;
            }else if (this.vel.Y < -terminalVel)
            {
                this.vel.Y = -terminalVel;
            };

            if (this.pos.X <= this.radius)
            {
                this.pos.X = this.radius;
                this.vel.X *= -1;
            }else if (this.pos.X >= W - this.radius)
            {
                this.pos.X = W - this.radius;
                this.vel.X *= -1;
            };

            if (this.pos.Y <= this.radius)
            {
                this.pos.Y = this.radius;
                this.vel.Y *= -1;
            }else if (this.pos.Y >= H - this.radius)
            {
                this.pos.Y = H - this.radius;
                this.vel.Y *= -1;
            };

            this.pos += this.vel * Raylib.GetFrameTime() * 120;
            this.force = new Vector2(0, 0);
        }

        public void CollisionResponse(List<Circle> circles)
        {
            foreach (Circle circ in circles)
            {
                if (circ != this)
                {
                    if (Collision.CircleToCircle(this, circ))
                    {
                        // Collision Response Over Here
                    };
                }
            }
        }
        
        public void Draw()
        {
            Raylib.DrawCircleV(this.pos, this.radius, Color.RAYWHITE);
            Raylib.DrawCircleLines((int)this.pos.X, (int)this.pos.Y, this.radius, Color.BLACK);
        }
    }
}