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

            int numOfCircles = 15;
            List<Circle> circles = new List<Circle>(0);

            for (int i = 0; i < numOfCircles; i++)
            {
                var newCirc = new Circle();

                newCirc.pos = new Vector2(Raylib.GetRandomValue(20, 900), Raylib.GetRandomValue(20, 900));

                newCirc.vel = new Vector2(Raylib.GetRandomValue(-32, 32) / 4, Raylib.GetRandomValue(-32, 32) / 4);
                newCirc.force = new Vector2(0, 0);

                newCirc.radius = Raylib.GetRandomValue(12, 30);
                newCirc.mass = (float) Math.Pow(newCirc.radius, 3) / 4;
                newCirc.elasticity = 0.4f;

                circles.Add(newCirc);
            }

            float FPS;
            string fpsText;

            Raylib.SetTargetFPS(120);

            while (!Raylib.WindowShouldClose())
            {
                FPS = Raylib.GetFPS();
                fpsText = Convert.ToString(FPS);

                foreach (Circle circ in circles)
                {
                    circ.Update(Width, Height, circles);
                }

                Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.DARKGRAY);

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