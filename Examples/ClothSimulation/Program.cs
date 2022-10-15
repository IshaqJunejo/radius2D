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
            Raylib.InitWindow(Width, Height, "Cloth Simulation");

            // Initializing the List of Lines
            List<Line> lines = new List<Line>(0);
            var line01 = new Line(1, 1, Width, 1);
            lines.Add(line01);
            var line02 = new Line(1, 0, 1, Height);
            lines.Add(line02);
            var line03 = new Line(Width, 0, Width, Height);
            lines.Add(line03);
            var line04 = new Line(0, Height, Width, Height);
            lines.Add(line04);

            // Initializing the List of Balls/Circles
            int numOfCircs = 13;
            float gapBetweenCircs = 30.0f;
            List<Circle> circles = new List<Circle>(0);
            for (int i = 0; i < numOfCircs; i++)
            {
                for (int j = 0; j < numOfCircs; j++)
                {
                    if (j == 0)
                    {
                        var circ = new Circle(((Width / 2) - (numOfCircs * gapBetweenCircs / 2)) + (gapBetweenCircs * i), 50 + (gapBetweenCircs * j * 1.5f), 0, 0, 0, 0, 0.0f, Color.GRAY);
                        circles.Add(circ);
                        
                    }else
                    {
                        var circ = new Circle(((Width / 2) - (numOfCircs * gapBetweenCircs / 2)) + (gapBetweenCircs * i), 50 + (gapBetweenCircs * j * 1.5f), 0, 0, 0, 150, 0.0f, Color.WHITE);
                        circles.Add(circ);
                    }
                }
            }

            // Initializing the List of Spring connected to Balls/Circles
            List<Spring> links = new List<Spring>(0);
            for (int i = 0; i < numOfCircs - 0; i++)
            {
                for (int j = 0; j < numOfCircs - 1; j++)
                {
                    int index = (i * numOfCircs) + j;
                    var joint = new Spring(circles[index], circles[index + 1], gapBetweenCircs * 1.5f, 70, 40);
                    links.Add(joint);

                    if (index < (numOfCircs * numOfCircs) - numOfCircs)
                    {
                        var joint2 = new Spring(circles[index], circles[index + numOfCircs], gapBetweenCircs, 70, 40);
                        links.Add(joint2);
                    }
                }
                if (i <= numOfCircs - 2)
                {
                    int index = (i * numOfCircs) + numOfCircs - 1;
                    var joint3 = new Spring(circles[index], circles[index + numOfCircs], gapBetweenCircs, 70, 40);
                    links.Add(joint3);
                }
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
                    // Applying Force to Circles (Wind Force) to Showcase the Cloth Simulation Effect
                    if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
                    {
                        circle.force.X += 0.05f * circle.mass;
                    }else if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
                    {
                        circle.force.X -= 0.05f * circle.mass;
                    }

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
                
                Spring toBeRemoved = new Spring(circles[0], circles[1], 1, 1, 1);
                foreach (Spring link in links)
                {
                    link.update(deltaTime);

                    if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT) && Collision.SpringToPoint(link, Raylib.GetMousePosition()))
                    {
                        toBeRemoved = link;
                    }
                }
                if (links.Contains(toBeRemoved))
                {
                    links.Remove(toBeRemoved);
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