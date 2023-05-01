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
            if ((circ1.pos.X - circ2.pos.X) * (circ1.pos.X - circ2.pos.X) + (circ1.pos.Y - circ2.pos.Y) * (circ1.pos.Y - circ2.pos.Y) <= (circ1.radius + circ2.radius) * (circ1.radius + circ2.radius))
            {
                return true;
            }else
            {
                return false;
            }
        }

        // Method to Calculate collision between two Bounding Boxes
        public static bool AABBToAABB(AABB a, AABB b)
        {
            if (a.getEdgeXMax() <= b.getEdgeXMin() || b.getEdgeXMax() <= a.getEdgeXMin())
            {
                return false;
            }

            if (a.getEdgeYMax() <= b.getEdgeYMin() || b.getEdgeYMax() <= a.getEdgeYMin())
            {
                return false;
            }
            
            return true;
        }

        public static Vector2 ClosestPointBoxToCircle(AABB box, Circle circ)
        {
            float closestPointX, closestPointY;

            if (circ.pos.X < box.getEdgeXMin())
            {
                closestPointX = box.getEdgeXMin();
            }else if (circ.pos.X > box.getEdgeXMax())
            {
                closestPointX = box.getEdgeXMax();
            }else
            {
                closestPointX = circ.pos.X;
            }

            if (circ.pos.Y < box.getEdgeYMin())
            {
                closestPointY = box.getEdgeYMin();
            }else if (circ.pos.Y > box.getEdgeYMax())
            {
                closestPointY = box.getEdgeYMax();
            }else
            {
                closestPointY = circ.pos.Y;
            }

            return new Vector2(closestPointX, closestPointY);
        }

        // Method to calculate Collision between Bounding Box and Line
        public static bool AABBToLine(AABB box, Line wall)
        {
            float left = Math.Max(box.pos.X, Math.Min(wall.p.X, wall.q.X));
            float right = Math.Min(box.pos.X, Math.Max(wall.p.X, wall.q.X));
            float top = Math.Max(box.pos.Y, Math.Min(wall.p.Y, wall.q.Y));
            float bottom = Math.Min(box.pos.Y, Math.Max(wall.p.Y, wall.q.Y));

            if (left > right || top > bottom)
            {
                return false;
            }

            // calculating the point of intersection
            /*float u1 = ((left - wall.p.X) * (wall.q.Y - wall.p.Y) - (top - wall.p.Y) * (wall.q.X - wall.p.X)) / (float)(Math.Pow((wall.q.X - wall.p.X), 2) + Math.Pow((wall.q.Y - wall.p.Y), 2));
            if (u1 > 1 || u1 < 0)
            {
                return false;
            }*/
            return true;
        }

        // Method to calculate Collision between Ball/Circle and Line
        public static float CircleToLine(Line l, Circle circ)
        {
            Vector2 v1 = (l.p - l.q) / l.length;
            Vector2 v2 = (l.q - l.p) / l.length;

            Vector2 v3 = circ.pos - l.p;
            Vector2 v4 = circ.pos - l.q;

            if (Vector2.Dot(v2, v3) > 0 && Vector2.Dot(v1, v4) > 0)
            {
                Vector2 dist = l.p - l.q;
                float Base = (float) Math.Sqrt(dist.X * dist.X + dist.Y * dist.Y);
                float height = TriangleArea(l.p, l.q, circ.pos) * 2 / Base;
                return height;
            }else
            {
                return circ.radius * 2;
            }
        }

        // Method to Check Collision between Spring And a Point
        public static bool SpringToPoint(Spring spr, Vector2 point)
        {
            float angleToPoint = (float) Math.Atan2(spr.ball1.pos.Y - point.Y, spr.ball1.pos.X - point.X);
            float magnitude = (float) Math.Sqrt((spr.ball1.pos.X - point.X) * (spr.ball1.pos.X - point.X) + (spr.ball1.pos.Y - point.Y) * (spr.ball1.pos.Y - point.Y));

            float angleOfSpr = (float) Math.Atan2(spr.ball1.pos.Y - spr.ball2.pos.Y, spr.ball1.pos.X - spr.ball2.pos.X);
            float magnitudeOfSpr = (float) Math.Sqrt((spr.ball1.pos.X - spr.ball2.pos.X) * (spr.ball1.pos.X - spr.ball2.pos.X) + (spr.ball1.pos.Y - spr.ball2.pos.Y) * (spr.ball1.pos.Y - spr.ball2.pos.Y));

            if (angleToPoint >= angleOfSpr - 0.000001f || angleToPoint <= angleOfSpr + 0.000001f)
            {
                if (magnitude <= magnitudeOfSpr)
                {
                    return true;
                }                
            }

            return false;
        }

        public static float PolygonToPolygon(Polygon poly01, Polygon poly02)
        {
            Polygon shape01 = poly01;
            Polygon shape02 = poly02;

            float overlap = (float) double.PositiveInfinity;

            for (var shape = 0; shape < 2; shape++)
            {
                if (shape == 1)
                {
                    shape01 = poly02;
                    shape02 = poly01;
                }

                for (var i = 0; i < shape01.UpdatedPositions.Length; i++)
                {
                    int b = (i + 1) % shape01.UpdatedPositions.Length;
                    Vector2 axisProj = new Vector2(-(shape01.UpdatedPositions[b].Y - shape01.UpdatedPositions[i].Y), shape01.UpdatedPositions[b].X - shape01.UpdatedPositions[i].X);
                    float dist = (float) Math.Sqrt(axisProj.X * axisProj.X + axisProj.Y * axisProj.Y);
                    axisProj = new Vector2(axisProj.X / dist, axisProj.Y / dist);

                    float min1 = (float) double.PositiveInfinity;
                    float max1 = (float) double.NegativeInfinity;

                    for (var j = 0; j < shape01.UpdatedPositions.Length; j++)
                    {
                        float dotProduct = (shape01.UpdatedPositions[j].X * axisProj.X + shape01.UpdatedPositions[j].Y * axisProj.Y);
                        min1 = Math.Min(min1, dotProduct);
                        max1 = Math.Max(max1, dotProduct);
                    }

                    float min2 = (float) double.PositiveInfinity;
                    float max2 = (float) double.NegativeInfinity;

                    for (var j = 0; j < shape02.UpdatedPositions.Length; j++)
                    {
                        float dotProduct = (shape02.UpdatedPositions[j].X * axisProj.X + shape02.UpdatedPositions[j].Y * axisProj.Y);
                        min2 = Math.Min(min2, dotProduct);
                        max2 = Math.Max(max2, dotProduct);
                    }

                    overlap = (float) Math.Min(Math.Min(max1, max2) - Math.Max(min1, min2), overlap);

                    if (!(max2 >= min1 && min1 <= max2))
                    {
                        return -1;
                    }
                }
            }
            return overlap;
        }

        public static float PolygonToCircle(Polygon poly, Circle circ)
        {
            Vector2 axisProj;
            float overlap = (float) double.PositiveInfinity;

            float min1, max1, min2, max2;

            for (var i = 0; i < poly.UpdatedPositions.Length; i++)
            {
                int b = (i + 1) % poly.UpdatedPositions.Length;
                axisProj = new Vector2(-(poly.UpdatedPositions[b].Y - poly.UpdatedPositions[i].Y), poly.UpdatedPositions[b].X - poly.UpdatedPositions[i].X);
                float dist = (float) Math.Sqrt(axisProj.X * axisProj.X + axisProj.Y * axisProj.Y);
                axisProj = new Vector2(axisProj.X / dist, axisProj.Y / dist);

                min1 = (float) double.PositiveInfinity;
                max1 = (float) double.NegativeInfinity;

                for (var j = 0; j < poly.UpdatedPositions.Length; j++)
                {
                    float dotProduct = (poly.UpdatedPositions[j].X * axisProj.X + poly.UpdatedPositions[j].Y * axisProj.Y);
                    min1 = Math.Min(min1, dotProduct);
                    max1 = Math.Max(max1, dotProduct);
                }

                min2 = (float) double.PositiveInfinity;
                max2 = (float) double.NegativeInfinity;

                for (var j = 0; j < 2; j++)
                {
                    float dotProduct;
                    if (j == 1)
                    {
                        dotProduct = ((circ.pos - (axisProj * circ.radius)).X * axisProj.X + (circ.pos - (axisProj * circ.radius)).Y * axisProj.Y);
                    }else
                    {
                        dotProduct = ((circ.pos + (axisProj * circ.radius)).X * axisProj.X + (circ.pos + (axisProj * circ.radius)).Y * axisProj.Y);
                    }

                    min2 = Math.Min(min2, dotProduct);
                    max2 = Math.Max(max2, dotProduct);
                }

                if (!(max2 >= min1 && min1 <= max2))
                {
                    return -1;
                }

                overlap = (float) Math.Min(Math.Min(max1, max2) - Math.Max(min1, min2), overlap);
            }

            int cpIndex = Collision.FindClosestPointOnPolygon(circ, poly);

            axisProj = poly.UpdatedPositions[cpIndex] - circ.pos;
            float temp = (float)Math.Sqrt(axisProj.X * axisProj.X + axisProj.Y * axisProj.Y);
            axisProj /= temp;

            min1 = (float) double.PositiveInfinity;
            max1 = (float) double.NegativeInfinity;

            for (var j = 0; j < poly.UpdatedPositions.Length; j++)
            {
                float dotProduct = (poly.UpdatedPositions[j].X * axisProj.X + poly.UpdatedPositions[j].Y * axisProj.Y);
                min1 = Math.Min(min1, dotProduct);
                max1 = Math.Max(max1, dotProduct);
            }

            min2 = (float) double.PositiveInfinity;
            max2 = (float) double.NegativeInfinity;

            for (var j = 0; j < 2; j++)
            {
                float dotProduct;
                if (j == 1)
                {
                    dotProduct = ((circ.pos - (axisProj * circ.radius)).X * axisProj.X + (circ.pos - (axisProj * circ.radius)).Y * axisProj.Y);
                }else
                {
                    dotProduct = ((circ.pos + (axisProj * circ.radius)).X * axisProj.X + (circ.pos + (axisProj * circ.radius)).Y * axisProj.Y);
                }

                min2 = Math.Min(min2, dotProduct);
                max2 = Math.Max(max2, dotProduct);
            }

            if (!(max2 >= min1 && min1 <= max2))
            {
                return -1;
            }

            overlap = (float) Math.Min(Math.Min(max1, max2) - Math.Max(min1, min2), overlap);

            return overlap;
        }

        private static int FindClosestPointOnPolygon(Circle circ, Polygon poly)
        {
            int result = -1;
            float minDistance = (float) double.PositiveInfinity;

            for (var i = 0; i < poly.UpdatedPositions.Length; i++)
            {
                Vector2 temp = circ.pos - poly.UpdatedPositions[i];
                float sqMag = temp.X * temp.X + temp.Y * temp.Y;

                if (sqMag < minDistance)
                {
                    minDistance = sqMag;
                    result = i;
                }
            }

            return result;
        }

        public static float RadToDeg(float rad)
        {
            return rad * 180 / 3.142f;
        }

        public static float DegToRad(float deg)
        {
            return deg * 3.142f / 180;
        }
    }
}