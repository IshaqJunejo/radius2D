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

            // Initializing the List of Lines
            List<Line> lines = new List<Line>(0);
            var line01 = new Line(1, 600, Width - 150, Height - 1);
            lines.Add(line01);
            var line02 = new Line(0, 0, 0, Height);
            lines.Add(line02);
            var line03 = new Line(Width, 0, Width, Height);
            lines.Add(line03);
            var line04 = new Line(0, Height, Width, Height);
            lines.Add(line04);

            // Initializing the List of Balls/Circles
            int numOfCircles = 100;
            List<Circle> circles = new List<Circle>(0);
            for (int i = 0; i < numOfCircles; i++)
            {
                var newCirc = new Circle(Raylib.GetRandomValue(0, 1000), -30 * i, 0, 0, 15, 0.8f);

                circles.Add(newCirc);
            }

            // Some Extra Variables
            float FPS;
            float deltaTime;
            string fpsText;

            // Setting FrameRate and Starting the Main Loop
            Raylib.SetTargetFPS(120);
            while (!Raylib.WindowShouldClose())
            {
                // Updating the Extra Variables
                FPS = Raylib.GetFPS();
                fpsText = Convert.ToString(FPS);
                deltaTime = Raylib.GetFrameTime();

                // Iterating through the List of Balls/Circles
                foreach (Circle circle in circles)
                {
                    // Updating the Positions of Balls/Circles
                    circle.Update(Width, Height, deltaTime);
                    // Looking for and Resolving the Collision between the Balls/Circles
                    foreach (Circle circ in circles)
                    {
                        circle.CollisionResponseCircle(circ, deltaTime);
                    }
                    foreach (Line l in lines)
                    {
                        circle.CollisionResponseLine(l, deltaTime);
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
                    // Drawing every Line
                    foreach (Line l in lines)
                    {
                        l.DrawLine();
                    }
                
                // End of Rendering section
                Raylib.EndDrawing();
            }

            // Closing/Ending the Program
            Raylib.CloseWindow();
        }
    }
}