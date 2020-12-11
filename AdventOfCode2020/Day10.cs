using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day10
    {
        public static string _input = @"";
        public static long Run(string input)
        {
            //input = _input
            var joltages = InputUtils.SplitLinesIntoIntList(input);
            var volt1 = 0;
            var volt3 = 0;
            joltages.Sort();
            var prev = 0;
            for (int i = 0; i < joltages.Count; i++)
            {
                if (joltages[i] - prev == 1)
                {
                    volt1++;
                }
                else if (joltages[i] - prev == 3)
                {
                    volt3++;
                }
                prev = joltages[i];
            }
            volt3++;
            return volt1 * volt3;
        }
    }
}
