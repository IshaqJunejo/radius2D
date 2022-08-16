using System.Numerics;
using Raylib_cs;

namespace Radius2D
{
    // Class for Balls/Circles
    public class Circle
    {
        // Public Variables for Usage including Position, Velocity, Force and few more
        public Vector2 pos;
        public Vector2 vel;
        public Vector2 force;
        public float radius;
        public float mass;
        public float elasticity;

        // Update Method for Balls/Circles
        public void Update(int W, int H)
        {
            // Initializing Terminal(Maximum) Velocity
            float terminalVel = 100.0f;

            // Gravity
            this.force.Y += 0.2f * this.mass;

            // Updating the Velocity using the Force
            this.vel += this.force / this.mass * Raylib.GetFrameTime() * 120;

            // Checking if Horizontal Velocity is in the Terminal Velocity
            if (this.vel.X >= terminalVel)
            {
                this.vel.X = terminalVel;
            }else if (this.vel.X < -terminalVel)
            {
                this.vel.X = -terminalVel;
            };

            // Checking if Vertical Velocity is in the Terminal Velocity
            if (this.vel.Y >= terminalVel)
            {
                this.vel.Y = terminalVel;
            }else if (this.vel.Y < -terminalVel)
            {
                this.vel.Y = -terminalVel;
            };

            // Looking if the Balls/Circles are out of the Screen's X axis
            if (this.pos.X <=  0 + this.radius)
            {
                this.pos.X = this.radius;
                this.vel.X *= this.elasticity * -1;
            }else if (this.pos.X >= W - this.radius)
            {
                this.pos.X = W - this.radius;
                this.vel.X *= this.elasticity * -1;
            };

            // Looking if the Balls/Circles are out of the Screen's Y axis
            if (this.pos.Y <= 0 + this.radius)
            {
                this.pos.Y = this.radius + 1;
                this.vel.Y *= this.elasticity * -1;
            }else if (this.pos.Y >= H - this.radius)
            {
                this.pos.Y = H - this.radius;
                this.vel.Y *= this.elasticity * -1;
            };

            // Updating the Ball's/Circle's Position using the Velocity
            this.pos += this.vel * Raylib.GetFrameTime() * 120;

            // Renewing the Force Vector
            this.force = new Vector2(0, 0);
        }

        // Method for responding to the Collisions
        public void CollisionResponseCircle(Circle circ)
        {
            // Checking if both the Balls/Circles are not the same
            if (circ.pos != this.pos)
            {
                // Checking if the Balls/Circles are overlapping or not
                if (Collision.CircleToCircle(this, circ))
                {
                    // Penetration 
                    Vector2 distance = this.pos - circ.pos;
                    float length = (float) Math.Sqrt(distance.X * distance.X + distance.Y * distance.Y);
                    Vector2 normal = distance / length;

                    float depth = (this.radius + circ.radius) - length;
                    Vector2 pentrateResolve = normal * depth / 2;

                    this.pos += pentrateResolve;
                    circ.pos -= pentrateResolve;

                    // Repulsion
                    float productOfElasticity = this.elasticity * circ.elasticity;
                    float ratioOfMass = this.mass / circ.mass;
                    Vector2 relativeVelocity = this.vel - circ.vel;
                    float seperatingVelocity = Vector2.Dot(normal, relativeVelocity) * -1;

                    Vector2 seperatingVelocityVector = seperatingVelocity * normal;

                    this.vel += seperatingVelocityVector * productOfElasticity;
                    circ.vel -= seperatingVelocityVector * productOfElasticity;
                };
            }
        }
        public void CollisionResponseLine(Line l)
        {
            if (Collision.CircleToLine(l, this, this.pos) < this.radius)
            {
                double ang01 = Math.Atan2(l.p.Y - this.pos.Y, l.p.X - this.pos.X);

                float depth = this.radius - Collision.CircleToLine(l, this, this.pos);

                if (ang01 - l.angle > 0)
                {
                    this.pos.X += depth * (float) Math.Cos(l.angle - (90 * Math.PI / 180));
                    this.pos.Y += depth * (float) Math.Sin(l.angle - (90 * Math.PI / 180));
                    
                    this.vel.X += depth * (float) Math.Cos(l.angle - (90 * Math.PI / 180));
                    this.vel.Y += depth * (float) Math.Sin(l.angle - (90 * Math.PI / 180));
                }else if (ang01 - l.angle < 0)
                {

                    this.pos.X += depth * (float) Math.Cos(l.angle + (90 * Math.PI / 180));
                    this.pos.Y += depth * (float) Math.Sin(l.angle + (90 * Math.PI / 180));

                    this.vel.X += depth * (float) Math.Cos(l.angle + (90 * Math.PI / 180));
                    this.vel.Y += depth * (float) Math.Sin(l.angle + (90 * Math.PI / 180));
                }else
                {
                    Console.Write("...");
                }
            }
        }
        
        // Method to draw the Balls/Circles
        public void Draw()
        {
            Raylib.DrawCircleV(this.pos, this.radius, Color.WHITE);
            Raylib.DrawCircleLines((int)this.pos.X, (int)this.pos.Y, this.radius, Color.WHITE);
        }
    }
}