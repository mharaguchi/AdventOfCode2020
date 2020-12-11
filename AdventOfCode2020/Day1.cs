using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public static class Day1
    {
        public static string input = @"";

        private static List<int> GetInputInts(string input)
        {
            var lines = Regex.Split(input, "\r\n|\r|\n");
            int[] myInts = Array.ConvertAll(lines, int.Parse);
            return myInts.ToList();
        }

        public static int Run(string input)
        {
            var nums = GetInputInts(input);
            foreach(var num in nums)
            {
                var diff = 2020 - num;
                if (nums.Contains(diff))
                {
                    return num * diff;
                }
            }
            return -1;
        }
    }
}
