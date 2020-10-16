using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Assignment
{
    public partial class Form1 : Form
    {

        DrawingPanel dp = null;
        Regex DrawlineRegex = new Regex("drawTo (\\d+,\\d+)");
        Regex RectangleRegex = new Regex("rectangle (\\d+,\\d+)");
        Regex MovePenRegex = new Regex("moveTo (\\d+,\\d+)");
        Regex CircleRegex = new Regex("circle (\\d+)");
        Regex FillRegex = new Regex("fill (on|off)");
        Regex TriangleRegex = new Regex("triangle (\\d+,\\d+,\\d+)");
        Regex ColourRegex = new Regex("pen (black|red|green|blue|yellow)");
        Bitmap MyBitmap = null;
        string[] SplitParameters;
        int CommandCase = 0;
        string TextInput, TempString;
        int x, y, z;

        public Form1()
        {
            InitializeComponent();
            MyBitmap = new Bitmap(GraphicsPanel.Width, GraphicsPanel.Height); //Initialising new bitmap to be drawn on
            dp = new DrawingPanel(GraphicsPanel, MyBitmap); //drawing panel for drawing onto bitmap
        }
        
        private void TxtBox_Input_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                /*
                 * TextInput to be split into command and parameter
                 * Tempstring stores parameters which is split again if more than 1 parameter
                 */
                TextInput = TxtBox_Input.Text;
                TempString = TextInput.Substring(TextInput.IndexOf(' ') + 1);
                SplitParameters = TempString.Split(',');

                if(DrawlineRegex.IsMatch(TextInput))
                {
                    CommandCase = 1;
                    try
                    {
                        x = SetX();
                        y = SetY();
                        OutputText(TextInput);
                        GraphicsPanel.Refresh();
                    }
                    catch
                    {
                        MessageBox.Show("Invalid input detected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if(RectangleRegex.IsMatch(TextInput))
                {
                    CommandCase = 2;
                    try
                    {
                        x = SetX();
                        y = SetY();
                        OutputText(TextInput);
                        GraphicsPanel.Refresh();
                    }
                    catch
                    {
                        MessageBox.Show("Invalid input detected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if(MovePenRegex.IsMatch(TextInput))
                {
                    CommandCase = 3;
                    try
                    {
                        x = SetX();
                        y = SetY();
                        OutputText(TextInput);
                        GraphicsPanel.Refresh();
                    }
                    catch
                    {
                        MessageBox.Show("Invalid input detected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if(CircleRegex.IsMatch(TextInput))
                {
                    CommandCase = 4;
                    try
                    {
                        x = int.Parse(TempString);
                        OutputText(TextInput);
                        GraphicsPanel.Refresh();
                    }
                    catch
                    {
                        MessageBox.Show("Invalid input detected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                                
                }
                else if (FillRegex.IsMatch(TextInput))
                {
                    OutputText(TextInput);
                    if(TempString == "on")
                    {
                        dp.SetFill(1);
                    }
                    else
                    {
                        dp.SetFill(0);
                    }
                }
                else if(TriangleRegex.IsMatch(TextInput))
                {
                    CommandCase = 5;
                    try
                    {
                        OutputText(TextInput);
                        x = SetX();
                        y = SetY();
                        z = int.Parse(SplitParameters[2]);
                        GraphicsPanel.Refresh();
                    }
                    catch
                    {
                        MessageBox.Show("Invalid input detected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if(ColourRegex.IsMatch(TextInput))
                {
                    OutputText(TextInput);
                    if (TempString == "black")
                    {
                        dp.PenColour(Color.Black);
                    }
                    else if (TempString == "red") 
                    {
                        dp.PenColour(Color.Red);
                    }
                    else if(TempString == "green")
                    {
                        dp.PenColour(Color.Green);
                    }
                    else if(TempString == "blue")
                    {
                        dp.PenColour(Color.Blue);
                    }
                    else if(TempString == "yellow")
                    {
                        dp.PenColour(Color.Yellow);
                    }
                }
                else if(TextInput.Trim() == "run")
                {
                    CommandCase = 0;
                    Language l = new Language(TxtBox_Code, GraphicsPanel, MyBitmap);
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

        private void OutputText(string TextInput)
        {
            TxtBox_Output.Text += TextInput;
            TxtBox_Output.AppendText(Environment.NewLine);
            TxtBox_Input.Clear();
        }

        private void GraphicsPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            switch (CommandCase)
            {
                case 1:
                    dp.DrawLine(x, y);
                    break;
                case 2:
                    dp.DrawRectangle(x, y);
                    break;
                case 3:
                    dp.MoveTo(x, y);
                    break;
                case 4:
                    dp.DrawCircle(x);
                    break;
                case 5:
                    dp.DrawTriangle(x, y, z);
                    break;
                default:
                    break;
            }
            g.DrawImageUnscaled(MyBitmap, 0, 0);
        }

    }
}
