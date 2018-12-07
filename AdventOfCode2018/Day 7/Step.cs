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
        public List<char> After { get; set; }
        public bool Visited { get; set; }
        public bool Finished { get; set; }

        public Step() { }

        public Step(char input)
        {
            Letter = input;
            After = new List<char>();
            Visited = false;
            Finished = false;
        }
    }
}
