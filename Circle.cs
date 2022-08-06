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

        public void Update(int W, int H, int offset, List<Circle> circles)
        {
            float gravity = 0.5f;
            float terminalVel = 10.0f;

            foreach (Circle circ in circles)
            {
                if (circ != this)
                {
                    if (Collision.CircleToCircle(this, circ) == true)
                    {
                        Vector2 distance = this.pos - circ.pos;
                        float radiiSum = this.radius + circ.radius;
                        float length = (float ) Math.Sqrt(distance.X * distance.X + distance.Y * distance.Y);
                        float depth = length - (this.radius + circ.radius + 1);
                        Vector2 unit = distance / length;

                        this.pos.X = circ.pos.X + (radiiSum + 1) * unit.X;
                        this.pos.Y = circ.pos.Y + (radiiSum + 1) * unit.Y;

                        //this.vel *= this.elasticity * circ.elasticity;
                        //circ.vel *= this.elasticity * circ.elasticity;

                        this.force -= unit * depth / 2;
                        circ.force += unit * depth / 2;
                    };
                }
            }
            this.force.Y += gravity * this.mass;

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

            if (this.pos.Y >= H - offset - this.radius)
            {
                this.pos.Y = H - offset - this.radius;
                this.vel.Y *= this.elasticity * -1;
            };
            if (this.pos.X < offset + this.radius)
            {
                this.pos.X = offset + this.radius;
                this.vel.X *= this.elasticity * -1;
            }else if (this.pos.X >= W - offset - this.radius)
            {
                this.pos.X = W - offset - this.radius;
                this.vel.X *= this.elasticity * -1;
            };

            this.pos += this.vel * Raylib.GetFrameTime() * 120;
            this.force = new Vector2(0, 0);
        }
        
        public void Draw()
        {
            Raylib.DrawCircleV(this.pos, this.radius, Color.RAYWHITE);
            Raylib.DrawCircleLines((int)this.pos.X, (int)this.pos.Y, this.radius, Color.BLACK);
        }
    }
}