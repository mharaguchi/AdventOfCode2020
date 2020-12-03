﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public static class InputUtils
    {
        public static string[] SplitLinesIntoStringArray(string input)
        {
            var lines = Regex.Split(input, "\r\n|\r|\n");
            return lines;
        }

        public static List<int> SplitLinesIntoIntList(string input)
        {
            var lines = Regex.Split(input, "\r\n|\r|\n");
            int[] myInts = Array.ConvertAll(lines, int.Parse);
            return myInts.ToList();
        }

        public static List<int> SplitLineIntoIntList(string input)
        {
            var stringInts = Regex.Split(input, " ");
            int[] myInts = Array.ConvertAll(stringInts, int.Parse);
            return myInts.ToList();
        }

        public static List<Day2PasswordSet> SplitLinesIntoPasswordList(string input)
        {
            var passwordSets = new List<Day2PasswordSet>();
            var lines = Regex.Split(input, "\r\n|\r|\n");
            foreach(var line in lines)
            {
                if (line.Length == 0)
                {
                    continue;
                }
                var index = 0;
                var minStr = line.Substring(index, line.IndexOf("-"));
                var min = Int32.Parse(minStr);
                index += minStr.Length + 1;
                var maxStr = line.Substring(index, line.IndexOf(" ") - index);
                var max = Int32.Parse(maxStr);
                index += maxStr.Length + 1;
                var letter = line[index].ToString();
                index += 3;
                var password = line.Substring(index, line.Length - index);
                var passwordSet = new Day2PasswordSet
                {
                    Min = min,
                    Max = max,
                    Letter = letter,
                    Password = password
                };
                passwordSets.Add(passwordSet);
            }

            return passwordSets;
        }

    }
}
