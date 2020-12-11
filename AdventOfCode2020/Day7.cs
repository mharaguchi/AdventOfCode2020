using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day7
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

        private static Dictionary<string, List<string>> _colorDict;
        private static Dictionary<string, List<string>> _dynamicColorDict;

        public static int Run(string input)
        {
            var matches = 0;
            //input = _input;
            var lines = InputUtils.SplitLinesIntoStringArray(input);
            _colorDict = GetColorDict(lines);
            _dynamicColorDict = new Dictionary<string, List<string>>();
            
            foreach (var kvp in _colorDict)
            {
                var colors = GetAllContainedColors(kvp.Key, new List<string>());
                _dynamicColorDict.Add(kvp.Key, colors);
            }
            foreach(var _dynamicColorDictEntry in _dynamicColorDict)
            {
                var colors = _dynamicColorDictEntry.Value;
                if (_dynamicColorDictEntry.Value.Contains("shiny gold"))
                {
                    Console.WriteLine(_dynamicColorDictEntry.Key);
                    matches++;
                }
            }
            return matches;
        }

        public static List<string> GetAllContainedColors(string color, List<string> containedColors)
        {
            if (_dynamicColorDict.ContainsKey(color))
            {
                return _dynamicColorDict[color].Union(containedColors).ToList();
            }
            else
            {
                if (!_colorDict.ContainsKey(color)) {
                    return containedColors.ToList();
                }
                else
                {
                    var innerColors = _colorDict[color];
                    foreach(var innerColor in innerColors)
                    {
                        if (containedColors.Contains(innerColor))
                        {
                            continue;
                        }
                        else
                        {
                            containedColors.Add(innerColor);
                            containedColors = containedColors.Union(GetAllContainedColors(innerColor, containedColors)).ToList();
                        }
                    }
                }
            }
            return containedColors;
        }

        public static Dictionary<string, List<string>> GetColorDict(string[] lines)
        {
            Dictionary<string, List<string>> colorDict = new Dictionary<string, List<string>>();
            foreach(var line in lines)
            {
                var innerBagColors = new List<string>();
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
                    }
                    var colorStr = containsColor.Substring(2, containsColor.Length - 2);
                    if (colorStr.StartsWith(" "))
                    {
                        Console.WriteLine("Foundone: " + colorStr);
                    }
                    innerBagColors.Add(colorStr);
                }
                if (innerBagColors.Count == 1 && innerBagColors[0] == " other")
                {
                    continue;
                }
                colorDict.Add(outerBagColor, innerBagColors);
            }
            return colorDict;
        }
    }
}
