using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public static class Day1Part2
    {
        public static string input = @"";

        private static string[] lines;

        private static List<int> GetInputInts(string input)
        {
            var lines = Regex.Split(input, "\r\n|\r|\n");
            int[] myInts = Array.ConvertAll(lines, int.Parse);
            return myInts.ToList();
        }

        public static long Run(string input)
        {
            var nums = GetInputInts(input);
            for(int i = 0; i < nums.Count; i++)
            {
                var num1 = nums[i];
                for (int j = i+1; j < nums.Count; j++)
                {
                    var num2 = nums[j];
                    var diff = 2020 - num1 - num2;
                    if (nums.Contains(diff) && nums.LastIndexOf(diff) != i && nums.LastIndexOf(diff) != j)
                    {
                        Console.WriteLine($"num1: {@num1}, num2: {@num2}, diff: {@diff}", num1, num2, diff);
                        return num1 * num2 * diff;
                    }
                }
            }
            return -1;
        }
    }
}
