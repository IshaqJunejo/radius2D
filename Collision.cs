using Raylib_cs;

namespace Radius2D
{
    public class Collision
    {
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
    }
}