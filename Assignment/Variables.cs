using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class Variables
    {

        string Name { get; set; }
        int Value { get; set; }

        public Variables(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            return "Name: " + Name + "Value: " + Value;
        }
    }
}
