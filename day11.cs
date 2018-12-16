using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace adventofcode
{
    class Day11
    {
        const int Serial = 8979;
        const int Width = 300;
        const int Height = 300;
        const int FuelCellGridSize = 3;
        List<FuelCellPower> fuelCellPowers;
        int[,] FuelCells;

        public void DoWork()
        {
            fuelCellPowers = new List<FuelCellPower>();

            CreateFuelCells();
            
            //too many loops so very slow. definitely a better way to do it...
            for(int g = 1; g <= Width; g++)
            {
                Console.WriteLine($"Calculating for grid size of {g}");
                GetAllFuelCellsPower(g);
            }

            var maxFuelCellPower = fuelCellPowers.Max(cell => cell.Power);
            var FuelCellWithMostPower = fuelCellPowers.First(x => x.Power == maxFuelCellPower);
            Console.WriteLine($"MaxFuelCell Power {FuelCellWithMostPower.Power}, at:" +
                $" {FuelCellWithMostPower.X},{FuelCellWithMostPower.Y} with grid size of {FuelCellWithMostPower.GridSize}");
            Console.ReadLine();
        }

        public void GetAllFuelCellsPower(int gridSize)
        {
            for (int y = 0; y < Width - gridSize; y++)
            {
                for (int x = 0; x < Height - gridSize; x++)
                {
                    fuelCellPowers.Add(new FuelCellPower(x + 1, y + 1, SumCellsInBox(x, y, gridSize), gridSize));
                }
            }
        }

        public void PrintCellsinBox(int startX, int startY, int boxSize)
        {
            for (int y = 0; y < boxSize; y++)
            {
                for (int x = 0; x < boxSize; x++)
                {
                    Console.Write(PadInt(FuelCells[x + startX - 1, y + startY - 1]));
                }
                Console.WriteLine();
            }            
        }

        public int SumCellsInBox(int startX, int startY, int boxSize)
        {
            int sum = 0;
            for (int y = 0; y < boxSize; y++)
            {
                for (int x = 0; x < boxSize; x++)
                {
                    sum += FuelCells[x + startX, y + startY];
                }
            }
            return sum;
        }

        public string PadInt(int i)
        {
            if (i >= 0)
                return $" {i} ";
            else
                return $"{i} ";
        }

        public void CreateFuelCells()
        {
            FuelCells = new int[Width, Height];
            for(int x = 0; x < Width; x++)
            {
                for(int y = 0; y < Height; y++)
                {
                    FuelCells[x, y] = CalculateCellValue(x + 1, y + 1, Serial);
                }
            }
        }

        public int CalculateCellValue(int xVal, int yVal, int serial)
        {
            int rackId = xVal + 10;
            int powerLevel = rackId * yVal;
            powerLevel += serial;
            powerLevel *= rackId;
            int hundredDigit = GetHundredDigit(powerLevel);
            return hundredDigit - 5;
        }

        public int GetHundredDigit(int input)
        {
            int inputLenght = input.ToString().Length;
            if (inputLenght > 2)
            {
                return int.Parse(input.ToString().ElementAt(inputLenght - 3).ToString());
            }
            return 0;
        }

        class FuelCellPower
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Power { get; set; }
            public int GridSize { get; set; }
            public FuelCellPower(int x, int y, int power, int gridSize)
            {
                X = x;
                Y = y;
                Power = power;
                GridSize = gridSize;
            }
        }
    }
}
