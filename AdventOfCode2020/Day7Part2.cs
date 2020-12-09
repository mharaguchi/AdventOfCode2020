using AdventOfCode2020.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day7Part2
    {
        public static string _input = @"light red bags contain 1 bright white bag, 2 muted yellow bags.
dark orange bags contain 3 bright white bags, 4 muted yellow bags.
bright white bags contain 1 shiny gold bag.
muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
dark olive bags contain 3 faded blue bags, 4 dotted black bags.
vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
faded blue bags contain no other bags.
dotted black bags contain no other bags.";

        public static string _input2 = @"shiny gold bags contain 2 dark red bags.
dark red bags contain 2 dark orange bags.
dark orange bags contain 2 dark yellow bags.
dark yellow bags contain 2 dark green bags.
dark green bags contain 2 dark blue bags.
dark blue bags contain 2 dark violet bags.
dark violet bags contain no other bags.";

        private static Dictionary<string, List<Day7BagContents>> _colorDict;
        private static Dictionary<string, long> _dynamicNumBagsDict;
        //private static Dictionary<string, List<Day7BagContents>> _dynamicColorDict;

        public static long Run(string input)
        {
            //input = _input2;
            var lines = InputUtils.SplitLinesIntoStringArray(input);
            _colorDict = GetColorDict(lines);
            _dynamicNumBagsDict = new Dictionary<string, long>();
            
            //foreach (var kvp in _colorDict)
            //{
            //    var bagCount = GetBagCount(kvp.Key);
            //    if (!_dynamicNumBagsDict.ContainsKey(kvp.Key))
            //    {
            //        _dynamicNumBagsDict.Add(kvp.Key, bagCount);
            //    }
            //}
            return GetBagCount("shiny gold");
        }

        public static long GetBagCount(string color)
        {
            if (_dynamicNumBagsDict.ContainsKey(color))
            {
                return _dynamicNumBagsDict[color];
            }
            else
            {
                if (_colorDict.ContainsKey(color) && _colorDict[color].Count == 0)
                {
                    _dynamicNumBagsDict[color] = 0;
                    return 0;
                }
                else
                {
                    var bagCount = (long)0;
                    var innerColors = _colorDict[color];
                    foreach (var innerColor in innerColors)
                    {
                        bagCount += innerColor.NumBags * (1 + GetBagCount(innerColor.BagColor));
                    }
                    _dynamicNumBagsDict[color] = bagCount;
                    return bagCount;
                }
            }
        }

        //public static List<Day7BagContents> GetAllContainedColors(string color, List<Day7BagContents> containedColors)
        //{
        //    if (_dynamicColorDict.ContainsKey(color))
        //    {
        //        return _dynamicColorDict[color].Union(containedColors).ToList();
        //    }
        //    else
        //    {
        //        if (!_colorDict.ContainsKey(color)) {
        //            return containedColors;
        //        }
        //        else
        //        {
        //            var innerColors = _colorDict[color];
        //            foreach(var innerColor in innerColors)
        //            {
        //                if (containedColors.Contains(innerColor))
        //                {
        //                    continue;
        //                }
        //                else
        //                {
        //                    containedColors.Add(innerColor);
        //                    containedColors = containedColors.Union(GetAllContainedColors(innerColor.BagColor, containedColors)).ToList();
        //                }
        //            }
        //        }
        //    }
        //    return containedColors;
        //}

        public static Dictionary<string, List<Day7BagContents>> GetColorDict(string[] lines)
        {
            Dictionary<string, List<Day7BagContents>> colorDict = new Dictionary<string, List<Day7BagContents>>();
            foreach(var line in lines)
            {
                var innerBags = new List<Day7BagContents>();
                var tokens = line.Split(" bags contain ");
                var outerBagColor = tokens[0];
                var containsColors = tokens[1].Replace(" bag,", " bags,").Replace(" bag.", " bags.").Split(" bags.")[0].Split(" bags, ");
                foreach(var containsColor in containsColors)
                {
                    int bagCount = -1;
                    var bagCountChar = containsColor[0];
                    if (bagCountChar > '0' && bagCountChar <= '9')
                    {
                        bagCount = int.Parse(bagCountChar.ToString());
                        var colorStr = containsColor.Substring(2, containsColor.Length - 2);
                        innerBags.Add(new Day7BagContents { BagColor = colorStr, NumBags = bagCount });
                    }
                }
                if (innerBags.Count == 1 && innerBags[0].BagColor == " other")
                {
                    continue;
                }
                colorDict.Add(outerBagColor, innerBags);
            }
            return colorDict;
        }
    }
}
