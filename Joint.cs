using System.Numerics;
using Raylib_cs;

namespace Radius2D
{
    public class Joint
    {
        private Circle ball1;
        private Circle ball2;
        private float length;
        public Joint(Circle circ1, Circle circ2, float length)
        {
            this.ball1 = circ1;
            this.ball2 = circ2;

            if (circ1.radius + circ2.radius <= length)
            {
                this.length = length;
            }else
            {
                this.length = circ1.radius + circ2.radius;
            }
            
        }

        public void update(float deltaTime)
        {
            for (int i = 0; i < 5; i++)
            {
                Vector2 axis = this.ball1.pos - this.ball2.pos;
                float dist = (float) Math.Sqrt((axis.X * axis.X) + (axis.Y * axis.Y));
                Vector2 normal = axis / dist;
            
                float different = this.length - dist;

                this.ball1.pos += different * normal * 0.5f * this.ball1.inverseMass * this.ball1.mass;
                this.ball2.pos -= different * normal * 0.5f * this.ball2.inverseMass * this.ball2.mass;
            }
        }

        public void draw()
        {
            Raylib.DrawLineV(ball1.pos, ball2.pos, Color.WHITE);
        }
    }
}