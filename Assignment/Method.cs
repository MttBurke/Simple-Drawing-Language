using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    /// <summary>
    /// Method class used to store methods with a list of commands and list of parameters
    /// </summary>
    class Method
    {
        /// <summary>
        /// Name of method
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// list of commands to be run
        /// </summary>
        public List<string> Commands { get; set; }
        /// <summary>
        /// List of paramaeters to be used
        /// </summary>
        public List<string> Parameters { get; set; }

        public Method()
        {

        }

        public Method(string name, List<string> commands, List<string> parameters)
        {
            Name = name;
            Commands = commands;
            Parameters = parameters;
        }
    }
}
