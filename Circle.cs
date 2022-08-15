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
            this.force.Y += 0.02f * this.mass;

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
                this.vel.X *= -1;
            }else if (this.pos.X >= W - this.radius)
            {
                this.pos.X = W - this.radius;
                this.vel.X *= -1;
            };

            // Looking if the Balls/Circles are out of the Screen's Y axis
            if (this.pos.Y <= 0 + this.radius)
            {
                this.pos.Y = this.radius;
                this.vel.Y *= -1;
            }else if (this.pos.Y >= H - this.radius)
            {
                this.pos.Y = H - this.radius;
                this.vel.Y *= -0.3f;
            };

            // Updating the Ball's/Circle's Position using the Velocity
            this.pos += this.vel * Raylib.GetFrameTime() * 120;

            // Renewing the Force Vector
            this.force = new Vector2(0, 0);
        }

        // Method for responding to the Collisions
        public void CollisionResponse(Circle circ)
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
                    Vector2 relativeVelocity = this.vel - circ.vel;
                    float seperatingVelocity = Vector2.Dot(normal, relativeVelocity) * -1;

                    Vector2 seperatingVelocityVector = seperatingVelocity * normal;

                    this.vel += seperatingVelocityVector;
                    circ.vel -= seperatingVelocityVector;
                };
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