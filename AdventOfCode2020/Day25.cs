using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day25
    {
        public static string _input = @"5764801
17807724";

        public static long Run(string input)
        {
            //input = _input;
            var tokens = InputUtils.SplitLinesIntoLongList(input);
            var cardPublicKey = tokens[0];
            var doorPublicKey = tokens[1];

            var cardLoopNumber = GetLoopNumber(cardPublicKey);
            var encryptionKey = RunLoops(cardLoopNumber, doorPublicKey);

            return encryptionKey;
        }

        public static int GetLoopNumber(long key)
        {
            var count = 1;
            var subject = 7;
            var loopValue = 1;
            while (true)
            {
                loopValue = (loopValue * subject) % 20201227;
                if (loopValue == key)
                {
                    return count;
                }
                count++;
            }
        }

        public static long RunLoops(int numLoops, long doorPublicKey)
        {
            long loopValue = 1;
            var subject = doorPublicKey;
            for(int i = 0; i < numLoops; i++)
            {
                loopValue = (loopValue * subject) % 20201227;
            }
            return loopValue;
        }
    }
}
