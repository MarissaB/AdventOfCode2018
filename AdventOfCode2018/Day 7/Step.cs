using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
    public class Step
    {
        public char Letter { get; set; }
        public List<char> Before { get; set; }

        public Step() { }

        public Step(char input)
        {
            Letter = input;
            Before = new List<char>();
        }
    }
}
