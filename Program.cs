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
            var line01 = new Line(-50, 600, Width - 150, Height - 1);
            lines.Add(line01);
            var line02 = new Line(0, 0, 0, Height);
            lines.Add(line02);
            var line03 = new Line(Width, 0, Width, Height);
            lines.Add(line03);
            var line04 = new Line(0, Height, Width, Height);
            lines.Add(line04);

            // Initializing the List of Balls/Circles
            int numOfRows = 2;
            List<Circle> circles = new List<Circle>(0);
            for (int i = 0; i < numOfRows; i++)
            {
                var newCirc = new Circle((50 * i) + 500, (50 * i) + 200, 0, 0, 10, 50, 0.8f, Color.RAYWHITE);
                var newCircle = new Circle((50 * i) + 550, (50 * i) + 250, 0, 0, 10, 50, 0.8f, Color.RAYWHITE);
                circles.Add(newCirc);
                circles.Add(newCircle);
            }

            List<Spring> links = new List<Spring>(0);
            var temp = new Spring(circles[0], circles[1], 90);
            var temp_spring = new Spring(circles[2], circles[3], 90);
            var temp_spring_00 = new Spring(circles[0], circles[2], 90);
            var temp_spring_01 = new Spring(circles[1], circles[3], 90);
            var newSpring = new Spring(circles[0], circles[3], 90 * 1.41421f);
            links.Add(newSpring);
            links.Add(temp);
            links.Add(temp_spring);
            links.Add(temp_spring_00);
            links.Add(temp_spring_01);

            // Some Extra Variables
            float FPS;
            float deltaTime;
            string fpsText;
            int subSteps = 2;

            // Setting FrameRate and Starting the Main Loop
            //Raylib.SetTargetFPS(120);
            while (!Raylib.WindowShouldClose())
            {
                // Updating the Extra Variables
                FPS = Raylib.GetFPS();
                fpsText = Convert.ToString(FPS);
                deltaTime = Raylib.GetFrameTime();

                for (int i = 0; i < subSteps; i++)
                {
                    // Iterating through the List of Balls/Circles
                    foreach (Circle circle in circles)
                    {
                        // Updating the Positions of Balls/Circles
                        circle.Update(Width, Height, deltaTime / subSteps);
                        // Looking for and Resolving the Collision between the Balls/Circles
                        foreach (Circle circ in circles)
                        {
                            circle.CollisionResponseCircle(circ, deltaTime / subSteps);
                        }
                        foreach (Line l in lines)
                        {
                            circle.CollisionResponseLine(l, deltaTime / subSteps);
                        }
                    };
                }
                
                foreach (Spring link in links)
                {
                    link.update(deltaTime / subSteps);
                }
                
                // Rendering Section of the Program
                Raylib.BeginDrawing();
                    // Drawing Background
                    Raylib.ClearBackground(Color.DARKGRAY);

                    // Draw the current FrameRate
                    Raylib.DrawText(fpsText, 20, 20, 20, Color.RAYWHITE);

                    foreach (Spring link in links)
                    {
                        link.draw();
                    }

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