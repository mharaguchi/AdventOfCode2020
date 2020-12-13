using AdventOfCode2020.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day13Part2
    {
        public static string _input = @"939
67,7,x,59,61";

        public static Dictionary<int, int> busNums = new Dictionary<int, int>();

        public static long Run(string input)
        {
            input = _input;
            var lines = InputUtils.SplitLinesIntoStringArray(input);
            var buses = SplitCSVLineIntoIntList(lines[1]);
            var maxBus = buses.Max();
            var maxBusPos = buses.IndexOf(maxBus);
            for (int i = 0; i < buses.Count; i++)
            {
                if (buses[i] > -1)
                {
                    busNums.Add(buses[i], i);
                }
            }

            var iteration = (long)1;
            while (true)
            {
                long timestamp = maxBus * iteration - maxBusPos;
                Console.WriteLine("Timestamp: " + timestamp.ToString());
                if (IsMatch(timestamp, buses))
                {
                    return timestamp;
                }
                iteration++;
            }
        }

        public static bool IsMatch(long timestamp, List<int> buses)
        {
            for(int i = 0; i < buses.Count; i++)
            {
                if (buses[i] == -1)
                {
                    continue;
                }
                if ((timestamp % buses[i] + i) % buses[i] != 0)
                {
                    return false;
                }
                //var waitTime = GetWaitTime(timestamp, buses[i]);
                //if (waitTime < 0)
                //{
                //    return false;
                //}
                //if (waitTime != i)
                //{
                //    return false;
                //}
            }
            return true;
        }

        public static int GetWaitTime(long timestamp, int bus)
        {
            var track = 0;
            while ((timestamp + track) % bus > 0)
            {
                track++;
                if (track > bus)
                {
                    return -1;
                }
            }
            return track;
        }

        public static List<int> SplitCSVLineIntoIntList(string input)
        {
            var ints = new List<int>();
            var stringInts = input.Split(',');
            foreach (var str in stringInts)
            {
                if (str.Length == 0)
                {
                    continue;
                }
                if (str.Equals("x"))
                {
                    ints.Add(-1);
                }
                else
                {
                    ints.Add(int.Parse(str));
                }
            }
            return ints;
        }
    }

}
