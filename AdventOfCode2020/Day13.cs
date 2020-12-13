using AdventOfCode2020.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day13
    {
        public static string _input = @"939
7,13,x,x,59,x,31,19";

        public static long Run(string input)
        {
            //input = _input;
            var lines = InputUtils.SplitLinesIntoStringArray(input);
            var timestamp = long.Parse(lines[0]);
            var buses = SplitCSVLineIntoIntList(lines[1]);
            buses.Sort();
            var minWait = long.MaxValue;
            var busNum = -1;
            foreach(var bus in buses)
            {
                var waitTime = GetWaitTime(timestamp, bus);
                if (waitTime < minWait)
                {
                    minWait = waitTime;
                    busNum = bus;
                }
            }
            return minWait * busNum;
        }

        public static int GetWaitTime(long timestamp, int bus)
        {
            var track = 0;
            while ((timestamp + track) % bus > 0)
            {
                track++;
            }
            return track;
        }

        public static List<int> SplitCSVLineIntoIntList(string input)
        {
            var ints = new List<int>();
            var stringInts = input.Split(',');
            foreach (var str in stringInts)
            {
                if (str.Equals("x") || str.Length == 0)
                {
                    continue;
                }
                ints.Add(int.Parse(str));
            }
            return ints;
        }
    }

}
