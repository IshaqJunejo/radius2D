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
        public float inverseMass;
        public float elasticity;
        public Color shade;

        public Circle(float posX, float posY, float velX, float velY, float radius, float mass, float elasticity, Color tint)
        {
            this.pos = new Vector2(posX, posY);
            this.vel = new Vector2(velX, velY);

            this.force = new Vector2(0, 0);

            this.radius = radius;
            //this.mass = (float) Math.Pow(this.radius, 2) / 2.0f;
            this.mass = mass;
            if (this.mass == 0)
            {
                this.inverseMass = 0;
            }else
            {
                this.inverseMass = 1 / this.mass;
            };
            this.elasticity = elasticity;
            this.shade = tint;
        }

        // Update Method for Balls/Circles
        public void Update(int W, int H, float deltaTime)
        {
            // Initializing Terminal(Maximum) Velocity
            float terminalVel = 7.5f;

            // Gravity
            this.force.Y += 0.1f * this.mass;

            // Updating the Velocity using the Force
            this.vel += this.force * this.inverseMass * deltaTime * 60;

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

            // Updating the Ball's/Circle's Position using the Velocity
            this.pos += this.vel * deltaTime * 60 * this.mass * this.inverseMass;

            // Renewing the Force Vector
            this.force = new Vector2(0, 0);
        }

        // Method for responding to the Collisions
        public void CollisionResponseCircle(Circle circ, float deltaTime)
        {
            // Checking if both the Balls/Circles are not the same
            if (circ.pos != this.pos)
            {
                // Checking if the Balls/Circles are overlapping or not
                if (Collision.CircleToCircle(this, circ))
                {
                    // Calculating Penetration 
                    Vector2 distance = this.pos - circ.pos;
                    float length = (float) Math.Sqrt(distance.X * distance.X + distance.Y * distance.Y);
                    Vector2 normal = distance / length;

                    float depth = (this.radius + circ.radius) - length;
                    Vector2 pentrateResolve = normal * depth / 2;

                    // Executing Penetration
                    this.pos += pentrateResolve;
                    circ.pos -= pentrateResolve;

                    // Calculating Repulsion
                    float productOfElasticity = this.elasticity * circ.elasticity;
                    float ratioOfMass = this.mass / circ.mass;
                    Vector2 relativeVelocity = this.vel - circ.vel;
                    float seperatingVelocity = Vector2.Dot(normal, relativeVelocity);
                    float newSeperatingVelocity = seperatingVelocity * -1 * productOfElasticity;

                    //Vector2 seperatingVelocityVector = newSeperatingVelocity * normal;
                    float seperatingVelocityDifference = newSeperatingVelocity - seperatingVelocity;
                    float impulse = seperatingVelocityDifference / (this.inverseMass + circ.inverseMass);
                    Vector2 impulseVector = impulse * normal;

                    // Executing Repulsion
                    this.vel += impulseVector * this.inverseMass * deltaTime * 60;
                    circ.vel -= impulseVector * circ.inverseMass * deltaTime * 60;
                };
            }
        }

        // Method for Responding to the Collision between Circle and Line
        public void CollisionResponseLine(Line l, float deltaTime)
        {
            // Checking for Collision activity
            if (Collision.CircleToLine(l, this) < this.radius)
            {
                // Calculating that Circle is lying on line's which side
                double ang01 = Math.Atan2(l.p.Y - this.pos.Y, l.p.X - this.pos.X);

                float depth = this.radius - Collision.CircleToLine(l, this);

                if (ang01 - l.angle > 0)
                {
                    this.pos.X += depth * (float) Math.Cos(l.angle - (90 * Math.PI / 180));
                    this.pos.Y += depth * (float) Math.Sin(l.angle - (90 * Math.PI / 180));
                    
                    this.vel.X += depth * (float) Math.Cos(l.angle - (90 * Math.PI / 180)) * this.elasticity * deltaTime * 60;
                    this.vel.Y += depth * (float) Math.Sin(l.angle - (90 * Math.PI / 180)) * this.elasticity * deltaTime * 60;
                }else if (ang01 - l.angle < 0)
                {

                    this.pos.X += depth * (float) Math.Cos(l.angle + (90 * Math.PI / 180));
                    this.pos.Y += depth * (float) Math.Sin(l.angle + (90 * Math.PI / 180));

                    this.vel.X += depth * (float) Math.Cos(l.angle + (90 * Math.PI / 180)) * this.elasticity * deltaTime * 60;
                    this.vel.Y += depth * (float) Math.Sin(l.angle + (90 * Math.PI / 180)) * this.elasticity * deltaTime * 60;
                }else
                {
                    Console.Write("...");
                }
            }
        }
        
        // Method to draw the Balls/Circles
        public void Draw()
        {
            Raylib.DrawCircleV(this.pos, this.radius, this.shade);
        }
    }
}