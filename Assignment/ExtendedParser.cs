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
    class ExtendedParser : Parser
    {

        DrawingPanel dp;
        Panel GraphicsPanel;
        Dictionary<string, int> vars = new Dictionary<string, int>();
        Regex VariableName = new Regex("\\w*\\d*");

        public ExtendedParser(Panel InputPanel, Bitmap InputBitmap) : base(InputPanel, InputBitmap)
        {
            GraphicsPanel = InputPanel;
            dp = new DrawingPanel(InputPanel, InputBitmap);
        }

        public void ParseTextBox(TextBox Inputbox)
        {
            foreach (string l in Inputbox.Lines)
            {
                string[] split = l.Split(' ');
                if (VariableName.IsMatch(split[0])) //If input is any type of text and it has some type of operator a variable will be added or changed
                {
                    string Name;
                    int Value;
                    if (split[1] == "=")
                    {
                        Name = split[0];
                        try
                        {
                            Value = int.Parse(split[2]);
                            vars.Add(Name, Value);
                        }
                        catch
                        {
                            MessageBox.Show("Invalid variable value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if (split[0] == "If")
                    {

                    }
                    else if (split[0] == "Loop")
                    {

                    }
                    else if (split[0] == "Method")
                    {

                    }
                    else
                    {
                        ParseCommand(l.Trim());
                    }
                }
            }
        }

        public override void ParseCommand(string InputCommand)
        {
            string Command = " ";
            string Parameter = " ";
            string[] SplitParameters = { " " };
            int x, y, z;
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
                if (vars.ContainsKey(SplitParameters[0]) && vars.ContainsKey(SplitParameters[1]))
                {
                    x = SetX(vars[SplitParameters[0]].ToString());
                    y = SetY(vars[SplitParameters[1]].ToString());
                    dp.DrawLine(x, y);
                    GraphicsPanel.Refresh();
                }
                else if (vars.ContainsKey(SplitParameters[0]))
                {
                    x = SetX(vars[SplitParameters[0]].ToString());
                    y = SetY(SplitParameters[1]);
                    dp.DrawLine(x, y);
                    GraphicsPanel.Refresh();
                }
                else if(vars.ContainsKey(SplitParameters[1]))
                {
                    x = SetX(SplitParameters[0]);
                    y = SetY(vars[SplitParameters[1]].ToString());
                    dp.DrawLine(x, y);
                    GraphicsPanel.Refresh();
                }
                else
                {
                    x = SetX(SplitParameters[0]);
                    y = SetY(SplitParameters[1]);
                    dp.DrawLine(x, y);
                    GraphicsPanel.Refresh();
                }
            }
            else if (Command.Trim() == "Rectangle")
            {
                if (vars.ContainsKey(SplitParameters[0]) && vars.ContainsKey(SplitParameters[1]))
                {
                    x = SetX(vars[SplitParameters[0]].ToString());
                    y = SetY(vars[SplitParameters[1]].ToString());
                    dp.DrawRectangle(x, y);
                    GraphicsPanel.Refresh();
                }
                else if (vars.ContainsKey(SplitParameters[0]))
                {
                    x = SetX(vars[SplitParameters[0]].ToString());
                    y = SetY(SplitParameters[1]);
                    dp.DrawRectangle(x, y);
                    GraphicsPanel.Refresh();
                }
                else if (vars.ContainsKey(SplitParameters[1]))
                {
                    x = SetX(SplitParameters[0]);
                    y = SetY(vars[SplitParameters[1]].ToString());
                    dp.DrawRectangle(x, y);
                    GraphicsPanel.Refresh();
                }
                else
                {
                    x = SetX(SplitParameters[0]);
                    y = SetY(SplitParameters[1]);
                    dp.DrawRectangle(x, y);
                    GraphicsPanel.Refresh();
                }
            }
            else if (Command.Trim() == "moveTo")
            {
                if (vars.ContainsKey(SplitParameters[0]) && vars.ContainsKey(SplitParameters[1]))
                {
                    x = SetX(vars[SplitParameters[0]].ToString());
                    y = SetY(vars[SplitParameters[1]].ToString());
                    dp.MoveTo(x, y);
                    GraphicsPanel.Refresh();
                }
                else if (vars.ContainsKey(SplitParameters[0]))
                {
                    x = SetX(vars[SplitParameters[0]].ToString());
                    y = SetY(SplitParameters[1]);
                    dp.MoveTo(x, y);
                    GraphicsPanel.Refresh();
                }
                else if (vars.ContainsKey(SplitParameters[1]))
                {
                    x = SetX(SplitParameters[0]);
                    y = SetY(vars[SplitParameters[1]].ToString());
                    dp.MoveTo(x, y);
                    GraphicsPanel.Refresh();
                }
                else
                {
                    x = SetX(SplitParameters[0]);
                    y = SetY(SplitParameters[1]);
                    dp.MoveTo(x, y);
                    GraphicsPanel.Refresh();
                }
            }
            else if (Command.Trim() == "Circle")
            {
                if (vars.ContainsKey(SplitParameters[0]))
                {
                    x = SetX(vars[SplitParameters[0]].ToString());
                    dp.DrawCircle(x);
                    GraphicsPanel.Refresh();
                }
                else
                {
                    x = SetX(SplitParameters[0]);
                    dp.DrawCircle(x);
                    GraphicsPanel.Refresh();
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
                if (vars.ContainsKey(SplitParameters[0]) && vars.ContainsKey(SplitParameters[1]) && vars.ContainsKey(SplitParameters[2]))
                {
                    x = SetX(vars[SplitParameters[0]].ToString());
                    y = SetY(vars[SplitParameters[1]].ToString());
                    z = SetY(vars[SplitParameters[2]].ToString());
                    dp.DrawTriangle(x, y, z);
                    GraphicsPanel.Refresh();
                }
                else if (vars.ContainsKey(SplitParameters[0]) && vars.ContainsKey(SplitParameters[1]))
                {
                    x = SetX(vars[SplitParameters[0]].ToString());
                    y = SetY(vars[SplitParameters[1]].ToString());
                    z = vars[SplitParameters[2]];
                    dp.DrawTriangle(x, y, z);
                    GraphicsPanel.Refresh();
                }
                else if (vars.ContainsKey(SplitParameters[1]) && vars.ContainsKey(SplitParameters[2]))
                {
                    x = SetX(SplitParameters[0]);
                    y = SetY(vars[SplitParameters[1]].ToString());
                    z = SetY(vars[SplitParameters[2]].ToString());
                    dp.DrawTriangle(x, y, z);
                    GraphicsPanel.Refresh();
                }
                else if (vars.ContainsKey(SplitParameters[0]) && vars.ContainsKey(SplitParameters[2]))
                {
                    x = SetX(vars[SplitParameters[0]].ToString());
                    y = SetY(SplitParameters[1]);
                    z = SetY(vars[SplitParameters[2]].ToString());
                    dp.DrawTriangle(x, y, z);
                    GraphicsPanel.Refresh();
                }
                else if (vars.ContainsKey(SplitParameters[0]))
                {
                    x = SetX(vars[SplitParameters[0]].ToString());
                    y = SetY(SplitParameters[1]);
                    z = vars[SplitParameters[2]];
                    dp.DrawTriangle(x, y, z);
                    GraphicsPanel.Refresh();
                }
                else if (vars.ContainsKey(SplitParameters[1]))
                {
                    x = SetX(SplitParameters[0]);
                    y = SetY(vars[SplitParameters[1]].ToString());
                    z = vars[SplitParameters[2]];
                    dp.DrawTriangle(x, y, z);
                    GraphicsPanel.Refresh();
                }
                else if (vars.ContainsKey(SplitParameters[2]))
                {
                    x = SetX(SplitParameters[0]);
                    y = SetY(SplitParameters[1]);
                    z = SetY(vars[SplitParameters[2]].ToString());
                    dp.DrawTriangle(x, y, z);
                    GraphicsPanel.Refresh();
                }
                else
                {
                    x = SetX(SplitParameters[0]);
                    y = SetY(SplitParameters[1]);
                    z = int.Parse(SplitParameters[2]);
                    dp.DrawTriangle(x, y, z);
                    GraphicsPanel.Refresh();
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

        private int SetX(string Input)
        {
            return int.Parse(Input);
        }

        private int SetY(string Input)
        {
            return int.Parse(Input);
        }
    }
}
