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
            var nonBlank = new List<string>();
            foreach(var line in lines)
            {
                if (line.Length > 0)
                {
                    nonBlank.Add(line);
                }
            }
            return nonBlank.ToArray();
        }

        public static List<int> SplitLinesIntoIntList(string input)
        {
            var lines = Regex.Split(input, "\r\n|\r|\n");
            if (lines[lines.Length - 1].Length == 0)
            {
                lines = lines.SkipLast(1).ToArray();
            }
            int[] myInts = Array.ConvertAll(lines, int.Parse);
            return myInts.ToList();
        }

        public static List<long> SplitLinesIntoLongList(string input)
        {
            var lines = Regex.Split(input, "\r\n|\r|\n");
            if (lines[lines.Length - 1].Length == 0)
            {
                lines = lines.SkipLast(1).ToArray();
            }
            long[] myLongs = Array.ConvertAll(lines, long.Parse);
            return myLongs.ToList();
        }

        public static List<int> SplitLineIntoIntList(string input, string separator)
        {
            var stringInts = Regex.Split(input, separator);
            if (stringInts[stringInts.Length - 1].Length == 0)
            {
                stringInts = stringInts.SkipLast(1).ToArray();
            }
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
                var tokens = StringUtils.SplitInOrder(line, new string[] { "-", " ", ": " });
                var passwordSet = new Day2PasswordSet
                {
                    Min = int.Parse(tokens[0]),
                    Max = int.Parse(tokens[1]),
                    Letter = tokens[2],
                    Password = tokens[3]
                };
                passwordSets.Add(passwordSet);
            }

            return passwordSets;
        }

        public static string[] SplitInputByBlankLines(string input)
        {
            if (input.Contains("\r\r"))
            {
                return input.Split("\r\r");
            }
            if (input.Contains("\n\n"))
            {
                return input.Split("\n\n");
            }
            if (input.Contains("\r\n\r"))
            {
                return input.Split("\r\n\r");
            }
            if (input.Contains("\n\r\n"))
            {
                return input.Split("\n\r\n");
            }
            return new string[] { input };
        }
    }
}
