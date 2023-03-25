namespace Radius2D
{
    class PhysicsLayer
    {
        public List<Circle> circles;
        public List<AABB> boxes;
        public List<Line> lines;
        public List<Spring> springs;

        public PhysicsLayer()
        {
            circles = new List<Circle>(0);
            boxes = new List<AABB>(0);
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
                foreach (AABB box in this.boxes)
                {
                    circle.CollisionResponseBox(box, deltaTime);
                }
                foreach (Line line in this.lines)
                {
                    circle.CollisionResponseLine(line, deltaTime);
                }
            }

            // Iterating through Boxes for updating them
            foreach (AABB box in this.boxes)
            {
                // Updating the Boxes
                box.Update(deltaTime);
                // Checking for Collisions
                foreach (AABB boundBox in this.boxes)
                {
                    box.CollisionResponseBox(boundBox, deltaTime);
                }

                foreach (Circle circ in this.circles)
                {
                    box.CollisionResponseCircle(circ, deltaTime);
                }
            }

            // Iterating through springs for updating them
            foreach (Spring spring in this.springs)
            {
                spring.Update(deltaTime);
            }
        }

        public void Draw()
        {
            // Iterating through circle for rendering them
            foreach (Circle circle in this.circles)
            {
                circle.Draw();
                Raylib_cs.Raylib.DrawCircle((int)circle.pos.X, (int)circle.pos.Y, 2, Raylib_cs.Color.WHITE);
            }

            // Iterating through boxes for rendering them
            foreach (AABB box in this.boxes)
            {
                box.drawBox();
                box.drawBoxLine();
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