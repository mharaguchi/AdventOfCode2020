using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day16
    {
        public static string _input = @"";
        public static bool[] valid = new bool[999];

        public static long Run(string input)
        {
            var tokens = InputUtils.SplitInputByBlankLines(input);
            MarkValid(InputUtils.SplitLinesIntoStringArray(tokens[0]));
            var ticketLines = InputUtils.SplitLinesIntoStringArray(tokens[2]);
            return GetInvalidSum(ticketLines);
        }

        public static void MarkValid(string[] rulesLines)
        {
            foreach(var ruleLine in rulesLines)
            {
                var tokens = StringUtils.SplitInOrder(ruleLine, new string[] { ": ", "-", " or ", "-" });
                var min1 = int.Parse(tokens[1]);
                var max1 = int.Parse(tokens[2]);
                var min2 = int.Parse(tokens[3]);
                var max2 = int.Parse(tokens[4]);
                for (int i = min1; i <= max1; i++)
                {
                    valid[i] = true;
                }
                for (int j = min2; j <= max2; j++)
                {
                    valid[j] = true;
                }
            }
        }

        public static long GetInvalidSum(string[] ticketLines)
        {
            long sum = (long)0;
            for (int i = 1; i < ticketLines.Length; i++) //skip "nearby tickets" line
            {
                if (ticketLines[i].Length == 0)
                {
                    continue;
                }
                var nums = InputUtils.SplitLineIntoIntList(ticketLines[i], ",");
                foreach(var num in nums)
                {
                    if (!valid[num])
                    {
                        sum += num;
                    }
                }
            }
            return sum;
        }
    }
}
