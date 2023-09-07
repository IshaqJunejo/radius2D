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
            Line line01;
            line01 = Line.makeLine(Width, 1, 0, 1);
            Layer.lines.Add(line01);

            Line line02;
            line02 = Line.makeLine(1, 0, 1, Height);
            Layer.lines.Add(line02);

            Line line03;
            line03 = Line.makeLine(Width, Height, Width, 0);
            Layer.lines.Add(line03);

            Line line04;
            line04 = Line.makeLine(0, Height, Width, Height);
            Layer.lines.Add(line04);

            Line line05;
            line05 = Line.makeLine(120, 230, 1000, 750);
            Layer.lines.Add(line05);

            // Adding Circles to the Physics Layer
            for (var i = 0; i < 5; i++)
            {
                var newCirc = new Circle(Raylib.GetRandomValue(10, 1000), Raylib.GetRandomValue(30, 950), 0, 0, Raylib.GetRandomValue(15, 35), Raylib.GetRandomValue(5, 15), 0.5f, Color.BROWN);
                //var newCirc = new Circle(Raylib.GetRandomValue(30, 1020), Raylib.GetRandomValue(30, 950), 0, 0, Raylib.GetRandomValue(15, 35), 0.0f, 0.5f, Color.BROWN);
                Layer.circles.Add(newCirc);
            }

            // Adding Polygons to the Physics Layer
            for (var i = 0; i < 50; i++)
            {
                var box = new Polygon(Raylib.GetRandomValue(30, 1020), Raylib.GetRandomValue(30, 920), Raylib.GetRandomValue(3, 8), Raylib.GetRandomValue(12, 30), 1.0f, 0.0f);
                box.angle = Raylib.GetRandomValue(-10, 10);
                Layer.polygons.Add(box);
            }

            /*foreach (var line in Layer.lines)
            {
                line.Update(1 / 12);
            }*/

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
                
                //Console.WriteLine(Layer.circles[0].vel);

                // Updating the Physics Layer
                Layer.Update(deltaTime);

                foreach (var line in Layer.lines)
                {
                    line.Update(deltaTime);
                }
                
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