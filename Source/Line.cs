using System.Numerics;
using Raylib_cs;

namespace Radius2D
{
    // Class for Lines
    public class Line : Polygon
    {
        public float length;

        // Method to Update Values of the Line, it will be used right after initializing (or Updating) the Line
        public Line(float centerX, float centerY, int NumOfVertices, float radie, float mass, float angle) : base(centerX, centerY, NumOfVertices, radie, mass, angle)
        {
            this.centrePos = new Vector2(centerX, centerY);

            // Setting Up Reference Positions
            this.ReferencePositions = new Vector2[4];
            for (var i = 0; i < NumOfVertices; i++)
            {
                this.ReferencePositions[i] = new Vector2((float)Math.Cos(Math.PI * i) * radie, (float)Math.Sin(Math.PI * i) * radie);
            }

            this.ReferencePositions[0] = new Vector2(this.ReferencePositions[0].X - (float)Math.Cos(Math.PI / 2), this.ReferencePositions[0].Y - (float)Math.Sin(Math.PI / 2));
            this.ReferencePositions[1] = new Vector2(this.ReferencePositions[1].X - (float)Math.Cos(Math.PI / 2), this.ReferencePositions[1].Y - (float)Math.Sin(Math.PI / 2));

            this.ReferencePositions[2] = new Vector2(this.ReferencePositions[1].X + (float)Math.Cos(Math.PI / 2), this.ReferencePositions[1].Y + (float)Math.Sin(Math.PI / 2));
            this.ReferencePositions[3] = new Vector2(this.ReferencePositions[0].X + (float)Math.Cos(Math.PI / 2), this.ReferencePositions[0].Y + (float)Math.Sin(Math.PI / 2));

            // Angle
            this.angle = angle;

            // Setting Up Updated Positions
            this.UpdatedPositions = new Vector2[4];
            for (var i = 0; i < 4; i++)
            {
                this.UpdatedPositions[i] = new Vector2(centrePos.X + ReferencePositions[i].X, centrePos.Y + ReferencePositions[i].Y);
                //Console.WriteLine(this.UpdatedPositions[i]);
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

            this.length = radie * 2.0f;
        }
        /*public Line()
        {
            this.p = new Vector2(posX1, posY1);
            this.q = new Vector2(posX2, posY2);

            this.length = (float) Math.Sqrt((this.p.X - this.q.X) * (this.p.X - this.q.X) + (this.p.Y - this.q.Y) * (this.p.Y - this.q.Y));
            this.angle = (float) Math.Atan2(this.p.Y - this.q.Y, this.p.X - this.q.X);
        }*/

        // Method to Draw the Line
        /*public void Draw()
        {
            Raylib.DrawLineV(this.p, this.q, Color.RAYWHITE);
        }*/
        public static Line makeLine(float posX1, float posY1, float posX2, float posY2)
        {
            float angle = (float) Math.Atan2(posY1 - posY2, posX1 - posX2);
            float distance = (float) Math.Sqrt(((posX1 - posX2) * (posX1 - posX2)) + ((posY1 - posY2) * (posY1 - posY2)));
            var newLine = new Line((posX1 + posX2) / 2, (posY1 + posY2) / 2, 2, distance / 2, 0, angle);

            return newLine;
        }
    }
}