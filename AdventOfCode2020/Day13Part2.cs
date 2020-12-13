using AdventOfCode2020.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public static List<int> _buses = new List<int>();

        public static long Run(string input)
        {
            var minIteration = 127064803049;
            //var minIteration = (long)1;
            //input = _input;
            var lines = InputUtils.SplitLinesIntoStringArray(input);
            _buses = SplitCSVLineIntoIntList(lines[1]);
            var maxBus = _buses.Max();
            var maxBusPos = _buses.IndexOf(maxBus);
            var firstBus = _buses[0];
            var iteration = GetFirstIteration(firstBus, maxBus, maxBusPos, minIteration);

            for (int i = 0; i < _buses.Count; i++)
            {
                if (_buses[i] > -1)
                {
                    busNums.Add(_buses[i], i);
                }
            }

            _buses.RemoveAll(x => x == -1);
            _buses.Sort();
            _buses.Reverse();

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            while (true)
            {
                long timestamp = maxBus * iteration - maxBusPos;
                //if (timestamp % firstBus > 0)
                //{
                //    iteration++;
                //    continue;
                //}
                Console.WriteLine("Timestamp: " + timestamp.ToString());

                if (IsMatchBusDict(timestamp))
                {
                    stopwatch.Stop();
                    TimeSpan ts = stopwatch.Elapsed;
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts.Hours, ts.Minutes, ts.Seconds,
                        ts.Milliseconds / 10);
                    Console.WriteLine("RunTime: " + elapsedTime);
                    return timestamp;
                }
                iteration += firstBus;
            }
        }

        public static long GetFirstIteration(int firstBus, int maxBus, int maxBusPos, long minIteration)
        {
            var found = false;
            var iteration = minIteration;
            while (!found)
            {
                var timestamp = maxBus * iteration - maxBusPos;
                if (timestamp % firstBus == 0)
                {
                    return iteration;
                }
                iteration++;
            }
            return -1;
        }

        public static bool IsMatchBusDict(long timestamp)
        {
            foreach (var bus in busNums)
            {
                if ((timestamp % bus.Key + bus.Value) % bus.Key != 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsMatchBigBusesFirst(long timestamp)
        {
            foreach(var bus in _buses)
            {
                if ((timestamp % bus + busNums[bus]) % bus != 0)
                {
                    return false;
                }
            }
            return true;
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
