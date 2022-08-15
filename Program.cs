using System.Numerics;
using Raylib_cs;

// Namespace for the Physics Simulation
namespace Radius2D
{
    // Main Class of the program
    class Program
    {
        // Main function of the Class to be executed
        static void Main()
        {
            // Initializing the Window to be rendered on
            const int Width = 1050;
            const int Height = 950;
            Raylib.InitWindow(Width, Height, "Physics Simulation");

            // Initializing the List of Balls/Circles
            int numOfCircles = 50;
            List<Circle> circles = new List<Circle>(0);
            for (int i = 0; i < numOfCircles; i++)
            {
                var newCirc = new Circle();

                newCirc.pos = new Vector2(Raylib.GetRandomValue(20, 900), Raylib.GetRandomValue(20, 900));

                newCirc.vel = new Vector2(Raylib.GetRandomValue(-8, 8), Raylib.GetRandomValue(-8, 8));
                newCirc.force = new Vector2(0, 0);

                newCirc.radius = Raylib.GetRandomValue(10, 30);
                newCirc.mass = (float) Math.Pow(newCirc.radius, 3) / 4;
                newCirc.elasticity = 1.0f;

                circles.Add(newCirc);
            }

            // Some Extra Variables
            float FPS;
            string fpsText;

            // Setting FrameRate and Starting the Main Loop
            Raylib.SetTargetFPS(120);
            while (!Raylib.WindowShouldClose())
            {
                // Updating the Extra Variables
                FPS = Raylib.GetFPS();
                fpsText = Convert.ToString(FPS);

                // Iterating through the List of Balls/Circles
                foreach (Circle circle in circles)
                {
                    // Updating the Positions of Balls/Circles
                    circle.Update(Width, Height);

                    // Looking for and Resolving the Collision between the Balls/Circles
                    foreach (Circle circ in circles)
                    {
                        circle.CollisionResponse(circ);
                    }
                };

                // Rendering Section of the Program
                Raylib.BeginDrawing();
                    // Drawing Background
                    Raylib.ClearBackground(Color.DARKGRAY);

                    // Draw the current FrameRate
                    Raylib.DrawText(fpsText, 20, 20, 20, Color.RAYWHITE);

                    // Drawing every Ball/Circle
                    foreach (Circle circ in circles)
                    {
                        circ.Draw();
                    }
                
                // End of Rendering section
                Raylib.EndDrawing();
            }

            // Closing/Ending the Program
            Raylib.CloseWindow();
        }
    }
}