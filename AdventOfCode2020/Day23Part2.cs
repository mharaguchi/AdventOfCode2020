using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day23Part2
    {
        public static string _input = @"389125467";
        public static List<int> _cups = new List<int>();
        public static int _currentCupIndex = 0;
        public static int _moves = 0;

        public static long Run(string input)
        {
            //input = _input;
            //_moves = 100;
            
            _moves = 10000000;
            
            ParseInput(input);

            for(int i = 0; i < _moves; i++)
            {
                RunMove(i);
            }

            var cup1 = _cups[(_cups.IndexOf(1) + 1) % _cups.Count];
            Console.WriteLine("cup1: " + cup1);
            var cup2 = _cups[(_cups.IndexOf(1) + 2) % _cups.Count];
            Console.WriteLine("cup2: " + cup2);
            long product = (long)cup1 * (long)cup2;
            return product;
            //return string.Join("", _cups);
        }

        public static void RunMove(int moveNum)
        {
            Console.WriteLine("Running move " + moveNum);
            var pickUp = new List<int>();
            var currentCup = _cups[_currentCupIndex];
            for(int i = 0; i < 3; i++)
            {
                pickUp.Add(_cups[(_currentCupIndex + i + 1) % _cups.Count]);
            }
            var dest = GetDestination(pickUp);
            //PrintStatus(moveNum, pickUp, dest);

            var removeIndex = _currentCupIndex + 1 % _cups.Count;
            Remove3(removeIndex);

            var destIndex = _cups.IndexOf(dest) + 1;

            _cups.InsertRange(destIndex, pickUp);


            //for(int i = 0; i < 3; i++)
            //{
            //    var pickedUp = pickUp[i];
            //    var sourceIndex = _cups.IndexOf(pickedUp);
            //    _cups.RemoveAt(sourceIndex);
            //    var destIndex = _cups.IndexOf(dest) + i + 1;
            //    _cups.Insert(destIndex, pickedUp);
            //}
            _currentCupIndex = (_cups.IndexOf(currentCup) + 1) % _cups.Count;
        }

        public static void Remove3(int startIndex)
        {
            if (_cups.Count - (startIndex + 2) > 0)
            {
                _cups.RemoveRange(startIndex, 3);
            }
            else if (_cups.Count - (startIndex + 2) == 0)
            {
                _cups.RemoveRange(startIndex, 2);
                _cups.RemoveAt(0);
            }
            else if (_cups.Count - (startIndex + 2) == -1)
            {
                _cups.RemoveAt(_cups.Count - 1);
                _cups.RemoveRange(0, 2);
            }
            else if (_cups.Count - (startIndex + 2) == -2)
            {
                _cups.RemoveRange(0, 3);
            }
        }

        public static int GetDestination(List<int> pickUp)
        {
            var currentCup = _cups[_currentCupIndex];
            var dest = currentCup;
            dest = DecreaseDest(dest);
            if (pickUp.Contains(dest))
            {
                dest = DecreaseDest(dest);
                if (pickUp.Contains(dest))
                {
                    dest = DecreaseDest(dest);
                    if (pickUp.Contains(dest))
                    {
                        dest = DecreaseDest(dest);
                    }
                }
            }
            return dest;
        }

        public static int DecreaseDest(int dest)
        {
            var newDest = dest;
            newDest--;
            if (newDest == 0)
            {
                newDest = _cups.Count;
            }
            return newDest;
        }

        public static void PrintStatus(int moveNum, List<int> pickUp, int dest)
        {
            Console.WriteLine($"-- move {moveNum}");
            Console.Write("cups: ");
            for (int i = 0; i < _cups.Count; i++)
            {
                if (i == _currentCupIndex)
                {
                    Console.Write($"({_cups[i]}) ");
                }
                else
                {
                    Console.Write(_cups[i] + " ");
                }
            }
            Console.WriteLine();
            Console.WriteLine("pick up: " + string.Join(", ", pickUp));
            Console.WriteLine("destination: " + dest);
            Console.WriteLine();
        }

        public static void ParseInput(string input)
        {
            foreach (char c in input)
            {
                _cups.Add(int.Parse(c.ToString()));
            }
            for (int i = 10; i <= 1000000; i++)
            {
                _cups.Add(i);
            }
        }
    }
}
