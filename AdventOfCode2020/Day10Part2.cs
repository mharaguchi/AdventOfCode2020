using AdventOfCode2020.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day10Part2
    {
        //        public static string _input = @"16
        //10
        //15
        //5
        //1
        //11
        //7
        //19
        //6
        //12
        //4";

        public static string _input = @"28
33
18
42
31
14
46
20
48
47
24
23
49
45
19
38
39
11
1
32
25
35
8
17
7
9
4
2
34
10
3";

        public static Dictionary<int, long> dynamicCombosDict = new Dictionary<int, long>();

        public static long Run(string input)
        {
            //input = _input;
            var joltageIntList = InputUtils.SplitLinesIntoIntList(input);
            joltageIntList.Sort();
            joltageIntList.Reverse();
            long combos = 0;
            var joltageList = new List<Day10Joltage>();
            for (int i = 0; i < joltageIntList.Count; i++)
            {
                joltageList.Add(new Day10Joltage { Joltage = joltageIntList[i], Pos = i });
            }
            var nextJoltages = GetValidNextJoltages(joltageList, 0);
            foreach (var joltage in nextJoltages)
            {
                combos += GetCombinations(joltageList, joltage.Pos, 0);
            }
            return combos;
        }

        public static long GetCombinations(List<Day10Joltage> joltageList, int index, long combos)
        {
            //get remaining sequence & hash
            var thisSequence = joltageList.GetRange(index, joltageList.Count - index);
            var hash = GetSequenceHashCode(thisSequence);

            if (dynamicCombosDict.ContainsKey(hash))
            {   //If sequence exists in dynamic dictionary, return value
                return dynamicCombosDict[hash];
            }

            var nextJoltages = GetValidNextJoltages(joltageList, index);
            if (nextJoltages.Count == 0 && joltageList[index].Joltage - 3 > 0)
            {
                dynamicCombosDict.Add(hash, 0);
                return 0;
            }
            else if (nextJoltages.Count == 0 && joltageList[index].Joltage - 3 <= 0)
            {
                dynamicCombosDict.Add(hash, 1);
                return 1;
            }
            else if (nextJoltages.Count > 0 && joltageList[index].Joltage - 3 <= 0)
            {
                foreach(var nextJoltage in nextJoltages)
                {
                    combos += GetCombinations(joltageList, nextJoltage.Pos, combos);
                }
                combos += 1;
                dynamicCombosDict.Add(hash, combos);
            }
            else if (nextJoltages.Count > 0 && joltageList[index].Joltage - 3 > 0)
            {
                foreach (var nextJoltage in nextJoltages)
                {
                    combos += GetCombinations(joltageList, nextJoltage.Pos, combos);
                }
                dynamicCombosDict.Add(hash, combos);
            }
            return combos;
        }

        public static List<Day10Joltage> GetValidNextJoltages(List<Day10Joltage> joltageList, int index)
        {
            var currentJoltage = joltageList[index];
            var nextJoltages = new List<Day10Joltage>();
            for (int i = 1; i < 4; i++) {
                if (index + i < joltageList.Count && currentJoltage.Joltage - joltageList[index + i].Joltage <= 3)
                {
                    nextJoltages.Add(joltageList[index + i]);
                }
            }
            return nextJoltages;
        }

        public static int GetSequenceHashCode<T>(this IList<T> sequence)
        {
            const int seed = 487;
            const int modifier = 31;

            unchecked
            {
                return sequence.Aggregate(seed, (current, item) =>
                    (current * modifier) + item.GetHashCode());
            }
        }
    }
}
