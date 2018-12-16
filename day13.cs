using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace adventofcode
{
    class Day13
    {
        const bool VISUALISE = false;
        int Height, Width;
        List<Cart> Carts;
        List<Crash> Crashes;
        char[,] Track;
        
        string input = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\Inputs\day13input.txt";
        public void DoWork()
        {
            Crashes = new List<Crash>();
            Carts = new List<Cart>();
            var Lines = File.ReadLines(input).ToList();
            Width = Lines.Max(line => line.Length);
            Height = Lines.Count();

            Track = ReadTrack(Lines);
            PrintTrack(VISUALISE);

            while(Carts.Count > 1)
            {
                MoveCartsAndPrint(null);
                Console.WriteLine($"Carts left: {Carts.Count()}");
                if(VISUALISE) Thread.Sleep(500);
            }

            Console.WriteLine("***************CRASHES***************");
            Crashes.ForEach(PrintCrash);
            Console.WriteLine("****************CARTS****************");
            Carts.ForEach(PrintCart);

            Console.ReadLine();
        }

        private void PrintCart(Cart cart)
        {
            Console.WriteLine($"Cart at: {cart.X},{cart.Y}");
        }

        private void PrintCrash(Crash crash)
        {
            Console.WriteLine($"Cart crashed at: {crash.X},{crash.Y}");
        }

        public void MoveCartsAndPrint(Object state)
        {
            Carts = Carts.OrderBy(cart => cart.Y).ThenBy(cart => cart.X).ToList();
            Carts.ForEach(cart => cart.Move(Track, Carts));

            Console.Clear();
            PrintTrack(VISUALISE);

            //remove crashed carts
            Carts.RemoveAll(cart => cart.Crashed);
        }
        
        public char[,] ReadTrack(List<string> input)
        {
            var output = new char[Width, Height];
            var cartId = 0;
            for (int y = 0; y < Height; y++)
            {
                var xCounter = 0;
                foreach (char trackChar in input.ElementAt(y))
                {
                    var mapChar = trackChar;
                    if(trackChar == 'v' || trackChar == '^')
                    {
                        Carts.Add(new Cart(cartId, trackChar, xCounter, y));
                        cartId++;
                        mapChar = '|';
                    }
                    if(trackChar == '>' || trackChar == '<')
                    {
                        Carts.Add(new Cart(cartId, trackChar, xCounter, y));
                        cartId++;
                        mapChar = '-';
                    }
                    output[xCounter, y] = mapChar;
                    xCounter++;
                }
            }

            return output;
        }

        public void PrintTrack(bool print)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (Carts.Count(cart => cart.X == x && cart.Y == y) > 1)
                    {
                        if(print) Console.Write('X');
                        Crashes.Add(new Crash() { X = x, Y = y });
                        foreach(var cart in Carts.Where(cart => cart.X == x && cart.Y == y))
                        {
                            cart.Crashed = true;
                        }
                    } else if (Carts.Count(cart => cart.X == x && cart.Y == y) == 1)
                    {
                        if (print) Console.Write(Carts.First(cart => cart.X == x && cart.Y == y).Direction);
                    } else
                    {
                        if (print) Console.Write(Track[x, y]);
                    }
                }
                if (print) Console.WriteLine();
            }
        }

        private class Crash
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        private class Cart
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int DirectionX { get; set; }
            public int DirectionY { get; set; }
            public int IntersectionCount { get; set; }
            public char Direction { get; set; }
            public bool Crashed { get; set; }
            public int CartId { get; set; }

            public Cart(int cartId, char direc, int x, int y)
            {
                CartId = cartId;
                X = x;
                Y = y;
                Direction = direc;
                IntersectionCount = 0;
                Crashed = false;
            }

            private void Move(List<Cart> allCarts)
            {
                if(!Crashed)
                {
                    X += DirectionX;
                    Y += DirectionY;
                }                
                foreach(var cart in allCarts.Where(cart => cart.X == X && cart.Y == Y && cart.CartId != CartId && !cart.Crashed))
                {
                    cart.Crashed = true;
                    Crashed = true;
                }
            }
            
            public bool Move(char[,] track, List<Cart> allCarts)
            {
                if(Direction == '^')
                {
                    DirectionNorth();
                    switch (track[X, Y - 1])
                    {
                        case '/':
                            Direction = '>';
                            Move(allCarts);
                            break;
                        case '\\':
                            Direction = '<';
                            Move(allCarts);
                            break;
                        case '+':
                            MoveIntersection(track, allCarts);
                            break;
                        default:
                            Move(allCarts);
                            break;
                    }
                    return true;
                }

                if (Direction == '>')
                {
                    DirectionEast();
                    switch (track[X + 1, Y])
                    {
                        case '\\':
                            Direction = 'v';
                            Move(allCarts);
                            break;
                        case '/':
                            Direction = '^';
                            Move(allCarts);
                            break;
                        case '+':
                            MoveIntersection(track, allCarts);
                            break;
                        default:
                            Move(allCarts);
                            break;
                    }
                    return true;
                }

                if (Direction == 'v')
                {
                    DirectionSouth();
                    switch (track[X, Y + 1])
                    {
                        case '/':
                            Direction = '<';
                            Move(allCarts);
                            break;
                        case '\\':
                            Direction = '>';
                            Move(allCarts);
                            break;
                        case '+':
                            MoveIntersection(track, allCarts);
                            break;
                        default:
                            Move(allCarts);
                            break;
                    }
                    return true;
                }

                if (Direction == '<')
                {
                    DirectionWest();
                    switch (track[X - 1, Y])
                    {
                        case '\\':
                            Direction = '^';
                            Move(allCarts);
                            break;
                        case '/':
                            Direction = 'v';
                            Move(allCarts);
                            break;
                        case '+':
                            MoveIntersection(track, allCarts);
                            break;
                        default:
                            Move(allCarts);
                            break;
                    }
                    return true;
                }

                return false;
            }

            public bool MoveIntersection(char[,] track, List<Cart> allCarts)
            {
                //go left
                if (IntersectionCount == 0)
                {
                    Move(allCarts);
                    ChangeDirectionLeft();
                    IntersectionCount++;
                    return true;
                }
                //go straight
                if (IntersectionCount == 1)
                {
                    Move(allCarts);
                    IntersectionCount++;
                    return true;
                }
                //go right
                if (IntersectionCount == 2)
                {
                    Move(allCarts);
                    ChangeDirectionRight();
                    IntersectionCount = 0;
                    return true;
                }
                return true;
            }

            private void ChangeDirectionLeft()
            {
                switch(Direction)
                {
                    case '>':
                        Direction = '^';
                        DirectionNorth();
                        break;
                    case 'v':
                        Direction = '>';
                        DirectionWest();
                        break;
                    case '<':
                        Direction = 'v';
                        DirectionSouth();
                        break;
                    case '^':
                        Direction = '<';
                        DirectionEast();
                        break;
                }
            }

            private void ChangeDirectionRight()
            {
                switch (Direction)
                {
                    case '<':
                        Direction = '^';
                        DirectionNorth();
                        break;
                    case '^':
                        Direction = '>';
                        DirectionWest();
                        break;
                    case '>':
                        Direction = 'v';
                        DirectionSouth();
                        break;
                    case 'v':
                        Direction = '<';
                        DirectionEast();
                        break;
                }
            }

            public void DirectionNorth()
            {
                DirectionY = -1;
                DirectionX = 0;
            }

            public void DirectionSouth()
            {
                DirectionY = 1;
                DirectionX = 0;
            }

            public void DirectionEast()
            {
                DirectionY = 0;
                DirectionX = 1;
            }

            public void DirectionWest()
            {
                DirectionY = 0;
                DirectionX = -1;
            }
        }
    }
}
