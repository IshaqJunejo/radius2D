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

        public void UpdateLayer(float deltaTime)
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
            }

            // Iterating through springs for updating them
            foreach (Spring spring in this.springs)
            {
                spring.update(deltaTime);
            }
        }

        public void DrawLayer()
        {
            // Iterating through circle for rendering them
            foreach (Circle circle in this.circles)
            {
                circle.Draw();
            }

            // Iterating through lines for rendering them
            foreach (Line line in this.lines)
            {
                line.DrawLine();
            }

            // Iterating through springs for rendering them
            foreach (Spring spring in this.springs)
            {
                spring.draw();
            }
        }
    }
}