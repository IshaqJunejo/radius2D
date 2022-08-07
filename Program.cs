using System.Numerics;
using Raylib_cs;

namespace Radius2D
{
    class Program
    {
        static void Main()
        {
            const int Width = 1050;
            const int Height = 950;

            Raylib.InitWindow(Width, Height, "Physics Simulation");

            List<Line> lines = new List<Line>(0);
            var temp = new Line();
            temp.p = new Vector2(50, 0);
            temp.q = new Vector2(50, Height - 50);
            lines.Add(temp);
            var temp_ = new Line();
            temp_.p = new Vector2(Width - 50, 0);
            temp_.q = new Vector2(Width - 50, Height - 50);
            lines.Add(temp_);
            var temp_line = new Line();
            temp_line.p = new Vector2(50, Height - 50);
            temp_line.q = new Vector2(Width - 50, Height - 50);
            lines.Add(temp_line);

            int numOfCircles = 200;
            List<Circle> circles = new List<Circle>(0);

            for (int i = 0; i < numOfCircles; i++)
            {
                var newCirc = new Circle();

                newCirc.pos = new Vector2(Raylib.GetRandomValue(100, 900), -50 * i);

                newCirc.vel = new Vector2(Raylib.GetRandomValue(-32, 32), Raylib.GetRandomValue(-32, 32));
                newCirc.force = new Vector2(0, 0);

                newCirc.radius = 10;
                newCirc.mass = (float) Math.Pow(newCirc.radius, 2) / 4;
                newCirc.elasticity = 0.4f;

                circles.Add(newCirc);
            }

            float FPS;
            string fpsText;
            int offset = 50;

            Raylib.SetTargetFPS(480);

            while (!Raylib.WindowShouldClose())
            {
                FPS = Raylib.GetFPS();
                fpsText = Convert.ToString(FPS);

                foreach (Circle circ in circles)
                {
                    circ.Update(Width, Height, offset, circles);
                };

                Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.DARKGRAY);

                    Raylib.DrawText(fpsText, 20, 20, 20, Color.RAYWHITE);
                    //Raylib.DrawLine(0 + offset, 0, 0 + offset, Height - offset, Color.RAYWHITE);
                    //Raylib.DrawLine(Width - offset, 0, Width - offset, Height - offset, Color.RAYWHITE);
                    //Raylib.DrawLine(0 + offset, Height - offset, Width - offset, Height - offset, Color.RAYWHITE);
                    foreach (Line l in lines)
                    {
                        l.DrawLine();
                    }

                    foreach (Circle circ in circles)
                    {
                        circ.Draw();
                    }

                Raylib.EndDrawing();
            }
        }
    }
}