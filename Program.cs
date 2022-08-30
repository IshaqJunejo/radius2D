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

            // Initializing the List of Balls/Circles and Springs
            int numOfCircs = 10;
            float radie = 50;
            List<Circle> circles = new List<Circle>(0);
            var centerOfCirc = new Circle(500, 150, 0, 0, 15, 20, 0.8f, Color.GRAY);
            circles.Add(centerOfCirc);
            for (float i = 0; i < 360; i += 360 / numOfCircs)
            {
                var newCirc = new Circle(500 + (float)((Math.Cos(i * Math.PI / 180)) * radie), 150 + (float)((Math.Sin(i * Math.PI / 180)) * radie), 0, 0, 10, 20, 0.8f, Color.WHITE);
                circles.Add(newCirc);
            }

            List<Spring> links = new List<Spring>(0);
            for (int i = 0; i < numOfCircs; i++)
            {
                var newLink = new Spring(circles[0], circles[i + 1], radie, 25.0f, 5.0f);
                links.Add(newLink);
                if (i != 0)
                {
                    var internalLink = new Spring(circles[i], circles[i + 1], (radie * 2.0f * (float) Math.PI) / numOfCircs, 25.0f, 5.0f);
                    links.Add(internalLink);
                }
            }
            var extraLink = new Spring(circles[1], circles[numOfCircs], (radie * 2.0f * (float) Math.PI) / numOfCircs, 25.0f, 5.0f);
            links.Add(extraLink);

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