using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace adventofcode
{
    class Day2
    {
        int TwoCount { get; set; }
        int ThreeCount { get; set; }
        string input = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Inputs\day2input.txt";
        public void DoWork()
        {
            TwoCount = 0;
            ThreeCount = 0;
            var Lines = File.ReadLines(input).Select(line => line).ToList();

            foreach(var line1 in Lines)
            {
                foreach(var line2 in Lines)
                {
                    if(HasOnlyOneDiff(line1, line2))
                    {
                        Console.WriteLine(GetSameDifference(line1, line2));
                    }
                }
            }

            //Lines.ForEach(CheckStringForDuplicates);
            //Console.WriteLine($"Checksum {TwoCount} * {ThreeCount} = {TwoCount * ThreeCount}");
            Console.ReadLine();
        }

        public void CheckStringForDuplicates(string str)
        {
            var CharCounts = new List<CharCount>();
            foreach(var Chr in str.ToCharArray())
            {
                if(CharCounts.Any(x => x.Char == Chr))
                {
                    CharCounts.First(x => x.Char == Chr).Count++;
                } else
                {
                    CharCounts.Add(new CharCount() { Char = Chr, Count = 1 });
                }
            }
            if (CharCounts.Any(x => x.Count == 2))
                TwoCount++;
            if (CharCounts.Any(x => x.Count == 3))
                ThreeCount++;
        }

        public bool HasOnlyOneDiff(string str1, string str2)
        {
            var diffs = 0;
            for(int i = 0; i < str1.Length; i++)
            {
                if(str1[i] != str2[i])
                {
                    diffs++;
                    if(diffs > 1)
                    {
                        return false;
                    }
                }
            }
            return (diffs == 1);
        }

        public string GetSameDifference(string str1, string str2)
        {
            var same = "";
            for (int i = 0; i < str1.Length; i++)
            {
                if (str1[i] == str2[i])
                {
                    same += str1[i];
                }
            }
            return same;
        }

        public class CharCount
        {
            public char Char { get; set; }
            public int Count { get; set; }
        }
    }
}
