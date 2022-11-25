using System.Numerics;
using Raylib_cs;

namespace Radius2D
{
    // Class for Capsules
    public class Capsule
    {
        public Vector2 pos;
        public Vector2 vel;
        public Vector2 force;
        private Vector2 pos1;
        private Vector2 pos2;
        public float length;
        public float angle;
        public float radius;
        public float mass;
        private float angVel;
        public Color shade;

        // Constructor
        public Capsule(float posX, float posY, float leng, float radius, float mass, float angle, Color shade)
        {
            this.pos = new Vector2(posX, posY);
            this.vel = new Vector2(0, 0);
            this.force = new Vector2(0, 0);

            this.radius = radius;
            this.mass = mass;

            this.angle = angle;
            this.length = leng;

            this.shade = shade;
        }

        // Method to Update Capsule
        public void Update(float deltaTime)
        {
            // Detecting Key Press for Change Direction
            if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
            {
                this.angVel += 0.001f;
            }else if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
            {
                this.angVel -= 0.001f;
            };

            // Detecting Key Press for adding velocity of capsule
            if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
            {
                this.vel.X += (float)(Math.Cos(this.angle)) * 60 * deltaTime;
                this.vel.Y -= (float)(Math.Sin(this.angle)) * 60 * deltaTime;
            }

            // Limiting the angular Velocity
            if (this.angVel >= 0.5f)
            {
                this.angVel = 0.5f;
            }

            // Updating Some Values
            this.angle += this.angVel;
            this.angVel /= 1.01f;
            
            this.pos += this.vel;
            this.vel /= 1.01f;

            this.pos1 = new Vector2(pos.X - (float)(Math.Cos(this.angle) * this.length / 2), pos.Y + (float)(Math.Sin(this.angle) * this.length / 2));
            this.pos2 = new Vector2(pos.X + (float)(Math.Cos(this.angle) * this.length / 2), pos.Y - (float)(Math.Sin(this.angle) * this.length / 2));
        }

        // Method to Draw Capsule
        public void Draw()
        {
            Raylib.DrawCircleSector(this.pos1, this.radius, (this.angle * 180 / 3.142f), (this.angle * 180 / 3.142f) - 180, (int)this.radius, this.shade);
            Raylib.DrawCircleSector(this.pos2, this.radius, (this.angle * 180 / 3.142f), (this.angle * 180 / 3.142f) + 180, (int)this.radius, this.shade);
            Raylib.DrawLineV(pos, pos2, this.shade);

            Vector2 corner0 = new Vector2(pos1.X + (float)(Math.Cos(this.angle + Math.PI / 2) * this.radius), pos1.Y + (float)(Math.Sin(this.angle - Math.PI / 2) * this.radius));
            Vector2 corner1 = new Vector2(pos1.X - (float)(Math.Cos(this.angle + Math.PI / 2) * this.radius), pos1.Y - (float)(Math.Sin(this.angle - Math.PI / 2) * this.radius));
            Vector2 corner2 = new Vector2(pos2.X + (float)(Math.Cos(this.angle - Math.PI / 2) * this.radius), pos2.Y - (float)(Math.Sin(this.angle - Math.PI / 2) * this.radius));
            Vector2 corner3 = new Vector2(pos2.X - (float)(Math.Cos(this.angle - Math.PI / 2) * this.radius), pos2.Y + (float)(Math.Sin(this.angle - Math.PI / 2) * this.radius));

            Raylib.DrawTriangle(corner0, corner1, corner2, this.shade);
            Raylib.DrawTriangle(corner0, corner2, corner3, this.shade);
        }
    }
}