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
    public class ExtendedParser : Parser
    {

        public DrawingPanel dp;
        Panel GraphicsPanel;
        Dictionary<string, int> vars = new Dictionary<string, int>();
        Regex VariableName = new Regex("\\w*\\d*");
        bool IfStatementValid = false; //Used for IF statements, Skips commands if the IF statement isn't correct until and ENDIF is found
        bool EndifFound = true;
        bool LoopStatement = false;
        string[] LineArray;
        int LineNum = 0;
        public List<string> Errors = new List<string>();

        public ExtendedParser(Panel InputPanel, Bitmap InputBitmap) : base(InputPanel, InputBitmap)
        {
            GraphicsPanel = InputPanel;
            dp = new DrawingPanel(InputPanel, InputBitmap);
        }

        public void ParseTextBox(TextBox Inputbox)
        {
            LineArray = Inputbox.Lines;
            int LoopLineNum = 0;
            int EndLoopLine = Array.IndexOf(LineArray, "Endloop");
            /*
             * Looping through each line that was written in command area
             */
            for (LineNum = 0; LineNum < LineArray.Length; LineNum++)
            {
                string[] split = { " " };
                if (LineArray[LineNum].Trim() != "Endif" || LineArray[LineNum].Trim() != "Endloop")
                {
                    split = LineArray[LineNum].Split(' ');
                }
                else if (LineArray[LineNum].Trim() == "Endif")
                {
                    split[0] = "Endif";
                }
                else if (LineArray[LineNum].Trim() == "Endloop")
                {
                    split[0] = "Endloop";
                }
                /*
                 * splitting each line where there is a space to get operators and operands.
                 */

                if (split[0] == "If")
                {
                    ParseIf(split);
                }
                else if (split[0] == "Endif")
                {
                    IfStatementValid = false;
                    EndifFound = true;
                }
                else if (split[0] == "Loop")
                {
                    /*
                     * If loop statements are still true loop through each line within LOOP and ENDLOOP statement
                     * if not LOOP is completely skipped
                     */
                    LoopLineNum = LineNum;
                    int ValueToCompare = int.Parse(split[3]);

                    if (vars.ContainsKey(split[1]))
                    {
                        if (split[2] == ">")
                        {
                            if (vars[split[1]] > ValueToCompare)
                            {
                                LoopStatement = true;
                            }
                            else
                            {
                                LoopStatement = false;
                                LineNum = EndLoopLine;
                            }
                        }
                        else if (split[2] == ">=")
                        {
                            if (vars[split[1]] >= ValueToCompare)
                            {
                                LoopStatement = true;
                            }
                            else
                            {
                                LoopStatement = false;
                                LineNum = EndLoopLine;
                            }
                        }
                        else if (split[2] == "<")
                        {
                            if (vars[split[1]] < ValueToCompare)
                            {
                                LoopStatement = true;
                            }
                            else
                            {
                                LoopStatement = false;
                                LineNum = EndLoopLine;
                            }
                        }
                        else if (split[2] == "<=")
                        {
                            if (vars[split[1]] <= ValueToCompare)
                            {
                                LoopStatement = true;
                            }
                            else
                            {
                                LoopStatement = false;
                                LineNum = EndLoopLine;
                            }
                        }
                        else if (split[2] == "!=")
                        {
                            if (vars[split[1]] != ValueToCompare)
                            {
                                LoopStatement = true;
                            }
                            else
                            {
                                LoopStatement = false;
                                LineNum = EndLoopLine;
                            }
                        }
                    }
                    else
                    {
                        Errors.Add("Variable: " + split[1] + " doesn't exist on line " + LineNum);
                    }
                }
                else if (split[0] == "Endloop")
                {
                    if (LoopStatement == true)
                    {
                        LineNum = LoopLineNum - 1;
                        LoopStatement = false;
                    }
                }
                else if (split[0] == "Method")
                {

                }
                else if (split[0] == "drawTo" || split[0] == "moveTo" || split[0] == "Rectangle" || split[0] == "Triangle" || split[0] == "Circle" || split[0] == "Fill" || split[0] == "Colour" || split[0] == "Clear" || split[0] == "Reset")
                {
                    if ((IfStatementValid == true && EndifFound == false) || (EndifFound == true && IfStatementValid == false))
                    {
                        ParseCommand(LineArray[LineNum].Trim());
                    }
                }
                else if (VariableName.IsMatch(split[0])) //If input is any type of text and it has some type of operator a variable will be added or changed
                {
                    if (!string.IsNullOrWhiteSpace(split[0]))
                    {
                        try
                        {
                            ParseVariable(split);
                        }
                        catch
                        {
                            Errors.Add("Invalid input line " + LineNum);
                        }
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
            if (InputCommand.Trim() == "Clear" || InputCommand.Trim() == "Reset")
            {
                Command = InputCommand.Trim();
            }
            else
            {
                try
                {
                    Command = InputCommand.Substring(0, InputCommand.IndexOf(' '));
                    Parameter = InputCommand.Substring(InputCommand.IndexOf(' ') + 1);
                    SplitParameters = Parameter.Split(',');
                }
                catch
                {
                    Errors.Add("Invalid input detected line " + LineNum);
                }
            }

            if (Command.Trim() == "drawTo")
            {
                try
                {
                    if (vars.ContainsKey(SplitParameters[0]) && vars.ContainsKey(SplitParameters[1]))
                    {
                        x = SetX(vars[SplitParameters[0]].ToString());
                        y = SetY(vars[SplitParameters[1]].ToString());
                        dp.DrawLine(x, y);
                    }
                    else if (vars.ContainsKey(SplitParameters[0]))
                    {
                        x = SetX(vars[SplitParameters[0]].ToString());
                        y = SetY(SplitParameters[1]);
                        dp.DrawLine(x, y);
                    }
                    else if (vars.ContainsKey(SplitParameters[1]))
                    {
                        x = SetX(SplitParameters[0]);
                        y = SetY(vars[SplitParameters[1]].ToString());
                        dp.DrawLine(x, y);
                    }
                    else
                    {
                        x = SetX(SplitParameters[0]);
                        y = SetY(SplitParameters[1]);
                        dp.DrawLine(x, y);
                    }
                }
                catch
                {
                    Errors.Add("Invalid parameters deteced line " + LineNum);
                }
            }
            else if (Command.Trim() == "Rectangle")
            {
                try
                {
                    if (vars.ContainsKey(SplitParameters[0]) && vars.ContainsKey(SplitParameters[1]))
                    {
                        x = SetX(vars[SplitParameters[0]].ToString());
                        y = SetY(vars[SplitParameters[1]].ToString());
                        dp.DrawRectangle(x, y);
                    }
                    else if (vars.ContainsKey(SplitParameters[0]))
                    {
                        x = SetX(vars[SplitParameters[0]].ToString());
                        y = SetY(SplitParameters[1]);
                        dp.DrawRectangle(x, y);
                    }
                    else if (vars.ContainsKey(SplitParameters[1]))
                    {
                        x = SetX(SplitParameters[0]);
                        y = SetY(vars[SplitParameters[1]].ToString());
                        dp.DrawRectangle(x, y);
                    }
                    else
                    {
                        x = SetX(SplitParameters[0]);
                        y = SetY(SplitParameters[1]);
                        dp.DrawRectangle(x, y);
                    }
                }
                catch
                {
                    Errors.Add("Invalid parameters deteced line " + LineNum);
                }
            }
            else if (Command.Trim() == "moveTo")
            {
                try
                {
                    if (vars.ContainsKey(SplitParameters[0]) && vars.ContainsKey(SplitParameters[1]))
                    {
                        x = SetX(vars[SplitParameters[0]].ToString());
                        y = SetY(vars[SplitParameters[1]].ToString());
                        dp.MoveTo(x, y);
                    }
                    else if (vars.ContainsKey(SplitParameters[0]))
                    {
                        x = SetX(vars[SplitParameters[0]].ToString());
                        y = SetY(SplitParameters[1]);
                        dp.MoveTo(x, y);
                    }
                    else if (vars.ContainsKey(SplitParameters[1]))
                    {
                        x = SetX(SplitParameters[0]);
                        y = SetY(vars[SplitParameters[1]].ToString());
                        dp.MoveTo(x, y);
                    }
                    else
                    {
                        x = SetX(SplitParameters[0]);
                        y = SetY(SplitParameters[1]);
                        dp.MoveTo(x, y);
                    }
                }
                catch
                {
                    Errors.Add("Invalid parameters deteced line " + LineNum);
                }
            }
            else if (Command.Trim() == "Circle")
            {
                try
                {
                    if (vars.ContainsKey(SplitParameters[0]))
                    {
                        x = SetX(vars[SplitParameters[0]].ToString());
                        dp.DrawCircle(x);
                    }
                    else
                    {
                        x = SetX(SplitParameters[0]);
                        dp.DrawCircle(x);
                    }
                }
                catch
                {
                    Errors.Add("Invalid parameters deteced line " + LineNum);
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
                    if (vars.ContainsKey(SplitParameters[0]) && vars.ContainsKey(SplitParameters[1]) && vars.ContainsKey(SplitParameters[2]))
                    {
                        x = SetX(vars[SplitParameters[0]].ToString());
                        y = SetY(vars[SplitParameters[1]].ToString());
                        z = SetY(vars[SplitParameters[2]].ToString());
                        dp.DrawTriangle(x, y, z);
                    }
                    else if (vars.ContainsKey(SplitParameters[0]) && vars.ContainsKey(SplitParameters[1]))
                    {
                        x = SetX(vars[SplitParameters[0]].ToString());
                        y = SetY(vars[SplitParameters[1]].ToString());
                        z = vars[SplitParameters[2]];
                        dp.DrawTriangle(x, y, z);
                    }
                    else if (vars.ContainsKey(SplitParameters[1]) && vars.ContainsKey(SplitParameters[2]))
                    {
                        x = SetX(SplitParameters[0]);
                        y = SetY(vars[SplitParameters[1]].ToString());
                        z = SetY(vars[SplitParameters[2]].ToString());
                        dp.DrawTriangle(x, y, z);
                    }
                    else if (vars.ContainsKey(SplitParameters[0]) && vars.ContainsKey(SplitParameters[2]))
                    {
                        x = SetX(vars[SplitParameters[0]].ToString());
                        y = SetY(SplitParameters[1]);
                        z = SetY(vars[SplitParameters[2]].ToString());
                        dp.DrawTriangle(x, y, z);
                    }
                    else if (vars.ContainsKey(SplitParameters[0]))
                    {
                        x = SetX(vars[SplitParameters[0]].ToString());
                        y = SetY(SplitParameters[1]);
                        z = vars[SplitParameters[2]];
                        dp.DrawTriangle(x, y, z);
                    }
                    else if (vars.ContainsKey(SplitParameters[1]))
                    {
                        x = SetX(SplitParameters[0]);
                        y = SetY(vars[SplitParameters[1]].ToString());
                        z = vars[SplitParameters[2]];
                        dp.DrawTriangle(x, y, z);
                    }
                    else if (vars.ContainsKey(SplitParameters[2]))
                    {
                        x = SetX(SplitParameters[0]);
                        y = SetY(SplitParameters[1]);
                        z = SetY(vars[SplitParameters[2]].ToString());
                        dp.DrawTriangle(x, y, z);
                    }
                    else
                    {
                        x = SetX(SplitParameters[0]);
                        y = SetY(SplitParameters[1]);
                        z = int.Parse(SplitParameters[2]);
                        dp.DrawTriangle(x, y, z);
                    }
                }
                catch
                {
                    Errors.Add("Invalid parameters deteced line " + LineNum);
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
            else if (Command.Trim() == "Clear")
            {
                dp.ClearPanel();
            }
            else if (Command.Trim() == "Reset")
            {
                dp.ResetPen();
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

        private void ParseVariable(string[] split)
        {
            /*
             * Parse variable eg x = 10
             * splits into 'x' '=' '10' new item in dictionary is created name is 'x' and value is '10'
             * if not initialising a variable with '=' checks if variable exists then + - * / value by input
             */

            string Name;
            int Value = 0;
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
                    Errors.Add("Invalid input for variable line " + LineNum);
                }
            }
            else if (split[1] == "+")
            {
                if (vars.ContainsKey(split[0]))
                {
                    try
                    {
                        vars[split[0]] += int.Parse(split[2]);
                    }
                    catch
                    {
                        Errors.Add("Invalid input for variable line " + LineNum);
                    }
                }
                else
                {
                    Errors.Add("Variable: " + split[0] + " doesn't exist line " + LineNum);
                }
            }
            else if (split[1] == "-")
            {
                if (vars.ContainsKey(split[0]))
                {
                    try
                    {
                        vars[split[0]] += int.Parse(split[2]);
                    }
                    catch
                    {
                        Errors.Add("Invalid input for variable line " + LineNum);
                    }
                }
                else
                {
                    Errors.Add("Variable: " + split[0] + " doesn't exist line " + LineNum);
                }
            }
            else if (split[1] == "*")
            {
                if (vars.ContainsKey(split[0]))
                {
                    try
                    {
                        vars[split[0]] += int.Parse(split[2]);
                    }
                    catch
                    {
                        Errors.Add("Invalid input for variable line " + LineNum);
                    }
                }
                else
                {
                    Errors.Add("Variable: " + split[0] + " doesn't exist line " + LineNum);
                }
            }
            else if (split[1] == "/")
            {
                if (vars.ContainsKey(split[0]))
                {
                    try
                    {
                        vars[split[0]] += int.Parse(split[2]);
                    }
                    catch
                    {
                        Errors.Add("Invalid input for variable line " + LineNum);
                    }
                }
                else
                {
                    Errors.Add("Variable: " + split[0] + " doesn't exist line " + LineNum);
                }
            }
        }

        private void ParseIf(string[] split)
        {
            /*
             * Parsing an If 
             * eg. If x < 10
             * splits into 'x' '<' '10' Checks if variable 'x' exists then compares with the expression '<' and value '10'
             * if true then execute everything after if statement
             * else do nothing until Endif is found
             */
            try
            {
                int ValueToCompare = int.Parse(split[3]);
                if (vars.ContainsKey(split[1]))
                {
                    if (split[2] == "=")
                    {
                        if (vars[split[1]] == ValueToCompare)
                        {
                            EndifFound = false;
                            IfStatementValid = true;
                        }
                    }
                    else if (split[2] == ">")
                    {
                        if (vars[split[1]] > ValueToCompare)
                        {
                            EndifFound = false;
                            IfStatementValid = true;
                        }
                    }
                    else if (split[2] == ">=")
                    {
                        if (vars[split[1]] >= ValueToCompare)
                        {
                            EndifFound = false;
                            IfStatementValid = true;
                        }
                    }
                    else if (split[2] == "<")
                    {
                        if (vars[split[1]] < ValueToCompare)
                        {
                            EndifFound = false;
                            IfStatementValid = true;
                        }
                    }
                    else if (split[2] == "<=")
                    {
                        if (vars[split[1]] <= ValueToCompare)
                        {
                            EndifFound = false;
                            IfStatementValid = true;
                        }
                    }
                    else if (split[2] == "!=")
                    {
                        if (vars[split[1]] != ValueToCompare)
                        {
                            EndifFound = false;
                            IfStatementValid = true;
                        }
                    }
                }
                else
                {
                    Errors.Add("Variable: " + split[0] + " doesn't exist line " + LineNum);
                }
            }
            catch
            {
                Errors.Add("Invalid value for comparison line " + LineNum);
            }
        }
    }
}
