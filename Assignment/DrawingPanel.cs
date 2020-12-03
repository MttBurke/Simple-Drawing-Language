using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Assignment
{
    /// <summary>
    /// Class used for drawing shapes that has been parsed
    /// </summary>
    public class DrawingPanel
    {

        Pen p = new Pen(Color.Black);
        Graphics g = null;
        public bool Fill = false; //shapes drawn with fill = true will be filled in
        public int PenX = 0;
        public int PenY = 0;
        CommandFactory factory;
       
        public DrawingPanel(Panel InputPanel, Bitmap InputBitmap)
        {
            g = Graphics.FromImage(InputBitmap);
            factory = new CommandFactory(g);
        }

        /// <summary>
        /// Draw a line from current point to input points
        /// </summary>
        /// <param name="InX">new X pos</param>
        /// <param name="InY">new Y pos</param>
        public void DrawLine(int InX, int InY)
        {
            var Line = factory.MakeLine(p, PenX, PenY, InX, InY);
            Line.Execute();
            PenX = InX; //Setting pen position to end position of line
            PenY = InY;
        }

        /// <summary>
        /// Move pen to a new point without drawing
        /// </summary>
        /// <param name="NewX">new X pos</param>
        /// <param name="NewY">new Y pos</param>
        public void MoveTo(int NewX, int NewY)
        {
            PenX = NewX;
            PenY = NewY;
        }

        /// <summary>
        /// Draw a rectangle from current point
        /// </summary>
        /// <param name="InWidth">width of rectangle</param>
        /// <param name="InHeight">height of rectangle</param>
        public void DrawRectangle(int InWidth, int InHeight)
        {
            var Rectangle = factory.MakeRectangle(p, PenX, PenY, InWidth, InHeight, Fill);
            Rectangle.Execute();
        }

        /// <summary>
        /// Draw a triangle with 3 points
        /// </summary>
        /// <param name="Input1">first point</param>
        /// <param name="Input2">second point</param>
        /// <param name="Input3">third point</param>
        public void DrawTriangle(int Input1, int Input2, int Input3)
        {
            var Triangle = factory.MakeTriangle(p, PenX, PenY, Input1, Input2, Input3, Fill);
            Triangle.Execute();
        }
  
        /// <summary>
        /// Draw a circle
        /// </summary>
        /// <param name="InRadius">Size of circle</param>
        public void DrawCircle(int InRadius)
        {
            var Circle = factory.MakeCircle(p, PenX, PenY, InRadius, Fill);
            Circle.Execute();
        }

        /// <summary>
        /// Change pen colour
        /// </summary>
        /// <param name="InColour">new Colour</param>
        public void PenColour(Color InColour)
        {
            p.Color = InColour;
        }

        /// <summary>
        /// Set fill shape on or off
        /// </summary>
        /// <param name="Input">0 or 1</param>
        public void SetFill(int Input)
        {
            if (Input == 1)
            {
                Fill = true;
            }
            else
            {
                Fill = false;
            }
        }

        /// <summary>
        /// Reset pen position to 0,0
        /// </summary>
        public void ResetPen()
        {
            PenX = 0;
            PenY = 0;
        }

        /// <summary>
        /// Clear drawing panel 
        /// </summary>
        public void ClearPanel()
        {
            g.Clear(Color.White);
        }
    }
}
