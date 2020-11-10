using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class Method
    {
        public string Name { get; set; }
        public List<string> Commands { get; set; }
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
