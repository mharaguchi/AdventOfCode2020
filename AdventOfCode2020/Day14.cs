using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day14
    {
        public static string _input = @"mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
mem[8] = 11
mem[7] = 101
mem[8] = 0";

        public static string _mask = "";
        public static Dictionary<int, long> _mem = new Dictionary<int, long>();

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
                var masked = ApplyMask(value);
                if (!_mem.ContainsKey(loc))
                {
                    _mem.Add(loc, masked);
                }
                else
                {
                    _mem[loc] = masked;
                }
            }
        }

        public static long ApplyMask(long value)
        {
            var valueBinaryStr = Convert.ToString(value, 2);
            if (valueBinaryStr.Length < _mask.Length)
            {
                valueBinaryStr = valueBinaryStr.PadLeft(_mask.Length, '0');
            }
            char[] valueBinary = valueBinaryStr.ToCharArray();
            for(int i = 0; i < _mask.Length; i++)
            {
                var thisMaskChar = _mask[i];
                if (thisMaskChar == 'X')
                {
                    continue;
                }
                var replacePos = valueBinary.Length - _mask.Length + i;
                valueBinary[replacePos] = thisMaskChar;
            }
            return Convert.ToInt64(new string (valueBinary), 2);
        }
    }
}
