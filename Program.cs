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

            int numOfCircles = 150;
            List<Circle> circles = new List<Circle>(numOfCircles);

            for (int i = 0; i < numOfCircles; i++)
            {
                var newCirc = new Circle();

                newCirc.pos = new Vector2(Raylib.GetRandomValue(100, 900), -50 * i);

                newCirc.vel = new Vector2(Raylib.GetRandomValue(-32, 32), Raylib.GetRandomValue(-32, 32));
                newCirc.force = new Vector2(0, 0);

                newCirc.radius = 10;
                newCirc.mass = (float) Math.Pow(newCirc.radius, 2) / 4;
                newCirc.elasticity = Raylib.GetRandomValue(1, 90) / 100.0f;

                circles.Add(newCirc);
            }

            float FPS;
            string fpsText;
            int offset = 50;

            Raylib.SetTargetFPS(120);

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

                    Raylib.DrawLine(0 + offset, 0, 0 + offset, Height - offset, Color.RAYWHITE);
                    Raylib.DrawLine(Width - offset, 0, Width - offset, Height - offset, Color.RAYWHITE);
                    Raylib.DrawLine(0 + offset, Height - offset, Width - offset, Height - offset, Color.RAYWHITE);

                    Raylib.DrawText(fpsText, 20, 20, 20, Color.RAYWHITE);

                    foreach (Circle circ in circles)
                    {
                        circ.Draw();
                    }

                Raylib.EndDrawing();
            }
        }
    }
}