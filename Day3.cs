using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace adventofcode
{
    public class Day3
    {
        public int[,] Board;

        public void DoWork()
        {
            var Cloths = File.ReadLines(@"C:\Users\Joe\Desktop\AdventOfCodeDay3 - input.txt").Select(line => new Cloth(line)).ToList();

            Board = new int[1000, 1000];
            //init board
            for (int x = 0; x < 1000; x++)
            {
                for (int y = 0; y < 1000; y++)
                {
                    Board[x, y] = 0;
                }
            }

            Cloths.ForEach(AddToBoard);
            Cloths.ForEach(CheckOverlaps);

            //dont need this just looks pretty
            PrintBoard();

            //get result
            int count = 0;
            for (int x = 0; x < 1000; x++)
            {
                for (int y = 0; y < 1000; y++)
                {
                    if (Board[x, y] > 1)
                    {
                        count++;
                    }
                }
            }

            Console.WriteLine("Day 3 -");
            Console.WriteLine("Square inchs with two or more claims: " + count);
            Console.WriteLine("Overlaps claim id: " + Cloths.Where(x => !x.overlaps).First().claim);

            Console.ReadLine();
        }

        private void CheckOverlaps(Cloth cloth)
        {
            for (int x = 0; x < cloth.width; x++)
            {
                for (int y = 0; y < cloth.height; y++)
                {
                    if (Board[cloth.xPos + x, cloth.yPos + y] > 1)
                        cloth.overlaps = true;
                }
            }
        }

        private void AddToBoard(Cloth cloth)
        {
            for (int x = 0; x < cloth.width; x++)
            {
                for (int y = 0; y < cloth.height; y++)
                {
                    Board[cloth.xPos + x, cloth.yPos + y]++;
                }
            }
        }

        private void PrintBoard()
        {
            var lines = new List<string>();

            for (int y = 0; y < 1000; y++)
            {
                var line = "";
                for (int x = 0; x < 1000; x++)
                {
                    line += Board[x, y].ToString();
                }
                lines.Add(line);
            }
            File.WriteAllLines(@"C:\Users\Joe\Desktop\AdventOfCodeDay3.txt", lines);
        }
    }

    public class Cloth
    {
        public int claim { get; set; }
        public int xPos { get; set; }
        public int yPos { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public bool overlaps { get; set; }

        public Cloth(string input)
        {
            var inputSplit = input.Replace("#", "").Replace("@ ", "").Replace("x", " ").Replace(":", "").Replace(",", " ").Split(' ');
            claim = int.Parse(inputSplit[0]);
            xPos = int.Parse(inputSplit[1]);
            yPos = int.Parse(inputSplit[2]);
            width = int.Parse(inputSplit[3]);
            height = int.Parse(inputSplit[4]);
            overlaps = false;
        }
    }
}
