using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Assignment
{
    class Parser
    {

        DrawingPanel dp;
        Panel GraphicsPanel;
        int x, y, z;
        string[] SplitParameters;
        string Command = " ";
        string Parameter;

        public Parser(Panel InputPanel, Bitmap InputBitmap)
        {
            GraphicsPanel = InputPanel;
            dp = new DrawingPanel(GraphicsPanel, InputBitmap);
        }

        public virtual void ParseCommand(string InputCommand)
        {
            /*
             * Splitting input text into command and parameters
             * */
            try
            {
                Command = InputCommand.Substring(0, InputCommand.IndexOf(' '));
                Parameter = InputCommand.Substring(InputCommand.IndexOf(' ') + 1);
                SplitParameters = Parameter.Split(',');
            }
            catch
            {
                MessageBox.Show("Invalid input detected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (Command.Trim() == "drawTo")
            {
                try
                {
                    x = SetX();
                    y = SetY();
                    dp.DrawLine(x, y);
                    GraphicsPanel.Refresh();
                }
                catch
                {
                    MessageBox.Show("Invalid input detected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (Command.Trim() == "Rectangle")
            {
                try
                {
                    x = SetX();
                    y = SetY();
                    dp.DrawRectangle(x, y);
                    GraphicsPanel.Refresh();
                }
                catch
                {
                    MessageBox.Show("Invalid input detected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (Command.Trim() == "moveTo")
            {
                try
                {
                    x = SetX();
                    y = SetY();
                    dp.MoveTo(x, y);
                    GraphicsPanel.Refresh();
                }
                catch
                {
                    MessageBox.Show("Invalid input detected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (Command.Trim() == "Circle")
            {
                try
                {
                    x = int.Parse(Parameter);
                    dp.DrawCircle(x);
                    GraphicsPanel.Refresh();
                }
                catch
                {
                    MessageBox.Show("Invalid input detected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else if (Command.Trim() == "Fill")
            {
                if (Parameter.ToLower() == "on")
                {
                    dp.SetFill(1);
                }
                else
                {
                    dp.SetFill(0);
                }
            }
            else if (Command.Trim() == "Triangle")
            {
                try
                {
                    x = SetX();
                    y = SetY();
                    z = int.Parse(SplitParameters[2]);
                    dp.DrawTriangle(x, y, z);
                    GraphicsPanel.Refresh();
                }
                catch
                {
                    MessageBox.Show("Invalid input detected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (Command.Trim() == "Colour")
            {
                if (Parameter == "black")
                {
                    dp.PenColour(Color.Black);
                }
                else if (Parameter == "red")
                {
                    dp.PenColour(Color.Red);
                }
                else if (Parameter == "green")
                {
                    dp.PenColour(Color.Green);
                }
                else if (Parameter == "blue")
                {
                    dp.PenColour(Color.Blue);
                }
                else if (Parameter == "yellow")
                {
                    dp.PenColour(Color.Yellow);
                }
            }
        }

        private int SetX()
        {
            /*
             * First part of parameter to be parsed into an int
             */
            return int.Parse(SplitParameters[0]);
        }

        private int SetY()
        {
            /*
             * SEcond part of parametere parsed into an int
             */
            return int.Parse(SplitParameters[1]);
        }
    }
}
