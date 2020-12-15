using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day15
    {
        public static string _input = @"14,1,17,0,3,20";
        //public static string _input = @"1,2,3";
        
        public static Dictionary<long, long> lastPosDict = new Dictionary<long, long>();
        //public static Dictionary<int, int> diffDict = new Dictionary<int, int>();

        public static long Run(string input)
        {
            input = _input;
            var nums = InputUtils.SplitLineIntoIntList(input, ",");
            for (long i = 0; i < nums.Count - 1; i++)
            {
                lastPosDict.Add(nums[(int)i], i);
            }
            long prevNum = nums[nums.Count - 1];
            for (long i = nums.Count; i < 30000000; i++)
            {
                if (lastPosDict.ContainsKey(prevNum))
                {
                    long thisNum = i - 1 - lastPosDict[prevNum];
                    lastPosDict[prevNum] = i - 1;
                    prevNum = thisNum;
                }
                else
                {
                    lastPosDict.Add(prevNum, i - 1);
                    prevNum = 0;
                }
            }
            return prevNum;
        }
    }
}
