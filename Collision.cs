using System.Numerics;

namespace Radius2D
{
    // Class for Methods of checking Collision between different objects
    public class Collision
    {
        // Method for Calculating Area of Triangle, it is used in collision between line and circle
        private static float TriangleArea(Vector2 a, Vector2 b, Vector2 c)
        {
            Vector2 AB = new Vector2(b.X - a.X, b.Y - a.Y);
            Vector2 AC = new Vector2(c.X - a.X, c.Y - a.Y);

            float crossProduct = AB.X * AC.Y - AB.Y * AC.X;

            if (crossProduct >= 0)
            {
                return crossProduct / 2;
            }else
            {
                return crossProduct * -1 / 2;
            };
        }

        // Method to Calculate collision between Two Balls/Circles
        public static bool CircleToCircle(Circle circ1, Circle circ2)
        {
            if (Math.Sqrt((circ1.pos.X - circ2.pos.X) * (circ1.pos.X - circ2.pos.X) + (circ1.pos.Y - circ2.pos.Y) * (circ1.pos.Y - circ2.pos.Y)) <= circ1.radius + circ2.radius)
            {
                return true;
            }else
            {
                return false;
            }
        }

        // Method to calculate Collision between Ball/Circle and Line
        public static float CircleToLine(Line l, Circle circ, Vector2 position)
        {
            /*if (Math.Atan2(l.p.Y - circ.pos.Y, l.p.X - circ.pos.X) >= -90 * 3.14 / 180 && Math.Atan2(l.p.Y - circ.pos.Y, l.p.X - circ.pos.X) <= 90 * 3.14 / 180)
            {
                if (Math.Atan2(l.q.Y - circ.pos.Y, l.q.X - circ.pos.X) <= 270 * 3.14 / 180 && Math.Atan2(l.q.Y - circ.pos.Y, l.q.X - circ.pos.X) >= 90 * 3.14 / 180)
                {
                    Vector2 dist = l.p - l.q;
                    float Base = (float) Math.Sqrt(dist.X * dist.X + dist.Y * dist.Y);
                    float height = TriangleArea(l.p, l.q, position) * 2 / Base;

                    return height;
                }elses
                {
                    return circ.radius * 2;
                }
            }else
            {
                return circ.radius * 2;
            }*/
            double angFrom01 = Math.Atan2(l.p.Y - circ.pos.Y, l.p.X - circ.pos.X);
            double angFrom02 = Math.Atan2(l.q.Y - circ.pos.Y, l.q.X - circ.pos.X);
            double distance = Math.Sqrt((l.p.X - circ.pos.X) * (l.p.X - circ.pos.X) + (l.p.Y - circ.pos.Y) * (l.p.Y - circ.pos.Y));

            float height = (float) (Math.Sin(angFrom01 - l.angle) * distance);
            if (angFrom02 < 90 * Math.PI / 180 && angFrom02 >= -90 * Math.PI / 180)
            {
                Console.Write(l.angle + " - " + angFrom01 + " X " + 180 + " -:- " + Math.PI + " = ");
                Console.WriteLine(l.angle - angFrom01 * 180 / Math.PI);
                //Console.WriteLine(90 * Math.PI / 180);
                if (l.angle - angFrom01 <= 90 * Math.PI / 180 && l.angle - angFrom01 > 90 * Math.PI / 180)
                {
                    Console.WriteLine(height);
                    return Math.Abs(height);
                }else if (l.angle - angFrom01 >= 100 * Math.PI / 180)
                {
                    return (float) distance;
                }else
                {
                    return circ.radius * 2;
                }
            }else
            {
                return circ.radius * 2;
            }
        }
    }
}