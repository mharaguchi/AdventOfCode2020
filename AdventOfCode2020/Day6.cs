using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day6
    {
        public static string _input = @"abc

a
b
c

ab
ac

a
a
a
a

b";

        public static int Run(string input)
        {
            var sum = 0;
            //input = _input;
            var groups = InputUtils.SplitInputByBlankLines(input);
            foreach(var group in groups)
            {
                var matchedChars = new List<char>();
                var lines = InputUtils.SplitLinesIntoStringArray(group);
                for (int j = 0; j < lines.Length; j++) 
                {
                    var line = lines[j];
                    if (j == 0)
                    {
                        matchedChars = lines[0].ToCharArray().ToList();
                        continue;
                    }
                    var stillMatchedChars = new List<char>(matchedChars);
                    for (int i = 0; i < matchedChars.Count; i++)
                    {
                        var matchedChar = matchedChars[i];
                        if (matchedChar >= 'a' && matchedChar <= 'z' && !line.Contains(matchedChar))
                        {
                            stillMatchedChars.Remove(matchedChar);
                        }
                    }
                    matchedChars = stillMatchedChars;
                }
                sum += matchedChars.Count;
            }
            return sum;
        }
    }
}
