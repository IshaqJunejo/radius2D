using System.Numerics;
using Raylib_cs;

namespace Radius2D
{
    // Class for the Spring Connections
    public class Spring
    {
        // Few Private Variables
        public Circle ball1;
        public Circle ball2;
        private float length;
        private float stiffness;
        private float dampFactor;

        // Constructor
        public Spring(Circle circ1, Circle circ2, float length, float stiff, float damp)
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
            this.stiffness = stiff;
            this.dampFactor = damp;
            
        }
        
        // Update Function to update Springs
        public void Update(float deltaTime)
        {
            // Finding the Normalized Position vector
            Vector2 len = this.ball1.pos - this.ball2.pos;
            float distance = (float) Math.Sqrt(len.X * len.X + len.Y * len.Y);
            Vector2 normal = len / distance;

            // Difference in length
            float deltaLength = distance - this.length;

            // Finding the velocity difference
            Vector2 deltaVel = this.ball1.vel - this.ball2.vel;

            // Dot product of velocity difference and normal position vector
            float dampFactorDotProduct = Vector2.Dot(deltaVel, normal);

            // Forces calculation
            float springForce = deltaLength * this.stiffness;
            Vector2 dampForce = (deltaVel) * dampFactor;
            Vector2 totalForce = (springForce * normal) + dampForce;

            // Adding the forces
            this.ball1.force -= totalForce;
            this.ball2.force += totalForce;
        }

        // Function to Draw Springs (as Lines)
        public void Draw()
        {
            Raylib.DrawLineV(ball1.pos, ball2.pos, Color.WHITE);
        }
    }
}