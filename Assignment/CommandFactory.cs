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

        /// <summary>
        /// Create a factory for shapes
        /// </summary>
        /// <param name="graphics">Graphics that will be drawn on.</param>
        public CommandFactory(Graphics graphics)
        {
            this.graphics = graphics;
        }

        /// <summary>
        /// Draw a new Circle
        /// </summary>
        /// <param name="pen">Pen colour to use</param>
        /// <param name="x">Initial x pos</param>
        /// <param name="y">Initial y pos</param>
        /// <param name="radius">Radius of circle</param>
        /// <param name="fill">Fill on or off</param>
        /// <returns></returns>
        public Circle MakeCircle(Pen pen, int x, int y, int radius, bool fill)
        {
            return new Circle(pen, x, y, radius, fill, graphics);
        }

        /// <summary>
        /// Make a new Rectangle
        /// </summary>
        /// <param name="pen">Pen colour to use</param>
        /// <param name="x">Initial x pos</param>
        /// <param name="y">Initial y pos</param>
        /// <param name="p1">Width</param>
        /// <param name="p2">Height</param>
        /// <param name="fill">Fill on or off</param>
        /// <returns></returns>
        public Commands.Rectangle MakeRectangle(Pen pen, int x, int y, int p1, int p2, bool fill)
        {
            return new Commands.Rectangle(pen, x, y, p1, p2, fill, graphics);
        }

        /// <summary>
        /// Make a new Triangle
        /// </summary>
        /// <param name="pen">Pen colour to use</param>
        /// <param name="x">Initial x pos</param>
        /// <param name="y">Initial y pos</param>
        /// <param name="p1">Side 1</param>
        /// <param name="p2">Side 2</param>
        /// <param name="p3">Side 3</param>
        /// <param name="fill">Fill on or off</param>
        /// <returns></returns>
        public Triangle MakeTriangle(Pen pen, int x, int y, int p1, int p2, int p3, bool fill)
        {
            return new Triangle(pen, x, y, p1, p2, p3, fill, graphics);
        }

        /// <summary>
        /// Make a new Line
        /// </summary>
        /// <param name="pen">Pen colour to use</param>
        /// <param name="x">Initial x pos</param>
        /// <param name="y">Initial y pos</param>
        /// <param name="p1">New x pos</param>
        /// <param name="p2">New y pos</param>
        /// <param name="fill">Fill on or off</param>
        /// <returns></returns>
        public Line MakeLine(Pen pen, int x, int y, int p1, int p2)
        {
            return new Line(pen, x, y, p1, p2, graphics);
        }
    }
}
