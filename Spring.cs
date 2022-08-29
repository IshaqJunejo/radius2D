using System.Numerics;
using Raylib_cs;

namespace Radius2D
{
    public class Spring
    {
        private Circle ball1;
        private Circle ball2;
        private float length;
        private float stiffness;
        public Spring(Circle circ1, Circle circ2, float length)
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
            this.stiffness = 1.0f;
            
        }

        public void update(float deltaTime)
        {
            Vector2 len = this.ball1.pos - this.ball2.pos;
            float distance = (float) Math.Sqrt(len.X * len.X + len.Y * len.Y);
            Vector2 normal = len / distance;

            float deltaLength = distance - this.length;

            this.ball1.force += normal * deltaLength * this.stiffness;
            this.ball2.force -= normal * deltaLength * this.stiffness;
        }

        public void draw()
        {
            Raylib.DrawLineV(ball1.pos, ball2.pos, Color.WHITE);
        }
    }
}