using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Assignment
{
    class Commands
    {
        public interface ICommands
        {
            void Execute();
        }

        /// <summary>
        /// Design Pattern for the different shapes
        /// </summary>
        public class Circle:ICommands
        {
            private int x;
            private int y;
            private Pen pen;
            private int radius;
            private bool fill;
            private Graphics graphics;

            public Circle(Pen pen, int x, int y, int radius, bool fill, Graphics graphics)
            {
                this.x = x;
                this.y = y;
                this.pen = pen;
                this.radius = radius;
                this.fill = fill;
                this.graphics = graphics;
            }

            public void Execute()
            {
                if (fill)
                {
                    graphics.FillEllipse(pen.Brush, x, y, radius, radius);
                }
                else
                {
                    graphics.DrawEllipse(pen, x, y, radius, radius);
                }
            }
        }

        public class Triangle:ICommands
        {
            private int x;
            private int y;
            private Pen pen;
            private int p1, p2, p3;
            private bool fill;
            private Graphics graphics;

            public Triangle(Pen pen, int x, int y, int p1, int p2, int p3, bool fill, Graphics graphics)
            {
                this.x = x;
                this.y = y;
                this.pen = pen;
                this.p1 = p1;
                this.p2 = p2;
                this.p3 = p3;
                this.fill = fill;
                this.graphics = graphics;
            }

            public void Execute()
            {
                Point Point1 = new Point(x - p1, y + p1);
                Point Point2 = new Point(x, y - p2);
                Point Point3 = new Point(x + p3, y + p3);
                Point[] Triangle =
                {
                Point1, Point2, Point3
                };

                if (fill)
                {
                    graphics.FillPolygon(pen.Brush, Triangle);
                }
                else
                {
                    graphics.DrawPolygon(pen, Triangle);
                }
            }
        }

        public class Line:ICommands
        {
            int x, y;
            Pen pen;
            Graphics graphics;
            int p1, p2;

            public Line(Pen pen, int x, int y, int p1, int p2, Graphics graphics)
            {
                this.pen = pen;
                this.x = x;
                this.y = y;
                this.p1 = p1;
                this.p2 = p2;
                this.graphics = graphics;
            }

            public void Execute()
            {
                graphics.DrawLine(pen, x, y, p1, p2);
            }
        }

        public class Rectangle:ICommands
        {
            int x, y;
            Pen pen;
            Graphics graphics;
            int p1, p2;
            bool fill;
            
            public Rectangle(Pen pen, int x, int y, int p1, int p2, bool fill, Graphics graphics)
            {
                this.x = x;
                this.y = y;
                this.pen = pen;
                this.p1 = p1;
                this.p2 = p2;
                this.fill = fill;
                this.graphics = graphics;
            }

            public void Execute()
            {
                if (fill)
                {
                    graphics.FillRectangle(pen.Brush, x, y, p1, p2);
                }
                else
                {
                    graphics.DrawRectangle(pen, x, y, p1, p2);
                }
            }
        }
    }
}
