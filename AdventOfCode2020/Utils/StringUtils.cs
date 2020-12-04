using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public static class StringUtils
    {
        public static List<string> SplitInOrder(string input, string[] splits)
        {
            var tokens = new List<string>();
            var inputTracker = 0;
            var splitsTracker = 0;
            var thisToken = new StringBuilder();
            while (inputTracker < input.Length)
            {
                var remainingString = input.Substring(inputTracker, input.Length - inputTracker);
                if (splitsTracker == splits.Length)
                {
                    tokens.Add(remainingString);
                    return tokens;
                }
                if (remainingString.StartsWith(splits[splitsTracker])) {
                    inputTracker += splits[splitsTracker].Length;
                    splitsTracker++;
                    if (thisToken.ToString().Length > 0)
                    {
                        tokens.Add(thisToken.ToString());
                    }
                    thisToken = new StringBuilder();
                }
                else {
                    thisToken.Append(input[inputTracker]);
                    inputTracker++;
                }
            }
            return tokens;
        }
    }
}
