using System.Numerics;
using Raylib_cs;

namespace Radius2D
{
    public class Polygon
    {
        public List<Vector2> ReferencePositions;
        public Vector2 centrePos;
        public float angle;
        public List<Vector2> UpdatedPositions;
        /*public Vector2 vel;
        public Vector2 force;
        public float mass;
        public float inverseMass;*/
        public Polygon(float centerX, float centerY, int NumOfEdges, float radie)
        {
            this.centrePos = new Vector2(centerX, centerY);
            this.ReferencePositions = new List<Vector2>(0);
            for (var i = 0; i < NumOfEdges; i++)
            {
                Vector2 referencePos = new Vector2((float)Math.Cos(2 * Math.PI / NumOfEdges * i) * radie, (float)Math.Sin(2 * Math.PI / NumOfEdges * i) * radie);
                ReferencePositions.Add(referencePos);
            }
            this.angle = 0.0f;
            this.UpdatedPositions = new List<Vector2>(0);
            for (var i = 0; i < NumOfEdges; i++)
            {
                Vector2 referencePos = new Vector2(centrePos.X + ReferencePositions[i].X, centrePos.Y + ReferencePositions[i].Y);
                UpdatedPositions.Add(referencePos);
            }
        }

        public void Update(float deltaTime)
        {}

        public void Draw()
        {
            Raylib.DrawLine((int)(this.UpdatedPositions[0].X), (int)(this.UpdatedPositions[0].Y), (int)(this.UpdatedPositions[this.UpdatedPositions.Count - 1].X), (int)(this.UpdatedPositions[this.UpdatedPositions.Count - 1].Y), Color.WHITE);
            Raylib.DrawLine((int)this.centrePos.X, (int)this.centrePos.Y, (int)(this.UpdatedPositions[0].X), (int)(this.UpdatedPositions[0].Y), Color.WHITE);
            for (var i = 0; i < this.ReferencePositions.Count - 1; i++)
            {
                Raylib.DrawLine((int)(this.UpdatedPositions[i].X), (int)(this.UpdatedPositions[i].Y), (int)(this.UpdatedPositions[i+1].X), (int)(this.UpdatedPositions[i+1].Y), Color.WHITE);
            }
        }
    }
}