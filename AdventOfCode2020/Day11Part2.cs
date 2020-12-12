using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day11Part2
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
            //occupied += CalcLeftSeats(x, y);
            //occupied += CalcTopBottom(x, y);
            //occupied += CalcRightSeats(x, y);
            occupied += CalcUpLeftDiag(x, y);
            occupied += CalcUp(x, y);
            occupied += CalcUpRightDiag(x, y);
            occupied += CalcDownLeftDiag(x, y);
            occupied += CalcDown(x, y);
            occupied += CalcDownRightDiag(x, y);
            occupied += CalcLeft(x, y);
            occupied += CalcRight(x, y);
            if (lines[y][x] == 'L' && occupied == 0)
            {
                return '#';
            }
            if (lines[y][x] == '#' && occupied > 4)
            {
                return 'L';
            }
            return lines[y][x];
        }

        //public static int CalcLeftSeats(int x, int y)
        //{
        //    var occupied = 0;
        //    occupied += CalcUpLeftDiag(x, y);
        //    occupied += CalcLeft(x, y);
        //    occupied += CalcDownLeftDiag(x, y);
        //    if (x - 1 >= 0)
        //    {

        //        if (lines[y][x - 1] == '#')
        //        {
        //            occupied++;
        //        }
        //        if (y + 1 < lines.Length)
        //        {
        //            if (lines[y+1][x-1] == '#')
        //            {
        //                occupied++;
        //            }
        //        }
        //    }
        //    return occupied;
        //}

        public static int CalcUpLeftDiag(int x, int y)
        {
            var thisX = x - 1;
            var thisY = y - 1;
            var thisChar = '.';
            while (thisX >= 0 && thisY >= 0 && thisChar == '.')
            {
                thisChar = lines[thisY][thisX];
                if (thisChar == '#')
                {
                    return 1;
                }
                thisY--;
                thisX--;
            }
            return 0;
        }

        public static int CalcLeft(int x, int y)
        {
            var thisX = x - 1;
            var thisChar = '.';
            while (thisX >= 0 && thisChar == '.')
            {
                thisChar = lines[y][thisX];
                if (thisChar == '#')
                {
                    return 1;
                }
                thisX--;
            }
            return 0;
        }

        public static int CalcDownLeftDiag(int x, int y)
        {
            var thisX = x - 1;
            var thisY = y + 1;
            var thisChar = '.';
            while (thisX >= 0 && thisY < lines.Length && thisChar == '.')
            {
                thisChar = lines[thisY][thisX];
                if (thisChar == '#')
                {
                    return 1;
                }
                thisX--;
                thisY++;
            }
            return 0;
        }

        public static int CalcUp(int x, int y)
        {
            var thisY = y - 1;
            var thisChar = '.';
            while (thisY >= 0 && thisChar == '.')
            {
                thisChar = lines[thisY][x];
                if (thisChar == '#')
                {
                    return 1;
                }
                thisY--;
            }
            return 0;
        }

        public static int CalcDown(int x, int y)
        {
            var thisY = y + 1;
            var thisChar = '.';
            while (thisY < lines.Length && thisChar == '.')
            {
                thisChar = lines[thisY][x];
                if (thisChar == '#')
                {
                    return 1;
                }
                thisY++;
            }
            return 0;
        }

        public static int CalcUpRightDiag(int x, int y)
        {
            var thisX = x + 1;
            var thisY = y - 1;
            var thisChar = '.';
            while (thisX < lines[0].Length && thisY >= 0 && thisChar == '.')
            {
                thisChar = lines[thisY][thisX];
                if (thisChar == '#')
                {
                    return 1;
                }
                thisX++;
                thisY--;
            }
            return 0;
        }

        public static int CalcRight(int x, int y)
        {
            var thisX = x + 1;
            var thisChar = '.';
            while (thisX < lines[0].Length && thisChar == '.')
            {
                thisChar = lines[y][thisX];
                if (thisChar == '#')
                {
                    return 1;
                }
                thisX++;
            }
            return 0;
        }

        public static int CalcDownRightDiag(int x, int y)
        {
            var thisX = x + 1;
            var thisY = y + 1;
            var thisChar = '.';
            while (thisX < lines[0].Length && thisY < lines.Length && thisChar == '.')
            {
                thisChar = lines[thisY][thisX];
                if (thisChar == '#')
                {
                    return 1;
                }
                thisX++;
                thisY++;
            }
            return 0;
        }

        //public static int CalcTopBottom(int x, int y)
        //{
        //    var occupied = 0;
        //    if (y - 1 >= 0)
        //    {
        //        if (lines[y - 1][x] == '#')
        //        {
        //            occupied++;
        //        }
        //    }
        //    if (y + 1 < lines.Length)
        //    {
        //        if (lines[y + 1][x] == '#')
        //        {
        //            occupied++;
        //        }
        //    }
        //    return occupied;
        //}

        //public static int CalcRightSeats(int x, int y)
        //{
        //    var occupied = 0;
        //    if (x + 1 < lines[0].Length)
        //    {
        //        if (y - 1 >= 0)
        //        {
        //            if (lines[y - 1][x + 1] == '#')
        //            {
        //                occupied++;
        //            }
        //        }
        //        if (lines[y][x + 1] == '#')
        //        {
        //            occupied++;
        //        }
        //        if (y + 1 < lines.Length)
        //        {
        //            if (lines[y + 1][x + 1] == '#')
        //            {
        //                occupied++;
        //            }
        //        }
        //    }
        //    return occupied;
        //}
    }
}
