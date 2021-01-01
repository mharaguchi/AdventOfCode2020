using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day23
    {
        public static string _input = @"389125467";
        public static List<int> _cups = new List<int>();
        public static int _currentCupIndex = 0;

        public static string Run(string input)
        {
            //input = _input;
            var moves = 100;
            
            ParseInput(input);

            for(int i = 0; i < moves; i++)
            {
                RunMove(i);
            }
            return string.Join("",_cups);
        }

        public static void RunMove(int moveNum)
        {
            var pickUp = new List<int>();
            var currentCup = _cups[_currentCupIndex];
            for(int i = 0; i < 3; i++)
            {
                pickUp.Add(_cups[(_currentCupIndex + i + 1) % _cups.Count]);
            }
            var dest = GetDestination(pickUp);
            
            PrintStatus(moveNum, pickUp, dest);

            for(int i = 0; i < 3; i++)
            {
                var pickedUp = pickUp[i];
                var sourceIndex = _cups.IndexOf(pickedUp);
                _cups.RemoveAt(sourceIndex);
                var destIndex = _cups.IndexOf(dest) + i + 1;
                _cups.Insert(destIndex, pickedUp);
            }
            _currentCupIndex = (_cups.IndexOf(currentCup) + 1) % _cups.Count;
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
                newDest = 9;
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
        }
    }
}
