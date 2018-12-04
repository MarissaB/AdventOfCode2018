using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
    public class Claim
    {
        public int ID { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
        public List<Tuple<int, int>> CoordinatesList = new List<Tuple<int, int>>();
        public bool Overlap { get; set; }

        public Claim(int v1, int v2, int v3, int v4, int v5)
        {
            ID = v1;
            Left = v2;
            Top = v3;
            Width = v4;
            Length = v5;
        }

        public Claim() { }

    }
}
