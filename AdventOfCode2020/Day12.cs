using AdventOfCode2020.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day12
    {
        public static string _input = @"";

        public static int Run(string input)
        {
            var lines = InputUtils.SplitLinesIntoStringArray(input);
            Day12Pos startingPos = new Day12Pos()
            {
                Direction = 90,
                X = 0,
                Y = 0
            };
            var curPos = startingPos;
            foreach (var line in lines)
            {
                curPos = ProcessCommand(curPos, line);
            }
            return Math.Abs(curPos.X) + Math.Abs(curPos.Y);
        }

        public static Day12Pos ProcessCommand(Day12Pos currentPos, string command)
        {
            var cmd = command[0];
            var num = int.Parse(command.Substring(1, command.Length - 1));

            switch (cmd)
            {
                case 'N':
                    return ProcessNCommand(currentPos, num);
                    break;
                case 'S':
                    return ProcessSCommand(currentPos, num);
                    break;
                case 'E':
                    return ProcessECommand(currentPos, num);
                    break;
                case 'W':
                    return ProcessWCommand(currentPos, num);
                    break;
                case 'F':
                    return ProcessFCommand(currentPos, num);
                    break;
                case 'L':
                    return ProcessLCommand(currentPos, num);
                    break;
                case 'R':
                    return ProcessRCommand(currentPos, num);
                    break;
            }
            return null;
        }

        public static Day12Pos ProcessNCommand(Day12Pos currentPos, int num)
        {
            return new Day12Pos
            {
                Direction = currentPos.Direction,
                X = currentPos.X,
                Y = currentPos.Y - num
            };
        }

        public static Day12Pos ProcessSCommand(Day12Pos currentPos, int num)
        {
            return new Day12Pos
            {
                Direction = currentPos.Direction,
                X = currentPos.X,
                Y = currentPos.Y + num
            };
        }
        public static Day12Pos ProcessECommand(Day12Pos currentPos, int num)
        {
            return new Day12Pos
            {
                Direction = currentPos.Direction,
                X = currentPos.X + num,
                Y = currentPos.Y
            };
        }
        public static Day12Pos ProcessWCommand(Day12Pos currentPos, int num)
        {
            return new Day12Pos
            {
                Direction = currentPos.Direction,
                X = currentPos.X - num,
                Y = currentPos.Y
            };
        }
        public static Day12Pos ProcessFCommand(Day12Pos currentPos, int num)
        {
            switch (currentPos.Direction)
            {
                case 0:
                    return ProcessNCommand(currentPos, num);
                case 180:
                    return ProcessSCommand(currentPos, num);
                case 90:
                    return ProcessECommand(currentPos, num);
                case 270:
                    return ProcessWCommand(currentPos, num);
            }
            return null;
        }

        public static Day12Pos ProcessLCommand(Day12Pos currentPos, int num)
        {
            int newDir = (currentPos.Direction - num) % 360;
            if (newDir < 0)
            {
                switch (newDir)
                {
                    case -90:
                        newDir = 270;
                        break;
                    case -180:
                        newDir = 180;
                        break;
                    case -270:
                        newDir = 90;
                        break;
                }
            }
            return new Day12Pos
            {
                Direction = newDir,
                X = currentPos.X,
                Y = currentPos.Y
            };
        }

        public static Day12Pos ProcessRCommand(Day12Pos currentPos, int num)
        {
            int newDir = (currentPos.Direction + num) % 360;
            return new Day12Pos
            {
                Direction = newDir,
                X = currentPos.X,
                Y = currentPos.Y
            };
        }
    }
}
