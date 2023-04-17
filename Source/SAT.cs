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
        private bool player;
        public Vector2 vel;
        public Vector2 force;
        public float mass;
        public float inverseMass;

        // Constructor
        public Polygon(float centerX, float centerY, int NumOfVertices, float radie, float mass, bool isItPlayer)
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

            // Player Flag
            this.player = isItPlayer;
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

            // Keyboard Inputs to move the polygons
            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) && this.player)
            {
                this.angle -= 0.75f * deltaTime;
            }
            else if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT) && this.player)
            {
                this.angle += 0.75f * deltaTime;
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_UP) && this.player)
            {
                this.centrePos.X += (float) Math.Cos(this.angle) * 2.0f;
                this.centrePos.Y += (float) Math.Sin(this.angle) * 2.0f;
            }
            else if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN) && this.player)
            {
                this.centrePos.X -= (float) Math.Cos(this.angle) * 2.0f;
                this.centrePos.Y -= (float) Math.Sin(this.angle) * 2.0f;
            }

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
                    Vector2 d = poly.centrePos - this.centrePos;
                    float magnitude = (float)Math.Sqrt(d.X * d.X + d.Y * d.Y);

                    this.centrePos -= Collision.PolygonToPolygon(this, poly) * d / magnitude / 2 * deltaTime * 60;
                    poly.centrePos += Collision.PolygonToPolygon(this, poly) * d / magnitude / 2 * deltaTime * 60;
                }
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