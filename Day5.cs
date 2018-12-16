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
            var uniqueChars = Line.Select(c => c.ToString()).ToList().ConvertAll(c => c.ToUpper()).Distinct();
            var LineLenght = 0;// Line.Count();
            var smallestPolymer = 0;

            foreach (var c in uniqueChars)
            {
                Console.WriteLine($"Checking: {c}");
                var LineCopyReplaced = RemoveCharacterFromList(c, Line);
                bool KeepGoing = true;
                while (KeepGoing)
                {
                    LineLenght = LineCopyReplaced.Count();
                    for (int i = (LineCopyReplaced.Count() - 1); i > 0; i--)
                    {
                        if (i >= LineCopyReplaced.Count())
                            break;
                        var LineCopy = new List<char>(LineCopyReplaced);
                        if (CheckReaction(LineCopy[i - 1], LineCopy[i]))
                        {
                            LineCopyReplaced.RemoveAt(i);
                            LineCopyReplaced.RemoveAt(i - 1);
                        }
                    }
                    if (LineLenght == LineCopyReplaced.Count())
                        KeepGoing = false;
                }
                if (smallestPolymer == 0 || LineCopyReplaced.Count() < smallestPolymer)
                {
                    smallestPolymer = LineCopyReplaced.Count();
                    Console.WriteLine($"Smaller polymer so far = {smallestPolymer}");
                }
            }

            Console.WriteLine($"Length of smallest polymer = {smallestPolymer}");
            Console.ReadLine();
        }

        public List<char> RemoveCharacterFromList(string c, List<char> list)
        {
            var replacement = new List<char>();

            replacement = list.Where(x => x.ToString().ToUpper() != c).ToList();

            return replacement;
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
