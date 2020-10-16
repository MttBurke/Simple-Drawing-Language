using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Assignment
{
    class Language
    {
        TextBox ProgramCode;
        DrawingPanel dp;
        Panel gp;
        Dictionary<string, int> Variables = new Dictionary<string, int>();

        public Language(TextBox InputProgram, Panel Panel, Bitmap bitmap)
        {
            dp = new DrawingPanel(Panel, bitmap);
            gp = Panel;
            ProgramCode = InputProgram;

            ParseLine();
        }

        private void ParseLine()
        {
            foreach(string Line in ProgramCode.Lines)
            {
                string Command = Line.Substring(0, Line.IndexOf(' '));
                string Parameter = Line.Substring(Line.IndexOf(' ') + 1);
                string[] SplitParameters = Parameter.Split(',');

                if (Command.Trim() == "drawTo")
                {
                    int x = int.Parse(SplitParameters[0]);
                    int y = int.Parse(SplitParameters[1]);
                    dp.DrawLine(x, y);
                    gp.Refresh();
                }
            }
        }
    }
}
