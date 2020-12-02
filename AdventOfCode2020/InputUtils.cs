using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public static class InputUtils
    {
        public static List<int> SplitLinesIntoIntList(string input)
        {
            var lines = Regex.Split(input, "\r\n|\r|\n");
            int[] myInts = Array.ConvertAll(lines, int.Parse);
            return myInts.ToList();
        }

        public static List<int> SplitLineIntoIntList(string input)
        {
            var stringInts = Regex.Split(input, " ");
            int[] myInts = Array.ConvertAll(stringInts, int.Parse);
            return myInts.ToList();
        }
    }
}
