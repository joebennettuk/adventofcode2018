using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;

namespace adventofcode
{
    class Day10
    {
        string input = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Inputs\day10input.txt";
        string output = @"C:\Users\Joe\Desktop\output\";

        public void DoWork()
        {
            var lightPoints = File.ReadLines(input).Select(x => new LightPoint(x)).ToList();

            var second = 1;
            var widthOffset = lightPoints.Min(lp => lp.xPos);
            bool match = false;
            while (!match)
            {
                Console.Clear();

                lightPoints.ForEach(lp => lp.MoveVelocity());
                second++;
                var board = new Board(lightPoints, output, second);
                
                //create images for these to make a tasty visualisation
                if (Math.Abs(board.minXPos) < 300)
                {
                    board.CreateBitmap(lightPoints);
                    if (board.minXPos < widthOffset)
                    {
                        Console.WriteLine("WE GOT A MATCH");
                        match = true;
                    }
                }

                widthOffset = board.minXPos;
            }

            Console.ReadLine();
        }

        class Board
        {
            public int Height { get; set; }
            public int Width { get; set; }
            private string Output { get; }
            private int Second { get; }
            public int minXPos { get; set; }
            public int maxXPos { get; set; }
            public int maxYPos { get; set; }            
            public int minYPos { get; set; }
            
            public Board(List<LightPoint> lightPoints, string output, int second)
            {
                minXPos = lightPoints.Min(lp => lp.xPos);
                maxYPos = lightPoints.Max(lp => lp.yPos);
                maxXPos = lightPoints.Max(lp => lp.xPos);
                minYPos = lightPoints.Min(lp => lp.yPos);
                Output = output;
                Second = second;
                Console.WriteLine($"leftestPoint - {minXPos}");
                Console.WriteLine($"lowestPoint - {minYPos}");
                Console.WriteLine($"rightestPoint - {maxXPos}");
                Console.WriteLine($"highestPoint - {maxYPos}");

                Width = Math.Abs(minXPos - maxXPos) + 1;
                Height = Math.Abs(minYPos - maxYPos) + 1;
            }

            public void CreateBitmap(List<LightPoint> lightPoints)
            {
                using (var bmp = new Bitmap(Width, Height))
                {
                    foreach (var lp in lightPoints)
                    {                        
                        var x = Math.Abs(lp.xPos - minXPos);
                        var y = Math.Abs(lp.yPos - maxYPos);
                        bmp.SetPixel(x, y, Color.Black);
                    }
                    
                    bmp.Save($"{Output}step{Second}.bmp");
                }
            }
        }

        class LightPoint
        {
            public int xPos { get; set; }
            public int yPos { get; set; }
            public int xVelocity { get; set; }
            public int yVelocity { get; set; }

            public LightPoint(string input)
            {
                var inputArray = input.Replace(" ", "").Replace("position=<", "").Replace("velocity=<", ",").Replace(">", "").Split(',');
                xPos = int.Parse(inputArray[0]);
                yPos = int.Parse(inputArray[1]);
                xVelocity = int.Parse(inputArray[2]);
                yVelocity = int.Parse(inputArray[3]);
            }

            public void MoveVelocity()
            {
                xPos += xVelocity;
                yPos += yVelocity;
            }
        }
        
    }
}
