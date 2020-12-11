using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public static class Day3
    {
        public static string input = @"1-3 a: abcde
1-3 b: cdefg
2-9 c: ccccccccc";

        public static long Run(string input)
        {
            var map = InputUtils.SplitLinesIntoStringArray(input);
            var treeValues = new List<int>();
            treeValues.Add(GetTreesValue(map, 1, 1));
            treeValues.Add(GetTreesValue(map, 3, 1));
            treeValues.Add(GetTreesValue(map, 5, 1));
            treeValues.Add(GetTreesValue(map, 7, 1));
            treeValues.Add(GetTreesValue(map, 1, 2));
            long product = 1;
            foreach(var treeValue in treeValues)
            {
                product *= (long)treeValue;
            }
            return product;
        }

        private static int GetTreesValue(string[] map, int routeX, int routeY)
        {
            var locX = 0;
            var locY = 0;
            int trees = 0;
            var width = map[0].Length;
            var height = map.Length;
            var end = false;
            while (!end)
            {
                locX += routeX;
                if (locX >= width)
                {
                    locX = locX % width;
                }
                locY += routeY;
                if (locY >= height - 1)
                {
                    break;
                }
                if (map[locY][locX] == '#')
                {
                    trees++;
                }
            }
            return trees;
        }
    }
}
