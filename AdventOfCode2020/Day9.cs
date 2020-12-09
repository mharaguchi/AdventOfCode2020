using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day9
    {
        public static readonly int PREAMBLE = 25;
        public static readonly int LOOKBACK = 25;
        public static string _input = @"35
20
15
25
47
40
62
55
65
95
102
117
150
182
127
219
299
277
309
576";

        public static long Run(string input)
        {
            //input = _input;
            var nums = InputUtils.SplitLinesIntoLongList(input);
            for(int i = PREAMBLE; i < nums.Count; i++)
            {
                var sum = nums[i];
                if (!Has2Addends(nums, sum, i))
                {
                    return GetSmallPlusLarge(nums, sum, i);
                }
            }
            return -1;
        }

        private static bool Has2Addends(List<long> nums, long sum, int index)
        {
            for (int i = index - LOOKBACK; i < index; i++)
            {
                var addend1 = nums[i];
                if (addend1 > sum)
                {
                    continue;
                }
                for (int j = i + 1; j < index; j++)
                {
                    var addend2 = nums[j];
                    if (addend1 + addend2 == sum)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static long GetSmallPlusLarge(List<long> nums, long targetSum, int index)
        {
            for (int i = 0; i < index; i++)
            {
                var small = long.MaxValue;
                var large = (long)-1;
                var addend1 = nums[i];
                var currentSum = addend1;
                small = addend1;
                large = addend1;
                for (int j = i + 1; j < index; j++)
                {
                    var thisAddend = nums[j];
                    if (thisAddend > large)
                    {
                        large = thisAddend;
                    }
                    if (thisAddend < small)
                    {
                        small = thisAddend;
                    }

                    currentSum += thisAddend;
                    if (currentSum == targetSum)
                    {
                        return small + large;
                    }
                    if (currentSum > targetSum)
                    {
                        break;
                    }
                }
            }
            return -1;
        }

    }
}
