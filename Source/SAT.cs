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
        public bool overlap;
        /*public Vector2 vel;
        public Vector2 force;
        public float mass;
        public float inverseMass;*/
        public Polygon(float centerX, float centerY, int NumOfVertices, float radie, bool isItPlayer)
        {
            this.centrePos = new Vector2(centerX, centerY);
            this.ReferencePositions = new Vector2[NumOfVertices];
            for (var i = 0; i < NumOfVertices; i++)
            {
                this.ReferencePositions[i] = new Vector2((float)Math.Cos(2 * Math.PI / NumOfVertices * i) * radie, (float)Math.Sin(2 * Math.PI / NumOfVertices * i) * radie);
            }
            this.angle = (float) (Math.PI / -2.0);
            this.UpdatedPositions = new Vector2[NumOfVertices];
            for (var i = 0; i < NumOfVertices; i++)
            {
                this.UpdatedPositions[i] = new Vector2(centrePos.X + ReferencePositions[i].X, centrePos.Y + ReferencePositions[i].Y);
            }

            this.player = isItPlayer;
        }

        public void Update(float deltaTime)
        {
            overlap = false;
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

            if (Raylib.IsKeyDown(KeyboardKey.KEY_A) && this.player == false)
            {
                this.angle -= 0.75f * deltaTime;
            }
            else if (Raylib.IsKeyDown(KeyboardKey.KEY_D) && this.player == false)
            {
                this.angle += 0.75f * deltaTime;
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_W) && this.player == false)
            {
                this.centrePos.X += (float) Math.Cos(this.angle) * 2.0f;
                this.centrePos.Y += (float) Math.Sin(this.angle) * 2.0f;
            }
            else if (Raylib.IsKeyDown(KeyboardKey.KEY_S) && this.player == false)
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
        }

        public void Draw(Color color)
        {
            Raylib.DrawLine((int)this.centrePos.X, (int)this.centrePos.Y, (int)(this.UpdatedPositions[0].X), (int)(this.UpdatedPositions[0].Y), color);
            for (var i = 0; i < this.ReferencePositions.Length; i++)
            {
                Raylib.DrawLine((int)(this.UpdatedPositions[i].X), (int)(this.UpdatedPositions[i].Y), (int)(this.UpdatedPositions[(i+1) % this.UpdatedPositions.Length].X), (int)(this.UpdatedPositions[(i+1) % this.UpdatedPositions.Length].Y), color);
            }
        }
    }
}