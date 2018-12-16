using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace adventofcode
{
    class Day5
    {
        string input = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Inputs\day5input.txt";
        public void DoWork()
        {
            var Line = File.ReadLines(input).First().ToCharArray().ToList();
            var LineLenght = Line.Count();
            bool KeepGoing = true;
            while(KeepGoing)
            {
                LineLenght = Line.Count();
                for (int i = Line.Count() - 1; i > 0; i--)
                {
                    var LineCopy = new List<char>(Line);
                    if (CheckReaction(LineCopy[i - 1], LineCopy[i]))
                    {
                        Line.RemoveAt(i);
                        Line.RemoveAt(i - 1);
                    }
                }
                if (LineLenght == Line.Count())
                    KeepGoing = false;
            }

            Console.WriteLine($"Length of polymer = {Line.Count()}");
            //Console.WriteLine($"Output = {String.Join("",Line.ToArray())}");
            Console.ReadLine();
        }

        bool CheckReaction(char chr1, char chr2)
        {
            if (chr1 == chr2)
                return false;
            if(chr1.ToString().ToUpper() == chr2.ToString())
                return true;
            if (chr1.ToString().ToLower() == chr2.ToString())
                return true;

            return false;
        }
    }
}
