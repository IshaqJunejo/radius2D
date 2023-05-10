using System.Numerics;
using Raylib_cs;

namespace Radius2D
{
    public class Polygon
    {
        public Vector2[] ReferencePositions;
        public Vector2 centrePos;
        public float angle;
        public Vector2[] UpdatedPositions;
        public Vector2 vel;
        public Vector2 force;
        public float mass;
        public float inverseMass;
        public float elasticity;

        // Constructor
        public Polygon(float centerX, float centerY, int NumOfVertices, float radie, float mass, float angle)
        {
            this.centrePos = new Vector2(centerX, centerY);

            // Setting Up Reference Positions
            this.ReferencePositions = new Vector2[NumOfVertices];
            for (var i = 0; i < NumOfVertices; i++)
            {
                this.ReferencePositions[i] = new Vector2((float)Math.Cos(2 * Math.PI / NumOfVertices * i) * radie, (float)Math.Sin(2 * Math.PI / NumOfVertices * i) * radie);
            }
            // Angle
            this.angle = (float) (Math.PI / -2.0);

            // Setting Up Updated Positions
            this.UpdatedPositions = new Vector2[NumOfVertices];
            for (var i = 0; i < NumOfVertices; i++)
            {
                this.UpdatedPositions[i] = new Vector2(centrePos.X + ReferencePositions[i].X, centrePos.Y + ReferencePositions[i].Y);
            }

            this.mass = mass;
            if (mass == 0.0f)
            {
                this.inverseMass = 0.0f;
            }else
            {
                this.inverseMass = 1 / mass;
            }

            this.elasticity = 0.8f;
        }

        // Update Function
        public void Update(float deltaTime)
        {
            // Initializing Terminal(Maximum) Velocity
            float terminalVel = 7.5f;

            // Gravity
            this.force.Y += 0.1f * this.mass;

            // Updating Velocity using Force
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

            // Updating Position using Rotation Transform Matrix
            for (var i = 0; i < this.UpdatedPositions.Length; i++)
            {
                this.UpdatedPositions[i].X = (this.ReferencePositions[i].X * (float)Math.Cos(this.angle)) - (this.ReferencePositions[i].Y * (float)Math.Sin(this.angle)) + this.centrePos.X;
                this.UpdatedPositions[i].Y = (this.ReferencePositions[i].X * (float)Math.Sin(this.angle)) + (this.ReferencePositions[i].Y * (float)Math.Cos(this.angle)) + this.centrePos.Y;
            }

            this.centrePos += this.vel * deltaTime * 60;
            this.force = new Vector2(0, 0);
        }

        // Method for Responding to Collisions
        public void CollisionResponsePolygon(Polygon poly, float deltaTime)
        {
            if (this != poly)
            {
                if (Collision.PolygonToPolygon(this, poly) > 0.0f)
                {
                    // Executing Penetration
                    Vector2 d = poly.centrePos - this.centrePos;
                    float magnitude = (float)Math.Sqrt(d.X * d.X + d.Y * d.Y);
                    Vector2 normal = d / magnitude;

                    this.centrePos -= Collision.PolygonToPolygon(this, poly) * normal / 2 * deltaTime * 60 * this.inverseMass / (this.inverseMass + poly.inverseMass);
                    poly.centrePos += Collision.PolygonToPolygon(this, poly) * normal / 2 * deltaTime * 60 * poly.inverseMass / (this.inverseMass + poly.inverseMass);

                    // Calculating Repulsion
                    float productOfElasticity = this.elasticity * poly.elasticity;
                    float ratioOfMass = this.mass / poly.mass;
                    Vector2 relativeVelocity = this.vel - poly.vel;
                    float seperatingVelocity = Vector2.Dot(normal, relativeVelocity);
                    float newSeperatingVelocity = seperatingVelocity * -1 * productOfElasticity;

                    float seperatingVelocityDifference = newSeperatingVelocity - seperatingVelocity;
                    float impulse = seperatingVelocityDifference / (this.inverseMass + poly.inverseMass);
                    Vector2 impulseVector = impulse * normal;

                    // Executing Repulsion
                    this.vel += impulseVector * this.inverseMass * deltaTime * 60;
                    poly.vel -= impulseVector * this.inverseMass * deltaTime * 60;
                }
            }
        }

        public void CollisionResponseCircle(Circle circ, float deltaTime)
        {
            if (Collision.PolygonToCircle(this, circ) > 0.0f)
            {
                // Executing Penetration
                Vector2 d = circ.pos - this.centrePos;
                float magnitude = (float)Math.Sqrt(d.X * d.X + d.Y * d.Y);
                Vector2 normal = d / magnitude;

                this.centrePos -= Collision.PolygonToCircle(this, circ) * normal / 2 * deltaTime * 60 * this.inverseMass / (this.inverseMass + circ.inverseMass);
                circ.pos += Collision.PolygonToCircle(this, circ) * normal / 2 * deltaTime * 60 * circ.inverseMass / (this.inverseMass + circ.inverseMass);

                // Calculating Repulsion
                float productOfElasticity = this.elasticity * circ.elasticity;
                float ratioOfMass = this.mass / circ.mass;
                Vector2 relativeVelocity = this.vel - circ.vel;
                float seperatingVelocity = Vector2.Dot(normal, relativeVelocity);
                float newSeperatingVelocity = seperatingVelocity * -1 * productOfElasticity;

                float seperatingVelocityDifference = newSeperatingVelocity - seperatingVelocity;
                float impulse = seperatingVelocityDifference / (this.inverseMass + circ.inverseMass);
                Vector2 impulseVector = impulse * normal;

                // Executing Repulsion
                this.vel += impulseVector * this.inverseMass * deltaTime * 60;
                circ.vel -= impulseVector * circ.inverseMass * deltaTime * 60;
            }
        }

        // Draw Method
        public void Draw()
        {
            // Draw Line from Centre to Vertex
            Raylib.DrawLine((int)this.centrePos.X, (int)this.centrePos.Y, (int)(this.UpdatedPositions[0].X), (int)(this.UpdatedPositions[0].Y), Color.WHITE);
            // Drawing Lines from Vertex to Vertex
            for (var i = 0; i < this.ReferencePositions.Length; i++)
            {
                Raylib.DrawLine((int)(this.UpdatedPositions[i].X), (int)(this.UpdatedPositions[i].Y), (int)(this.UpdatedPositions[(i+1) % this.UpdatedPositions.Length].X), (int)(this.UpdatedPositions[(i+1) % this.UpdatedPositions.Length].Y), Color.WHITE);
            }
        }
    }
}