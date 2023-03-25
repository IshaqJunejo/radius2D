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

            // Initiating the Physics Layer
            var Layer = new PhysicsLayer();

            // Adding Lines in the Physics Layer
            var line01 = new Line(Width, 1, 0, 1);
            Layer.lines.Add(line01);
            var line02 = new Line(1, 0, 1, Height);
            Layer.lines.Add(line02);
            var line03 = new Line(Width, 0, Width, Height);
            Layer.lines.Add(line03);
            var line04 = new Line(0, Height, Width, Height);
            Layer.lines.Add(line04);

            // Adding AABB's to Physics Layer
            for (var i = 0; i < 30; i++)
            {
                var newBox = new AABB(Raylib.GetRandomValue(10, 1000), Raylib.GetRandomValue(10, 250), Raylib.GetRandomValue(25, 75), Raylib.GetRandomValue(25, 75), Raylib.GetRandomValue(1, 15), Color.BROWN);
                Layer.boxes.Add(newBox);
            }

            for (var i = 0; i < 5; i++)
            {
                var newCirc = new Circle(Raylib.GetRandomValue(10, 1000), Raylib.GetRandomValue(10, 250), 0, 0, Raylib.GetRandomValue(15, 35), Raylib.GetRandomValue(5, 15), 0.5f, Color.BROWN);
                Layer.circles.Add(newCirc);
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

                // Updating the Physics Layer
                Layer.Update(deltaTime);
                
                // Rendering Section of the Program
                Raylib.BeginDrawing();
                    // Drawing Background
                    Raylib.ClearBackground(Color.DARKGRAY);

                    // Draw the current FrameRate
                    Raylib.DrawText(fpsText, 20, 20, 20, Color.RAYWHITE);

                    // Drawing the Physics Layer's every Element
                    Layer.Draw();
                
                // End of Rendering section
                Raylib.EndDrawing();
            }

            // Closing/Ending the Program
            Raylib.CloseWindow();
        }
    }
}