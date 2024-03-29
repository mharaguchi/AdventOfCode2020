﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day18
    {
        public static string _input = @"((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2";

        public static long Run(string input)
        {
            //input = _input;

            long sum = 0;
            var lines = InputUtils.SplitLinesIntoStringArray(input);
            foreach(var line in lines)
            {
                var lineNoSpaces = line.Replace(" ", "");
                sum += EvaluateExpression(lineNoSpaces);
            }
            return sum;
        }

        public static long EvaluateExpression(string expr)
        {
            long current = -1;
            var track = 0;
            char opr = '?';
            while (track <= expr.Length - 1) {
                var thisChar = expr[track];
                if (thisChar >= '0' && thisChar <= '9')
                {
                    var thisNum = long.Parse(thisChar.ToString());
                    if (current == -1)
                    {
                        current = thisNum;
                    }
                    else
                    {
                        current = ProcessPiece(current, thisNum, opr);
                    }
                    track++;
                    continue;
                }
                if (thisChar == '+' || thisChar == '*')
                {
                    opr = thisChar;
                    track++;
                    continue;
                }
                if (thisChar == '(')
                {
                    var endParenIndex = FindEndParenIndex(expr, track);
                    var subExprLength = endParenIndex - track;
                    var subExprString = expr.Substring(track + 1, subExprLength - 1);
                    var subExprResult = EvaluateExpression(subExprString);
                    if (current == -1)
                    {
                        current = subExprResult;
                    }
                    else
                    {
                        current = ProcessPiece(current, subExprResult, opr);
                    }
                    track += subExprLength + 1;
                    continue;
                }
            }
            return current;
        }

        public static long ProcessPiece(long current, long operand, char opr)
        {
            if (opr == '+')
            {
                current += operand;
            }
            else
            {
                current *= operand;
            }
            return current;
        }

        public static int FindEndParenIndex(string expr, int startIndex)
        {
            var depth = 1;
            var index = startIndex;
            while(depth > 0)
            {
                index++;
                if (expr[index] == '(')
                {
                    depth++;
                }
                else if (expr[index] == ')')
                {
                    depth--;
                }
            }
            return index;
        }
    }
}
