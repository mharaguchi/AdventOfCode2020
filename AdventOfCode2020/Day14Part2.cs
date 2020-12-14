using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day14Part2
    {
        public static string _input = @"mask = 000000000000000000000000000000X1001X
mem[42] = 100
mask = 00000000000000000000000000000000X0XX
mem[26] = 1";

        public static string _mask = "";
        public static Dictionary<long, long> _mem = new Dictionary<long, long>();

        public static long Run(string input)
        {
            //input = _input;
            var lines = InputUtils.SplitLinesIntoStringArray(input);
            foreach(var line in lines)
            {
                ProcessLine(line);
            }
            var sum = (long)0;
            foreach(var kvp in _mem)
            {
                sum += kvp.Value;
            }
            return sum;
        }

        public static void ProcessLine(string line)
        {
            if (line.StartsWith("mask"))
            {
                _mask = line.Substring(7, line.Length - 7);
            }
            else
            {
                var tokens = StringUtils.SplitInOrder(line, new string[] { "mem[", "] = " });
                var loc = int.Parse(tokens[0]);
                var value = long.Parse(tokens[1]);
                var maskedLocs = ApplyMask(loc);
                foreach (var maskedLoc in maskedLocs)
                {
                    if (!_mem.ContainsKey(maskedLoc))
                    {
                        _mem.Add(maskedLoc, value);
                    }
                    else
                    {
                        _mem[maskedLoc] = value;
                    }
                }
            }
        }

        public static List<long> ApplyMask(long value)
        {
            var maskedLocs = new List<long>();
            var locBinaryStr = Convert.ToString(value, 2);
            if (locBinaryStr.Length < _mask.Length)
            {
                locBinaryStr = locBinaryStr.PadLeft(_mask.Length, '0');
            }
            char[] firstLocBinary = locBinaryStr.ToCharArray();

            var locBinaryList = new List<char[]>();
            locBinaryList.Add(firstLocBinary);

            for(int i = 0; i < _mask.Length; i++)
            {
                var thisMaskChar = _mask[i];
                if (thisMaskChar == '0')
                {
                    continue;
                }
                else if (thisMaskChar == '1')
                {
                    foreach (var thisLocBinary in locBinaryList)
                    {
                        var replacePos = thisLocBinary.Length - _mask.Length + i;
                        thisLocBinary[replacePos] = thisMaskChar;
                    }
                }
                else if (thisMaskChar == 'X') {
                    var locBinaryListCopy = new List<char[]>(locBinaryList);
                    var newLocBinaryList = new List<char[]>();

                    foreach (var thisLocBinary in locBinaryListCopy)
                    {
                        var replacePos = thisLocBinary.Length - _mask.Length + i;
                        var newVal1 = (char[])thisLocBinary.Clone();
                        newVal1[replacePos] = '0';
                        var newVal2 = (char[])thisLocBinary.Clone();
                        newVal2[replacePos] = '1';

                        newLocBinaryList.Add(newVal1);
                        newLocBinaryList.Add(newVal2);
                    }
                    locBinaryList = newLocBinaryList;
                }
            }
            foreach(var thisLocBinary in locBinaryList)
            {
                maskedLocs.Add(Convert.ToInt64(new string(thisLocBinary), 2));
            }
            return maskedLocs;
        }
    }
}
