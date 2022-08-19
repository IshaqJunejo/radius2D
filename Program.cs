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
            var line01 = new Line();
            line01.p = new Vector2(100, 600);
            line01.q = new Vector2(Width - 150, 800);
            lines.Add(line01);
            var line02 = new Line();
            line02.p = new Vector2(0, 0);
            line02.q = new Vector2(0, Height);
            lines.Add(line02);
            var line03 = new Line();
            line03.p = new Vector2(Width, 0);
            line03.q = new Vector2(Width, Height);
            lines.Add(line03);
            var line04 = new Line();
            line04.p = new Vector2(0, Height);
            line04.q = new Vector2(Width, Height);
            lines.Add(line04);

            foreach (Line line in lines)
            {
                line.UpdateValues();
            }

            // Initializing the List of Balls/Circles
            int numOfCircles = 50;
            List<Circle> circles = new List<Circle>(0);
            for (int i = 0; i < numOfCircles; i++)
            {
                var newCirc = new Circle();

                newCirc.pos = new Vector2(Raylib.GetRandomValue(0, 1000), -25 * i);

                newCirc.vel = new Vector2(0, 0);
                newCirc.force = new Vector2(0, 0);

                newCirc.radius = Raylib.GetRandomValue(8, 25);
                newCirc.mass = (float) Math.Pow(newCirc.radius, 3) / 4;
                if (newCirc.mass == 0)
                {
                    newCirc.inverseMass = 0;
                }else
                {
                    newCirc.inverseMass = 1 / newCirc.mass;
                };
                newCirc.elasticity = 1.0f;

                circles.Add(newCirc);
            }

            // Some Extra Variables
            float FPS;
            string fpsText;

            // Setting FrameRate and Starting the Main Loop
            Raylib.SetTargetFPS(60);
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
                        circle.CollisionResponseCircle(circ);
                    }
                    foreach (Line l in lines)
                    {
                        circle.CollisionResponseLine(l);
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