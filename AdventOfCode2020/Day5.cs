using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public static class Day5
    {
        public static string _input = @"";

        public static int Run(string input)
        {
            var tracker = new bool[1023];
            var maxSeat = -1;
            var lines = InputUtils.SplitLinesIntoStringArray(input);
            foreach(var line in lines)
            {
                if (line.Length == 0)
                {
                    continue;
                }
                var seatNum = GetSeat(line);
                tracker[seatNum] = true;
                if (seatNum > maxSeat)
                {
                    maxSeat = seatNum;
                }
            }
            for (int i = 8; i < 1015; i++)
            {
                if (!tracker[i])
                {
                    return i;
                }
            }
            return -1;
        }

        public static int GetSeat(string input)
        {
            var row = GetRow(input.Substring(0, 7));
            var col = GetCol(input.Substring(7, 3));
            var seatNum = row * 8 + col;
            return seatNum;
        }

        public static int GetRow(string input)
        {
            var start = 0;
            var end = 127;
            for (int i = 0; i < input.Length - 1; i++)
            {
                if (input[i] == 'F')
                {
                    end -= (((end - start) / 2) + 1);
                }
                else
                {
                    start += (((end - start) / 2) + 1);
                }
            }
            if (input[input.Length - 1] == 'F')
            {
                return start;
            }
            return end;
        }

        public static int GetCol(string input)
        {
            var start = 0;
            var end = 7;
            for (int i = 0; i < input.Length - 1; i++)
            {
                if (input[i] == 'L')
                {
                    end -= (((end - start) / 2) + 1);
                }
                else
                {
                    start += (((end - start) / 2) + 1);
                }
            }
            if (input[input.Length - 1] == 'L')
            {
                return start;
            }
            return end;
        }
    }
}
