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
            int numOfCircles = 10;
            List<Circle> circles = new List<Circle>(0);
            for (int i = 0; i < numOfCircles; i++)
            {
                var newCirc = new Circle((30 * i) + 100, 20, Raylib.GetRandomValue(-4, 4), Raylib.GetRandomValue(-4, 4), 20, 30, 0.8f, Color.RAYWHITE);

                circles.Add(newCirc);
            }
            for (var i = 0; i < numOfCircles; i++)
            {
                var newCirc = new Circle(Width / 4, -30 * i, 0, 0, 20, 1, 0.8f, Color.GRAY);
                circles.Add(newCirc);
            }
            circles[0].mass = 0;
            circles[0].inverseMass = 0;
            circles[numOfCircles - 1].mass = 0;
            circles[numOfCircles - 1].inverseMass = 0;
            //circles[0].pos = new Vector2(200, 450);

            List<Spring> links = new List<Spring>(0);
            for (var i = 0; i < numOfCircles - 1; i++)
            {
                var newLink = new Spring(circles[i], circles[i + 1], 10);
                links.Add(newLink);
            }

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