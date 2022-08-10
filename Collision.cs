using System.Numerics;

namespace Radius2D
{
    public class Collision
    {
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

        public static float CircleToLine(Line l, Circle circ, Vector2 position)
        {
            if (Math.Atan2(l.p.Y - circ.pos.Y, l.p.X - circ.pos.X) >= -90 * 3.14 / 180 && Math.Atan2(l.p.Y - circ.pos.Y, l.p.X - circ.pos.X) <= 90 * 3.14 / 180)
            {
                if (Math.Atan2(l.q.Y - circ.pos.Y, l.q.X - circ.pos.X) >= -90 * 3.14 / 180 && Math.Atan2(l.q.Y - circ.pos.Y, l.q.X - circ.pos.X) <= 90 * 3.14 / 180)
                {
                    Vector2 dist = l.p - l.q;
                    float Base = (float) Math.Sqrt(dist.X * dist.X + dist.Y * dist.Y);
                    float height = TriangleArea(l.p, l.q, position) * 2 / Base;

                    return height;
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