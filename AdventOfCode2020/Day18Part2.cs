using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day18Part2
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
                var lineWithAdditionParens = AddParensAroundAddition(lineNoSpaces);
                sum += EvaluateExpression(lineWithAdditionParens);
            }
            return sum;
        }

        public static string AddParensAroundAddition(string line)
        {
            var newLine = line;
            var numPluses = line.Count(x => x == '+');
            for(int i = 1; i < numPluses + 1; i++)
            {
                var plusIndex = GetNthIndex(newLine, '+', i);
                newLine = AddParens(newLine, plusIndex);
            }
            return newLine;
        }

        public static string AddParens(string line, int targetIndex)
        {
            var newLine = line;
            var index = targetIndex;
            int openParenIndex = -1;
            int closeParenIndex = -1;
            var charBefore = line[index - 1];
            if (charBefore == ')')
            {
                openParenIndex = FindBeginParenIndex(line, index - 1);
            }
            else if (charBefore >= '0' && charBefore <= '9')
            {
                openParenIndex = targetIndex - 1;
            }
            var charAfter = line[index + 1];
            if (charAfter == '(')
            {
                closeParenIndex = FindEndParenIndex(line, index + 1);
            }
            else if (charAfter >= '0' && charAfter <= '9')
            {
                closeParenIndex = targetIndex + 2;
            }
            newLine = newLine.Insert(openParenIndex, "(");
            newLine = newLine.Insert(closeParenIndex + 1, ")");
            return newLine;
        }

        //from StackOverflow
        public static int GetNthIndex(string s, char t, int n)
        {
            int count = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == t)
                {
                    count++;
                    if (count == n)
                    {
                        return i;
                    }
                }
            }
            return -1;
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

        public static int FindBeginParenIndex(string expr, int endIndex)
        {
            var depth = 1;
            var index = endIndex;
            while (depth > 0)
            {
                index--;
                if (expr[index] == '(')
                {
                    depth--;
                }
                else if (expr[index] == ')')
                {
                    depth++;
                }
            }
            return index;
        }
    }
}
