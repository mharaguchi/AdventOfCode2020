using System.Linq;

namespace AdventOfCode2020
{
    public static class Day2
    {
        //public static string input = @"\r\n";
        public static string input = @"1-3 a: abcde
1-3 b: cdefg
2-9 c: ccccccccc";

        public static int Run(string input)
        {
            int matches = 0;
            var passwordSets = InputUtils.SplitLinesIntoPasswordList(input);
            foreach(var passwordSet in passwordSets)
            {
                var count = passwordSet.Password.Count(c => c == passwordSet.Letter.ToCharArray()[0]);
                if (count >= passwordSet.Min && count <= passwordSet.Max)
                {
                    matches++;
                }
            }
            return matches;
        }

        public static int RunPart2(string input)
        {
            int matches = 0;
            var passwordSets = InputUtils.SplitLinesIntoPasswordList(input);
            foreach (var passwordSet in passwordSets)
            {
                var password = passwordSet.Password;
                var letter = passwordSet.Letter;
                if ((password[passwordSet.Min-1] == letter.ToCharArray()[0] && password[passwordSet.Max-1] != letter.ToCharArray()[0]) ||
                    (password[passwordSet.Min-1] != letter.ToCharArray()[0] && password[passwordSet.Max-1] == letter.ToCharArray()[0]))
                {
                    matches++;
                }
            }
            return matches;
        }

    }
}
