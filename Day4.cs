using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace adventofcode
{
    class Day4
    {
        string input = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Inputs\day4input.txt";
        public void DoWork()
        {
            var Lines = File.ReadLines(input).Select(line => line).ToList();


        }
    }
    
}
