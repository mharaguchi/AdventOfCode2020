using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day11
    {
        public static string[] lines;
        public static string[] newLines;

        public static int Run(string input)
        {
            //input = _input;
            lines = InputUtils.SplitLinesIntoStringArray(input);
            newLines = new string[lines.Length];
            return GetOccupiedSeats();
        }

        public static int GetOccupiedSeats()
        {
            var changes = -1;
            var iterations = 0;
            lines.CopyTo(newLines, 0);
            while(changes != 0)
            {
                iterations++;
                Console.WriteLine("Running iteration " + iterations.ToString());
                changes = CalcSeatsAndReturnChanges();
                newLines.CopyTo(lines, 0);
                Console.WriteLine("Changes: " + changes.ToString());
            }

            var occupied = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                occupied += lines[i].Count(c => c == '#');
            }
            return occupied;
        }

        public static int CalcSeatsAndReturnChanges()
        {
            var changes = 0;
            for(int i = 0; i < lines[0].Length; i++)
            {
                for (int j = 0; j < lines.Length; j++)
                {
                    var newChar = CalcSeat(i, j);
                    newLines[j] = newLines[j].Remove(i, 1).Insert(i, newChar.ToString());
                    if (newChar != lines[j][i])
                    {
                        changes++;
                    }
                }
            }
            return changes;
        }

        public static char CalcSeat(int x, int y)
        {
            if (lines[y][x] == '.')
            {
                return '.';
            }
            var occupied = 0;
            occupied += CalcLeftSeats(x, y);
            occupied += CalcTopBottom(x, y);
            occupied += CalcRightSeats(x, y);
            if (lines[y][x] == 'L' && occupied == 0)
            {
                return '#';
            }
            if (lines[y][x] == '#' && occupied > 3)
            {
                return 'L';
            }
            return lines[y][x];
        }

        public static int CalcLeftSeats(int x, int y)
        {
            var occupied = 0;
            if (x - 1 >= 0)
            {
                if (y - 1 >= 0)
                {
                    if (lines[y - 1][x - 1] == '#')
                    {
                        occupied++;
                    }
                }
                if (lines[y][x - 1] == '#')
                {
                    occupied++;
                }
                if (y + 1 < lines.Length)
                {
                    if (lines[y+1][x-1] == '#')
                    {
                        occupied++;
                    }
                }
            }
            return occupied;
        }

        public static int CalcTopBottom(int x, int y)
        {
            var occupied = 0;
            if (y - 1 >= 0)
            {
                if (lines[y - 1][x] == '#')
                {
                    occupied++;
                }
            }
            if (y + 1 < lines.Length)
            {
                if (lines[y + 1][x] == '#')
                {
                    occupied++;
                }
            }
            return occupied;
        }

        public static int CalcRightSeats(int x, int y)
        {
            var occupied = 0;
            if (x + 1 < lines[0].Length)
            {
                if (y - 1 >= 0)
                {
                    if (lines[y - 1][x + 1] == '#')
                    {
                        occupied++;
                    }
                }
                if (lines[y][x + 1] == '#')
                {
                    occupied++;
                }
                if (y + 1 < lines.Length)
                {
                    if (lines[y + 1][x + 1] == '#')
                    {
                        occupied++;
                    }
                }
            }
            return occupied;
        }
    }
}
