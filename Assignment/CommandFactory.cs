using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using static Assignment.Commands;

namespace Assignment
{
    /// <summary>
    /// Factory class for Commands
    /// </summary>
    class CommandFactory
    {
        Graphics graphics;

        public CommandFactory(Graphics graphics)
        {
            this.graphics = graphics;
        }

        public Circle MakeCircle(Pen pen, int x, int y, int radius, bool fill)
        {
            return new Circle(pen, x, y, radius, fill, graphics);
        }

        public Commands.Rectangle MakeRectangle(Pen pen, int x, int y, int p1, int p2, bool fill)
        {
            return new Commands.Rectangle(pen, x, y, p1, p2, fill, graphics);
        }

        public Triangle MakeTriangle(Pen pen, int x, int y, int p1, int p2, int p3, bool fill)
        {
            return new Triangle(pen, x, y, p1, p2, p3, fill, graphics);
        }

        public Line MakeLine(Pen pen, int x, int y, int p1, int p2)
        {
            return new Line(pen, x, y, p1, p2, graphics);
        }
    }
}
