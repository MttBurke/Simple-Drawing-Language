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
        List<Variables> variables = new List<Variables>();
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
                            variables.Add(new Variables(Name, Value));
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
    }
}
