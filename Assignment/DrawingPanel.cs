using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Assignment
{
    class DrawingPanel
    {

        Pen p = new Pen(Color.Black);
        Graphics g = null;
        bool Fill = false; //shapes drawn with fill = true will be filled in
        int PenX = 0;
        int PenY = 0;

        public DrawingPanel(Panel InputPanel, Bitmap InputBitmap)
        {
            g = Graphics.FromImage(InputBitmap);
        }

        public void DrawLine(int InX, int InY)
        {
            g.DrawLine(p, PenX, PenY, InX, InY);
            PenX = InX; //Setting pen position to end position of line
            PenY = InY;
        }

        public void MoveTo(int NewX, int NewY)
        {
            PenX = NewX;
            PenY = NewY;
        }

        public void DrawRectangle(int InWidth, int InHeight)
        {
            if(Fill == true)
            {
                g.FillRectangle(p.Brush, PenX, PenY, InWidth, InHeight);
            }
            else
            {
                g.DrawRectangle(p, PenX, PenY, InWidth, InHeight);
            }        
        }

        public void DrawTriangle(int Input1, int Input2, int Input3)
        {
            
            Point Point1 = new Point(PenX - Input1, PenY + Input1);
            Point Point2 = new Point(PenX, PenY - Input2);
            Point Point3 = new Point(PenX + Input3, PenY + Input3);
            Point[] Triangle =
            {
                Point1, Point2, Point3
            };
            if (Fill == true)
            {
                g.FillPolygon(p.Brush, Triangle);
            }
            else
            {
                g.DrawPolygon(p, Triangle);
            }
        }

        public void DrawCircle(int InRadius)
        {
            if(Fill == true)
            {
                g.FillEllipse(p.Brush, PenX, PenY, InRadius, InRadius);
            }
            else
            {
                g.DrawEllipse(p, PenX, PenY, InRadius, InRadius);
            }          
        }

        public void PenColour(Color InColour)
        {
            p.Color = InColour;
        }

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
    }
}
