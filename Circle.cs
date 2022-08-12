using System.Numerics;
using Raylib_cs;

namespace Radius2D
{
    public class Circle
    {
        public Vector2 pos;
        public Vector2 vel;
        public Vector2 force;
        public float radius;
        public float mass;
        public float elasticity;
        private Color tint;

        public void Update(int W, int H, int offset, List<Circle> circles, List<Line> lines)
        {
            float gravity = 0.01f;
            float terminalVel = 2.0f;

            foreach (Circle circ in circles)
            {
                if (circ != this)
                {
                    if (Collision.CircleToCircle(this, circ) == true)
                    {
                        Vector2 distance = this.pos - circ.pos;
                        float radiiSum = this.radius + circ.radius;
                        float length = (float) Math.Sqrt(distance.X * distance.X + distance.Y * distance.Y);
                        float depth = length - (this.radius + circ.radius + 1);
                        Vector2 unit = distance / length;

                        this.pos.X = circ.pos.X + (radiiSum + 1) * unit.X;
                        this.pos.Y = circ.pos.Y + (radiiSum + 1) * unit.Y;

                        //this.vel *= this.elasticity * circ.elasticity;
                        //circ.vel *= this.elasticity * circ.elasticity;

                        this.force -= unit * depth / 2;
                        circ.force += unit * depth / 2;
                    };
                }
            }
            tint = Color.RAYWHITE;
            /*foreach (Line line in lines)
            {
                Vector2 temp_vector = new Vector2(this.pos.X + this.vel.X, this.pos.Y + this.vel.Y);
                if (Collision.CircleToLine(line, this, temp_vector) <= this.radius)
                {
                    //Console.WriteLine(Collision.CircleToLine(line, this, temp_vector));
                    tint = Color.BLUE;
                    //float incidenceAngle = (float) Math.Atan2(this.vel.Y, this.vel.X) * 180 / 3.14f;
                    float normalRayAngle = line.angle + 90.0f;
                    //float deltaAngle = normalRayAngle - incidenceAngle;

                    //float newAngle = normalRayAngle + deltaAngle * 2.0f;

                    //this.force.X += (float) Math.Cos(normalRayAngle * 3.14f / 180) * this.mass;
                    //this.force.Y += (float) Math.Sin(normalRayAngle * 3.14f / 180) * this.mass;
                    this.pos.X -= (this.radius - Collision.CircleToLine(line, this, temp_vector)) * (float) Math.Cos(line.angle + 90 * 3.14f / 180);
                    this.pos.Y -= (this.radius - Collision.CircleToLine(line, this, temp_vector)) * (float) Math.Sin(line.angle + 90 * 3.14f / 180);
                }
            }*/
            Vector2 temp_vector = new Vector2(this.pos.X + this.vel.X, this.pos.Y + this.vel.Y);
            if (Collision.CircleToLine(lines[3], this, temp_vector) <= this.radius)
            {
                //Console.WriteLine(Collision.CircleToLine(line, this, temp_vector));
                tint = Color.BLUE;
                //float incidenceAngle = (float) Math.Atan2(this.vel.Y, this.vel.X) * 180 / 3.14f;
                float normalRayAngle = lines[3].angle + 90.0f;
                //float deltaAngle = normalRayAngle - incidenceAngle;

                //float newAngle = normalRayAngle + deltaAngle * 2.0f;

                //this.force.X += (float) Math.Cos(normalRayAngle * 3.14f / 180) * this.mass;
                //this.force.Y += (float) Math.Sin(normalRayAngle * 3.14f / 180) * this.mass;
                //this.pos.X -= (this.radius - Collision.CircleToLine(lines[3], this, temp_vector)) * (float) Math.Cos(lines[3].angle + 90 * 3.14f / 180);
                //this.pos.Y -= (this.radius - Collision.CircleToLine(lines[3], this, temp_vector)) * (float) Math.Sin(lines[3].angle + 90 * 3.14f / 180);
            }
            this.force.Y += gravity * this.mass;

            this.vel += this.force / this.mass * Raylib.GetFrameTime() * 120;

            if (this.vel.X >= terminalVel)
            {
                this.vel.X = terminalVel;
            }else if (this.vel.X < -terminalVel)
            {
                this.vel.X = -terminalVel;
            };

            if (this.vel.Y >= terminalVel)
            {
                this.vel.Y = terminalVel;
            }else if (this.vel.Y < -terminalVel)
            {
                this.vel.Y = -terminalVel;
            };

            /*if (this.pos.Y >= H - offset - this.radius)
            {
                this.pos.Y = H - offset - this.radius;
                this.vel.Y *= this.elasticity * -1;
            };
            if (this.pos.X < offset + this.radius)
            {
                this.pos.X = offset + this.radius;
                this.vel.X *= this.elasticity * -1;
            }else if (this.pos.X >= W - offset - this.radius)
            {
                this.pos.X = W - offset - this.radius;
                this.vel.X *= this.elasticity * -1;
            };*/

            this.pos += this.vel * Raylib.GetFrameTime() * 120;
            this.force = new Vector2(0, 0);
        }
        
        public void Draw()
        {
            Raylib.DrawCircleV(this.pos, this.radius, tint);
            Raylib.DrawCircleLines((int)this.pos.X, (int)this.pos.Y, this.radius, Color.BLACK);
            /*for (int i = 0; i < 270; i+=5)
            {
                Raylib.DrawLine((int)this.pos.X, (int)this.pos.Y, (int)(this.pos.X + Math.Cos(i * 3.14 / 180) * this.radius * 10), (int)(this.pos.Y + Math.Sin(i * 3.14 / 180) * this.radius * 10), Color.BLACK);
            }*/
        }
    }
}