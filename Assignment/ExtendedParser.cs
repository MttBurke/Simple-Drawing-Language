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
    /// <summary>
    /// The ExtendedParse class inherited from Parser class
    /// used to parse a command or commands from a textbox.
    /// </summary>
    public class ExtendedParser : Parser
    {

        public DrawingPanel dp;
        Dictionary<string, int> vars = new Dictionary<string, int>();
        Regex VariableName = new Regex("\\w*\\d*");
        public bool IfStatementValid = false; //Used for IF statements, Skips commands if the IF statement isn't correct until and ENDIF is found
        public bool EndifFound = true;
        bool LoopStatement = false;
        int LineNum = 0;
        public List<string> Errors = new List<string>();
        List<Method> ListMethods = new List<Method>();
        List<string> PanelCommands = new List<string> { "drawTo", "moveTo", "Rectangle", "Triangle", "Circle", "Clear", "Fill", "Reset", "Colour" };

        public ExtendedParser(Panel InputPanel, Bitmap InputBitmap) : base(InputPanel, InputBitmap)
        {
            dp = new DrawingPanel(InputPanel, InputBitmap);
        }

        /// <summary>
        /// Parses commands from multiple lines of code within an array of string.
        /// </summary>
        /// <param name="LineArray">Array of string containing all the lines used in program</param>
        public void ParseTextBox(string[] LineArray)
        {
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
                    List<string> CommandsToAdd = new List<string>();
                    Method method = new Method
                    {
                        Name = split[1]
                    };
                    int EndLineNum = Array.IndexOf(LineArray, "Endmethod");

                    if (EndLineNum !=  -1)
                    {
                        for (int i = LineNum + 1; i < EndLineNum; i++)
                        {
                            CommandsToAdd.Add(LineArray[i]);
                        }

                        method.Commands = CommandsToAdd;
                        method.Parameters = GetParameters(split[2]);
                        ListMethods.Add(method);
                        LineNum = EndLineNum;
                    }
                    else
                    {
                        Errors.Add("Endmethod not found on for method: " + method.Name + " on line: " + LineNum);
                    }
                }
                else if (ListMethods.Any(method => method.Name == split[0]))
                {
                    List<string> CommandsToRun = new List<string>();
                    var MethodToRun = ListMethods.First(method => method.Name == split[0]);
                    int TempNum = LineNum;
                    string[] ParamsToUse = GetParametersBetween(split[1]);

                    try
                    {
                        if (MethodToRun.Parameters[0] == "")
                        {
                            CommandsToRun = MethodToRun.Commands;
                        }
                        else if (MethodToRun.Parameters.Count == ParamsToUse.Length)
                        {
                            for (int i = 0; i < ParamsToUse.Length; i++)
                            {
                                if (!vars.ContainsKey(ParamsToUse[i]))
                                {
                                    vars.Add(MethodToRun.Parameters[i], int.Parse(ParamsToUse[i]));
                                }
                            }
                            CommandsToRun = MethodToRun.Commands;
                        }
                        else
                        {
                            Errors.Add("Invalid parameters for Method: " + MethodToRun.Name);
                        }

                        ParseTextBox(CommandsToRun.ToArray());
                        LineNum = TempNum;
                    }
                    catch
                    {
                        Errors.Add("Invalid parameter for Method: " + MethodToRun.Name);
                    }
                }
                else if (PanelCommands.Contains(split[0]))
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

        /// <summary>
        /// Parses individual commands for drawing and not including commands used for sequence, iteration, and selection.
        /// </summary>
        /// <param name="InputCommand">String of command including the parameters to be split</param>
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
                        x = vars[SplitParameters[0]];
                        y = vars[SplitParameters[1]];
                        dp.DrawLine(x, y);
                    }
                    else if (vars.ContainsKey(SplitParameters[0]))
                    {
                        x = vars[SplitParameters[0]];
                        y = int.Parse(SplitParameters[1]);
                        dp.DrawLine(x, y);
                    }
                    else if (vars.ContainsKey(SplitParameters[1]))
                    {
                        x = int.Parse(SplitParameters[0]);
                        y = vars[SplitParameters[1]];
                        dp.DrawLine(x, y);
                    }
                    else
                    {
                        x = int.Parse(SplitParameters[0]);
                        y = int.Parse(SplitParameters[1]);
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
                        x = vars[SplitParameters[0]];
                        y = vars[SplitParameters[1]];
                        dp.DrawRectangle(x, y);
                    }
                    else if (vars.ContainsKey(SplitParameters[0]))
                    {
                        x = vars[SplitParameters[0]];
                        y = int.Parse(SplitParameters[1]);
                        dp.DrawRectangle(x, y);
                    }
                    else if (vars.ContainsKey(SplitParameters[1]))
                    {
                        x = int.Parse(SplitParameters[0]);
                        y = vars[SplitParameters[1]];
                        dp.DrawRectangle(x, y);
                    }
                    else
                    {
                        x = int.Parse(SplitParameters[0]);
                        y = int.Parse(SplitParameters[1]);
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
                        x = vars[SplitParameters[0]];
                        y = vars[SplitParameters[1]];
                        dp.MoveTo(x, y);
                    }
                    else if (vars.ContainsKey(SplitParameters[0]))
                    {
                        x = vars[SplitParameters[0]];
                        y = int.Parse(SplitParameters[1]);
                        dp.MoveTo(x, y);
                    }
                    else if (vars.ContainsKey(SplitParameters[1]))
                    {
                        x = int.Parse(SplitParameters[0]);
                        y = vars[SplitParameters[1]];
                        dp.MoveTo(x, y);
                    }
                    else
                    {
                        x = int.Parse(SplitParameters[0]);
                        y = int.Parse(SplitParameters[1]);
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
                        x = vars[SplitParameters[0]];
                        dp.DrawCircle(x);
                    }
                    else
                    {
                        x = int.Parse(SplitParameters[0]);
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
                        x = vars[SplitParameters[0]];
                        y = vars[SplitParameters[1]];
                        z = vars[SplitParameters[2]];
                        dp.DrawTriangle(x, y, z);
                    }
                    else if (vars.ContainsKey(SplitParameters[0]) && vars.ContainsKey(SplitParameters[1]))
                    {
                        x = vars[SplitParameters[0]];
                        y = vars[SplitParameters[1]];
                        z = int.Parse(SplitParameters[2]);
                        dp.DrawTriangle(x, y, z);
                    }
                    else if (vars.ContainsKey(SplitParameters[1]) && vars.ContainsKey(SplitParameters[2]))
                    {
                        x = int.Parse(SplitParameters[0]);
                        y = vars[SplitParameters[1]];
                        z = vars[SplitParameters[2]];
                        dp.DrawTriangle(x, y, z);
                    }
                    else if (vars.ContainsKey(SplitParameters[0]) && vars.ContainsKey(SplitParameters[2]))
                    {
                        x = vars[SplitParameters[0]];
                        y = int.Parse(SplitParameters[1]);
                        z = vars[SplitParameters[2]];
                        dp.DrawTriangle(x, y, z);
                    }
                    else if (vars.ContainsKey(SplitParameters[0]))
                    {
                        x = vars[SplitParameters[0]];
                        y = int.Parse(SplitParameters[1]);
                        z = int.Parse(SplitParameters[2]);
                        dp.DrawTriangle(x, y, z);
                    }
                    else if (vars.ContainsKey(SplitParameters[1]))
                    {
                        x = int.Parse(SplitParameters[0]);
                        y = vars[SplitParameters[1]];
                        z = int.Parse(SplitParameters[2]);
                        dp.DrawTriangle(x, y, z);
                    }
                    else if (vars.ContainsKey(SplitParameters[2]))
                    {
                        x = int.Parse(SplitParameters[0]);
                        y = int.Parse(SplitParameters[1]);
                        z = vars[SplitParameters[2]];
                        dp.DrawTriangle(x, y, z);
                    }
                    else
                    {
                        x = int.Parse(SplitParameters[0]);
                        y = int.Parse(SplitParameters[1]);
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

        /// <summary>
        /// Get parameters between () then splitting them to be added to a list for creation of a method
        /// </summary>
        /// <param name="input">Line where Method keyword has been used</param>
        /// <returns>A list of string with parameters to be added to create a method</returns>
        private List<string> GetParameters(string input)
        {
            /*
             * getting parameters between () then splitting  them to add to a list for method
             */
            input = input.Replace("(", string.Empty).Replace(")", string.Empty);
            List<string> Parameters = new List<string>();
            string[] split = input.Split(',');
            for (int i = 0; i < split.Length; i++)
            {
                Parameters.Add(split[i]);
            }
            return Parameters;
        }

        /// <summary>
        /// Gets parameters between () to be used when a created method has been called. 
        /// </summary>
        /// <param name="input">Line where created method name has been used</param>
        /// <returns>An array of string containing all parameters from a method to be used</returns>
        private string[] GetParametersBetween(string input)
        {
            /*
             * Splitting parameters between () to be used in the method command
             */
            input = input.Replace("(", string.Empty).Replace(")", string.Empty);
            return input.Split(',');
        }

        /// <summary>
        /// Used to parse a line where a variable has been declared
        /// </summary>
        /// <param name="split">Line where variable has been declared to be parsed</param>
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

        /// <summary>
        /// Parsing an IF statement
        /// </summary>
        /// <param name="split">Line where IF statement has been mentioned</param>
        private void ParseIf(string[] split)
        {
            /*
             * Parsing an If 
             * eg. If x < 10
             * splits into 'x' '<' '10' Checks if variable 'x' exists then compares with the expression '<' and value '10'
             * if true then execute everything after if statement
             * else do nothing until Endif is found
             */
            EndifFound = false;
            try
            {
                int ValueToCompare = int.Parse(split[3]);
                if (vars.ContainsKey(split[1]))
                {
                    if (split[2] == "=")
                    {
                        if (vars[split[1]] == ValueToCompare)
                        {
                            IfStatementValid = true;
                        }
                    }
                    else if (split[2] == ">")
                    {
                        if (vars[split[1]] > ValueToCompare)
                        {
                            IfStatementValid = true;
                        }
                    }
                    else if (split[2] == ">=")
                    {
                        if (vars[split[1]] >= ValueToCompare)
                        {
                            IfStatementValid = true;
                        }
                    }
                    else if (split[2] == "<")
                    {
                        if (vars[split[1]] < ValueToCompare)
                        {
                            IfStatementValid = true;
                        }
                    }
                    else if (split[2] == "<=")
                    {
                        if (vars[split[1]] <= ValueToCompare)
                        {
                            IfStatementValid = true;
                        }
                    }
                    else if (split[2] == "!=")
                    {
                        if (vars[split[1]] != ValueToCompare)
                        {
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
