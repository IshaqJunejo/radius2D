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

            int numOfCircles = 20;
            List<Circle> circles = new List<Circle>(0);

            for (int i = 0; i < numOfCircles; i++)
            {
                var newCirc = new Circle();

                newCirc.pos = new Vector2(Raylib.GetRandomValue(20, 900), -60 * i);

                newCirc.vel = new Vector2(Raylib.GetRandomValue(-8, 8) / 4, Raylib.GetRandomValue(-8, 8) / 4);
                newCirc.force = new Vector2(0, 0);

                newCirc.radius = Raylib.GetRandomValue(8, 25);
                newCirc.mass = (float) Math.Pow(newCirc.radius, 3) / 4;
                newCirc.elasticity = 1.0f;

                circles.Add(newCirc);
            }

            float FPS;
            string fpsText;

            Raylib.SetTargetFPS(120);

            while (!Raylib.WindowShouldClose())
            {
                FPS = Raylib.GetFPS();
                fpsText = Convert.ToString(FPS);

                foreach (Circle circle in circles)
                {
                    circle.Update(Width, Height);
                };

                foreach (Circle circle in circles)
                {
                    foreach (Circle circ in circles)
                    {
                        circle.CollisionResponse(circ);
                    }
                };

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