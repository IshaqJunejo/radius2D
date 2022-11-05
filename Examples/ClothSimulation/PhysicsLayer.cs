using Raylib_cs;

namespace Radius2D
{
    class PhysicsLayer
    {
        public List<Circle> circles;
        public List<Line> lines;
        public List<Spring> springs;

        public PhysicsLayer()
        {
            circles = new List<Circle>(0);
            lines = new List<Line>(0);
            springs = new List<Spring>(0);
        }

        public void Update(float deltaTime)
        {
            // Iterating through the list of Circles
            foreach (Circle circle in this.circles)
            {
                // Updating the Circles
                circle.Update(deltaTime);
                // Checking for Collisions
                foreach (Circle circ in this.circles)
                {
                    circle.CollisionResponseCircle(circ, deltaTime);
                }
                foreach (Line line in this.lines)
                {
                    circle.CollisionResponseLine(line, deltaTime);
                }

                if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
                {
                    circle.force.X -= 0.05f * deltaTime * 120 * circle.mass;
                }else if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
                {
                    circle.force.X += 0.05f * deltaTime * 120 * circle.mass;
                }
            }

            // Iterating through springs for updating them
            Spring toBeRemoved = new Spring(this.circles[0], this.circles[1], 1, 1, 1);
            foreach (Spring spring in this.springs)
            {
                spring.Update(deltaTime);

                if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT) && Collision.SpringToPoint(spring, Raylib.GetMousePosition()))
                {
                    toBeRemoved = spring;
                }
            }

            if (springs.Contains(toBeRemoved))
            {
                springs.Remove(toBeRemoved);
            }
        }

        public void Draw()
        {
            // Iterating through circle for rendering them
            foreach (Circle circle in this.circles)
            {
                circle.Draw();
            }

            // Iterating through lines for rendering them
            foreach (Line line in this.lines)
            {
                line.Draw();
            }

            // Iterating through springs for rendering them
            foreach (Spring spring in this.springs)
            {
                spring.Draw();
            }
        }
    }
}