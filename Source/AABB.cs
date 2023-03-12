using System.Numerics;
using Raylib_cs;

namespace Radius2D
{
    public class AABB
    {
        public Vector2 pos;
        public Vector2 vel;
        public Vector2 force;
        public float width;
        public float height;
        public float mass;
        public float inverseMass;
        private Color shade;

        public AABB(float posX, float posY, float Width, float Height, float Mass, Color color)
        {
            this.pos = new Vector2(posX, posY);

            this.width = Width;
            this.height = Height;

            this.mass = Mass;
            if (Mass != 0.0f)
            {
                this.inverseMass = 1 / Mass;
            }else
            {
                this.inverseMass = 0.0f;
            }

            this.shade = color;
        }

        public void Update(float deltaTime)
        {
            float terminalVel = 7.5f;
            this.force.Y += 0.1f * this.mass;
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

            this.pos += this.vel * deltaTime * 60;

            // Just a Demo Code Snippet
            if (this.pos.Y >= 950 - this.height)
            {
                this.pos.Y = 950 - this.height;
            }

            this.force = new Vector2(0, 0);
        }

        public void CollisionResponseBox(AABB box, float deltaTime)
        {
            if (this != box)
            {
                if (Collision.AABBToAABB(this, box))
                {
                    float depthX = Math.Min(this.getEdgeXMax(), box.getEdgeXMax()) - Math.Max(this.getEdgeXMin(), box.getEdgeXMin());
                    float depthY = Math.Min(this.getEdgeYMax(), box.getEdgeYMax()) - Math.Max(this.getEdgeYMin(), box.getEdgeYMin());

                    if (depthX < depthY)
                    {
                        if (this.getEdgeXMin() > box.getEdgeXMin())
                        {
                            this.pos.X += depthX / 2;
                            box.pos.X -= depthX / 2;
                        }else
                        {
                            this.pos.X -= depthX / 2;
                            box.pos.X += depthX / 2;
                        }
                    }else
                    {
                        if (this.getEdgeYMin() > box.getEdgeYMin())
                        {
                            this.pos.Y += depthY / 2;
                            box.pos.Y -= depthY / 2;
                        }else
                        {
                            this.pos.Y -= depthY / 2;
                            box.pos.Y += depthY / 2;
                        }
                    }
                }
            }
        }

        public float getEdgeXMin()
        {
            return this.pos.X;
        }

        public float getEdgeXMax()
        {
            return this.pos.X + this.width;
        }

        public float getEdgeYMin()
        {
            return this.pos.Y;
        }

        public float getEdgeYMax()
        {
            return this.pos.Y + this.height;
        }

        public void drawBox()
        {
            Raylib.DrawRectangle((int)this.pos.X, (int)this.pos.Y, (int)this.width, (int)this.height, this.shade);
        }

        public void drawBoxC()
        {
            Raylib.DrawRectangle((int)this.pos.X, (int)this.pos.Y, (int)this.width, (int)this.height, Color.WHITE);
        }
    }
}