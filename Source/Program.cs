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
            List<AABB> boxes = new List<AABB>(0);

            for (var i = 0; i < 500; i++)
            {
                AABB newBox = new AABB(Raylib.GetRandomValue(10, 1000), Raylib.GetRandomValue(10, 250), Raylib.GetRandomValue(5, 15), Raylib.GetRandomValue(5, 15), 1, Color.BROWN);
                boxes.Add(newBox);
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

                foreach (AABB box in boxes)
                {
                    box.Update(deltaTime);
                }

                foreach (AABB box in boxes)
                {
                    foreach (AABB BoundingBox in boxes)
                    {
                        box.CollisionResponseBox(BoundingBox, deltaTime);
                    }
                }
                
                // Rendering Section of the Program
                Raylib.BeginDrawing();
                    // Drawing Background
                    Raylib.ClearBackground(Color.DARKGRAY);

                    // Draw the current FrameRate
                    Raylib.DrawText(fpsText, 20, 20, 20, Color.RAYWHITE);

                    // Drawing the Physics Layer's every Element
                    Layer.Draw();

                    foreach (AABB box in boxes)
                    {
                        //box.drawBox();
                        box.drawBoxLine();
                    }
                
                // End of Rendering section
                Raylib.EndDrawing();
            }

            // Closing/Ending the Program
            Raylib.CloseWindow();
        }
    }
}