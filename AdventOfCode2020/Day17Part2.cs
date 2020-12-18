using System;
using System.Linq;

namespace AdventOfCode2020
{
    public static class Day17Part2
    {
        public static string _input = @".#.
..#
###";


        public static readonly int cycles = 6;
        public static bool[] values;
        public static int inputWidth = -1;
        public static int inputHeight = -1;
        public static int wIncrement = 0;
        public static int zIncrement = 0;
        public static int yIncrement = 0;
        public static int xIncrement = 1;
        public static int maxWidth = -1;
        public static int maxHeight = -1;
        public static int maxZ = 2 * cycles + 1;
        public static int maxW = 2 * cycles + 1;
        public static int maxSize = -1;

        public static long Run(string input)
        {
            //input = _input;
            var lines = InputUtils.SplitLinesIntoStringArray(input);
            inputWidth = lines[0].Length;
            inputHeight = lines.Length;
            maxWidth =  inputWidth + 2 * cycles;
            maxHeight = inputHeight + 2 * cycles;
            yIncrement = maxWidth;
            zIncrement = maxWidth * maxHeight;
            wIncrement = maxZ * maxWidth * maxHeight;
            maxSize = maxWidth * maxHeight * maxZ * maxW;

            values = new bool[maxSize];

            Initialize(lines);
            //PrintValues(0);

            for (int i = 1; i <= cycles; i++)
            {
                RunCycle(i);
                //PrintValues(i);
            }

            var count = values.Where(x => x == true).Count();
            return count;
        }

        public static void Initialize(string[] lines)
        {
            var startingX = (maxWidth / 2) - (lines[0].Length / 2);
            var startingY = (maxHeight / 2) - (lines.Length / 2);
            var zLoc = maxZ / 2;
            var wLoc = maxW / 2;
            for (int y = 0; y < lines.Length; y++)
            {
                var yLoc = startingY + y;
                for (int x = 0; x < lines[0].Length; x++)
                {
                    var thisChar = lines[y][x];
                    var xLoc = startingX + x;
                    var thisPos = GetPos(xLoc, yLoc, zLoc, wLoc);
                    values[thisPos] = thisChar == '#';
                }
            }
        }

        public static void RunCycle(int cycleNum)
        {
            var newValues = new bool[maxSize];

            var startingActiveX = (maxWidth / 2) - (inputWidth / 2) - cycleNum;
            var endingActiveX = startingActiveX + inputWidth + cycleNum * 2 - 1;
            var startingActiveY = (maxHeight / 2) - (inputHeight / 2) - cycleNum;
            var endingActiveY = startingActiveY + inputHeight + cycleNum * 2 - 1;
            var startingActiveZ = (maxZ / 2) - cycleNum;
            var endingActiveZ = startingActiveZ + cycleNum * 2;
            var startingActiveW = startingActiveZ;
            var endingActiveW = endingActiveZ;

            for (int w = startingActiveW; w <= endingActiveW; w++)
            {
                for (int z = startingActiveZ; z <= endingActiveZ; z++)
                {
                    for (int y = startingActiveY; y <= endingActiveY; y++)
                    {
                        for (int x = startingActiveX; x <= endingActiveX; x++)
                        {
                            var thisPos = GetPos(x, y, z, w);
                            newValues[thisPos] = GetNewValue(x, y, z, w);
                        }
                    }
                }
            }
            values = newValues;
        }

        //public static void PrintValues(int cycleNum)
        //{
        //    var startingActiveX = (maxWidth / 2) - (inputWidth / 2) - cycleNum;
        //    var endingActiveX = startingActiveX + inputWidth + cycleNum * 2 - 1;
        //    var startingActiveY = (maxHeight / 2) - (inputHeight / 2) - cycleNum;
        //    var endingActiveY = startingActiveY + inputHeight + cycleNum * 2 - 1;
        //    var startingActiveZ = (maxZ / 2) - cycleNum;
        //    var endingActiveZ = startingActiveZ + cycleNum * 2;

        //    for (int z = startingActiveZ; z <= endingActiveZ; z++)
        //    {
        //        Console.WriteLine("z=" + (z - maxZ / 2).ToString());
        //        for (int y = startingActiveY; y <= endingActiveY; y++)
        //        {
        //            for (int x = startingActiveX; x <= endingActiveX; x++)
        //            {
        //                var thisPos = GetPos(x, y, z, 0);
        //                var charValue = values[thisPos] ? '#' : '.';
        //                Console.Write(charValue);
        //            }
        //            Console.WriteLine();
        //        }
        //    }
        //    Console.WriteLine();
        //}

        public static int GetPos(int x, int y, int z, int w)
        {
            return w * wIncrement + z * zIncrement + y * yIncrement + x;
        }

        public static bool GetNewValue(int x, int y, int z, int w)
        {
            var pos = GetPos(x, y, z, w);
            var curVal = values[pos];
            var activeCount = 0;

            for (int newX = x - 1; newX <= x + 1; newX++)
            {
                if (newX < 0 || newX > maxWidth - 1)
                {
                    continue;
                }
                for (int newY = y - 1; newY <= y + 1; newY++)
                {
                    if (newY < 0 || newY > maxHeight - 1)
                    {
                        continue;
                    }
                    for (int newZ = z - 1; newZ <= z + 1; newZ++)
                    {
                        if (newZ < 0 || newZ > maxZ - 1)
                        {
                            continue;
                        }
                        for (int newW = w - 1; newW <= w + 1; newW++)
                        {
                            if (newW < 0 || newW > maxW - 1)
                            {
                                continue;
                            }
                            if (newX == x && newY == y && newZ == z && newW == w)
                            {
                                continue;
                            }
                            var newPos = GetPos(newX, newY, newZ, newW);
                            if (values[newPos])
                            {
                                activeCount++;
                            }
                        }
                    }
                }
            }

            if (curVal && (activeCount == 2 || activeCount == 3))
            {
                return true;
            }
            if (!curVal && (activeCount == 3))
            {
                return true;
            }
            return false;
        }
    }
}
