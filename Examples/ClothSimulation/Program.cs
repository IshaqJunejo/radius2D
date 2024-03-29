﻿using Raylib_cs;
using Radius2D;

// Namespace for the Physics Simulation
namespace ClothSimulation
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

            var layer = new PhysicsLayer();

            // Initializing the Lines
            var line01 = new Line(1, 1, Width, 1);
            layer.lines.Add(line01);
            var line02 = new Line(1, 0, 1, Height);
            layer.lines.Add(line02);
            var line03 = new Line(Width, 0, Width, Height);
            layer.lines.Add(line03);
            var line04 = new Line(0, Height, Width, Height);
            layer.lines.Add(line04);

            // Initializing the Balls/Circles
            int numOfCircs = 13;
            float gapBetweenCircs = 30.0f;
            float verticalGap = 1.5f;
            float springStrength = 70.0f;
            for (int i = 0; i < numOfCircs; i++)
            {
                for (int j = 0; j < numOfCircs; j++)
                {
                    if (j == 0)
                    {
                        var circ = new Circle(((Width / 2) - (numOfCircs * gapBetweenCircs / 2)) + (gapBetweenCircs * i), 50 + (gapBetweenCircs * verticalGap * j), 0, 0, 0, 0, 0.0f, Color.GRAY);
                        layer.circles.Add(circ);
                        
                    }else
                    {
                        var circ = new Circle(((Width / 2) - (numOfCircs * gapBetweenCircs / 2)) + (gapBetweenCircs * i), 50 + (gapBetweenCircs * verticalGap * j), 0, 0, 0, 150, 0.0f, Color.WHITE);
                        layer.circles.Add(circ);
                    }
                }
            }

            // Initializing the Spring connected to Balls/Circles
            for (int i = 0; i < numOfCircs - 0; i++)
            {
                for (int j = 0; j < numOfCircs - 1; j++)
                {
                    int index = (i * numOfCircs) + j;
                    var joint = new Spring(layer.circles[index], layer.circles[index + 1], gapBetweenCircs * 1.5f, springStrength, springStrength / 3.0f);
                    layer.springs.Add(joint);

                    if (index < (numOfCircs * numOfCircs) - numOfCircs)
                    {
                        var joint2 = new Spring(layer.circles[index], layer.circles[index + numOfCircs], gapBetweenCircs, springStrength, springStrength / 3.0f);
                        layer.springs.Add(joint2);
                    }
                }
                if (i <= numOfCircs - 2)
                {
                    int index = (i * numOfCircs) + numOfCircs - 1;
                    var joint3 = new Spring(layer.circles[index], layer.circles[index + numOfCircs], gapBetweenCircs, springStrength, springStrength / 3.0f);
                    layer.springs.Add(joint3);
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

                // The Wind effect for our Cloth
                foreach (Circle circ in layer.circles)
                {
                    if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
                    {
                        circ.force.X -= 0.05f * deltaTime * 120 * circ.mass;
                    }else if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
                    {
                        circ.force.X += 0.05f * deltaTime * 120 * circ.mass;
                    }
                }

                // Iterating through springs for updating them
                Spring toBeRemoved = new Spring(layer.circles[0], layer.circles[1], 1, 1, 1);
                foreach (Spring spring in layer.springs)
                {
                    if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT) && Collision.SpringToPoint(spring, Raylib.GetMousePosition()))
                    {
                        toBeRemoved = spring;
                    }
                }

                if (layer.springs.Contains(toBeRemoved))
                {
                    layer.springs.Remove(toBeRemoved);
                }

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