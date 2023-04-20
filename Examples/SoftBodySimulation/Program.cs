using Raylib_cs;
using Radius2D;

// Namespace for the Physics Simulation
namespace SoftBodySimulation
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
            Raylib.InitWindow(Width, Height, "Soft Body Simulation");

            // Initializing the Physics Layer
            var layer = new PhysicsLayer();

            // Initializing the List of Lines
            var line01 = new Line(-50, 600, Width - 250, Height - 1);
            layer.lines.Add(line01);
            var line02 = new Line(1, 0, 1, Height);
            layer.lines.Add(line02);
            var line03 = new Line(Width, 0, Width, Height);
            layer.lines.Add(line03);
            var line04 = new Line(0, Height, Width, Height);
            layer.lines.Add(line04);
            var line05 = new Line(Width / 2, 350, Width, 150);
            layer.lines.Add(line05);
            var line06 = new Line(Width / 2, 350, Width, 500);
            layer.lines.Add(line06);

            // Initializing the List of Balls/Circles
            int numOfCircs = 10;
            float radie = 50.0f;
            float springStrength = 3.0f;
            float radiusOfCircs = 10.0f;
            var centerOfCirc = new Circle(700, 150, 0, 0, 15, 20, 0.8f, Color.GRAY);
            layer.circles.Add(centerOfCirc);
            for (float i = 0; i < 360; i += 360 / numOfCircs)
            {
                var newCirc = new Circle(700 + (float)((Math.Cos(i * Math.PI / 180)) * radie), 150 + (float)((Math.Sin(i * Math.PI / 180)) * radie), 0, 0, radiusOfCircs, 20, 0.8f, Color.WHITE);
                layer.circles.Add(newCirc);
            }

            // Initializing the List of Spring connected to Balls/Circles
            for (int i = 0; i < numOfCircs; i++)
            {
                var newLink = new Spring(layer.circles[0], layer.circles[i + 1], radie, 3.0f, 1.0f);
                layer.springs.Add(newLink);
                if (i != 0)
                {
                    var internalLink = new Spring(layer.circles[i], layer.circles[i + 1], (radie * 2.0f * (float) Math.PI) / numOfCircs, springStrength, springStrength / 3.0f);
                    layer.springs.Add(internalLink);
                }
            }
            var extraLink = new Spring(layer.circles[1], layer.circles[numOfCircs], (radie * 2.0f * (float) Math.PI) / numOfCircs, springStrength, springStrength / 3.0f);
            layer.springs.Add(extraLink);

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

                layer.Update(deltaTime);
                
                // Rendering Section of the Program
                Raylib.BeginDrawing();
                    // Drawing Background
                    Raylib.ClearBackground(Color.DARKGRAY);

                    // Draw the current FrameRate
                    Raylib.DrawText(fpsText, 20, 20, 20, Color.RAYWHITE);

                    layer.Draw();
                
                // End of Rendering section
                Raylib.EndDrawing();
            }

            // Closing/Ending the Program
            Raylib.CloseWindow();
        }
    }
}