using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode
{
    class Day1
    {
        private int Frequency { get; set; }
        private List<int> SavedFreqs { get; set; }
        string input = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Inputs\day1input.txt";
        public void DoWork()
        {
            var Lines = File.ReadLines(input).Select(line => int.Parse(line)).ToList();
            SavedFreqs = new List<int>();
            bool FoundDuplicate = false;
            Frequency = 0;

            while (!FoundDuplicate)
            {
                foreach (var freq in Lines)
                {
                    GetFrequency(freq);
                    if (!SavedFreqs.Any(x => x == Frequency))
                        SavedFreqs.Add(Frequency);
                    else
                    {
                        Console.WriteLine("Duplicate frequency = " + Frequency);
                        FoundDuplicate = true;
                        break;
                    }
                }
            }
            

            //Console.WriteLine("Frequency = " + Frequency);
            Console.ReadLine();
        }

        public void GetFrequency(int val)
        {
            Frequency += val;
        }
    }
}
