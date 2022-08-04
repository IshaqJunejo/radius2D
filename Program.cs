using System.Collections.Generic;
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

            int numOfCircles = 300;
            List<Particle> circles = new List<Particle>(numOfCircles);

            for (int i = 0; i < numOfCircles; i++)
            {
                var newCirc = new Particle();

                newCirc.pos = new Vector2(Raylib.GetRandomValue(100, 900), -80 * i);

                newCirc.vel = new Vector2(Raylib.GetRandomValue(-4, 4), Raylib.GetRandomValue(-4, 4));
                newCirc.force = new Vector2(0, 0);

                newCirc.radius = 10;
                newCirc.mass = (float) Math.Pow(newCirc.radius, 2) / 4;
                newCirc.elasticity = 0.5f;

                circles.Add(newCirc);
            }

            float FPS;
            int offset = 50;

            Raylib.SetTargetFPS(120);

            while (!Raylib.WindowShouldClose())
            {
                FPS = Raylib.GetFPS();

                foreach (Particle circ in circles)
                {
                    circ.Update(Width, Height, offset);
                }

                Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.DARKGRAY);

                    Raylib.DrawLine(0 + offset, 0, 0 + offset, Height - offset, Color.RAYWHITE);
                    Raylib.DrawLine(Width - offset, 0, Width - offset, Height - offset, Color.RAYWHITE);
                    Raylib.DrawLine(0 + offset, Height - offset, Width - offset, Height - offset, Color.RAYWHITE);

                    foreach (Particle circ in circles)
                    {
                        circ.Draw();
                    }

                Raylib.EndDrawing();
            }
        }
    }
}