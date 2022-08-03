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

            Raylib.SetTargetFPS(120);

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.DARKGRAY);

                    Raylib.DrawLine(0 + 50, 0, 0 + 50, Height - 50, Color.RAYWHITE);
                    Raylib.DrawLine(Width - 50, 0, Width - 50, Height - 50, Color.RAYWHITE);
                    Raylib.DrawLine(0 + 50, Height - 50, Width - 50, Height - 50, Color.RAYWHITE);
                Raylib.EndDrawing();
            }
        }
    }
}