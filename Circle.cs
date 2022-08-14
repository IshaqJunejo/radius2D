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
        private Color tint;

        public void Update(int W, int H)
        {
            float terminalVel = 100.0f;
            //this.force.Y += 0.02f * this.mass;

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
                this.vel.Y *= -0.3f;
            };

            this.pos += this.vel * Raylib.GetFrameTime() * 120;
            this.force = new Vector2(0, 0);
            tint = Color.RAYWHITE;
        }

        public void CollisionResponse(Circle circ)
        {
            if (circ.pos != this.pos)
            {
                if (Collision.CircleToCircle(this, circ))
                {
                    // Penetration Over Here
                    Vector2 distance = this.pos - circ.pos;
                    float length = (float) Math.Sqrt(distance.X * distance.X + distance.Y * distance.Y);
                    Vector2 normal = distance / length;

                    float depth = (this.radius + circ.radius) - length;
                    Vector2 pentrateResolve = normal * depth / 2;

                    this.pos += pentrateResolve;
                    circ.pos -= pentrateResolve;

                    // Collision Response Over Here
                    Vector2 relativeVelocity = this.vel - circ.vel;
                    float seperatingVelocity = Vector2.Dot(normal, relativeVelocity) * -1;

                    Vector2 seperatingVelocityVector = seperatingVelocity * normal;

                    this.vel += seperatingVelocityVector;
                    circ.vel -= seperatingVelocityVector;
                };
            }
        }
        
        public void Draw()
        {
            Raylib.DrawCircleV(this.pos, this.radius, tint);
            Raylib.DrawCircleLines((int)this.pos.X, (int)this.pos.Y, this.radius, Color.WHITE);
        }
    }
}