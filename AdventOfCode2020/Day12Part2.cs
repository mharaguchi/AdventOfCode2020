using AdventOfCode2020.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day12Part2
    {
        public static string _input = @"F10
N3
F7
R90
F11";

        public static int Run(string input)
        {
            //input = _input;
            var lines = InputUtils.SplitLinesIntoStringArray(input);
            Day12Pos startingPos = new Day12Pos()
            {
                Direction = 90,
                X = 0,
                Y = 0
            };
            Day12Pos waypointPos = new Day12Pos()
            {
                Direction = 90,
                X = 10,
                Y = -1
            };
            var boardState = new Day12BoardState()
            {
                Boat = startingPos,
                Waypoint = waypointPos
            };
            foreach (var line in lines)
            {
                boardState = ProcessCommand(boardState, line);
            }
            return Math.Abs(boardState.Boat.X) + Math.Abs(boardState.Boat.Y);
        }

        public static Day12BoardState ProcessCommand(Day12BoardState currentState, string command)
        {
            var cmd = command[0];
            var num = int.Parse(command.Substring(1, command.Length - 1));

            switch (cmd)
            {
                case 'N':
                    return ProcessNCommand(currentState, num);
                    break;
                case 'S':
                    return ProcessSCommand(currentState, num);
                    break;
                case 'E':
                    return ProcessECommand(currentState, num);
                    break;
                case 'W':
                    return ProcessWCommand(currentState, num);
                    break;
                case 'F':
                    return ProcessFCommand(currentState, num);
                    break;
                case 'L':
                    return ProcessLCommand(currentState, num);
                    break;
                case 'R':
                    return ProcessRCommand(currentState, num);
                    break;
            }
            return null;
        }

        public static Day12BoardState ProcessNCommand(Day12BoardState currentState, int num)
        {
            var currentPos = currentState.Waypoint;
            currentState.Waypoint = new Day12Pos
            {
                Direction = 0,
                X = currentPos.X,
                Y = currentPos.Y - num
            };
            return currentState;
        }

        public static Day12BoardState ProcessSCommand(Day12BoardState currentState, int num)
        {
            var currentPos = currentState.Waypoint;
            currentState.Waypoint = new Day12Pos
            {
                Direction = currentPos.Direction,
                X = currentPos.X,
                Y = currentPos.Y + num
            };
            return currentState;
        }
        public static Day12BoardState ProcessECommand(Day12BoardState currentState, int num)
        {
            var currentPos = currentState.Waypoint;
            currentState.Waypoint = new Day12Pos
            {
                Direction = currentPos.Direction,
                X = currentPos.X + num,
                Y = currentPos.Y
            };
            return currentState;
        }
        public static Day12BoardState ProcessWCommand(Day12BoardState currentState, int num)
        {
            var currentPos = currentState.Waypoint;
            currentState.Waypoint = new Day12Pos
            {
                Direction = currentPos.Direction,
                X = currentPos.X - num,
                Y = currentPos.Y
            };
            return currentState;
        }


        public static Day12BoardState ProcessFCommand(Day12BoardState currentState, int num)
        {
            Day12Pos boatPos = currentState.Boat;
            Day12Pos waypointPos = currentState.Waypoint;

            int offsetX = waypointPos.X - boatPos.X;
            int offsetY = waypointPos.Y - boatPos.Y;

            currentState.Boat = new Day12Pos
            {
                X = boatPos.X + offsetX * num,
                Y = boatPos.Y + offsetY * num
            };
            currentState.Waypoint = new Day12Pos
            {
                X = currentState.Boat.X + offsetX,
                Y = currentState.Boat.Y + offsetY
            };
            return currentState;
        }

        public static Day12BoardState RotateL90(Day12BoardState currentState)
        {
            Day12Pos boatPos = currentState.Boat;
            Day12Pos waypointPos = currentState.Waypoint;

            int offsetX = waypointPos.X - boatPos.X;
            int offsetY = waypointPos.Y - boatPos.Y;

            int newOffsetX = offsetY;
            int newOffsetY = offsetX * -1;

            currentState.Waypoint = new Day12Pos
            {
                X = boatPos.X + newOffsetX,
                Y = boatPos.Y + newOffsetY
            };
            return currentState;
        }

        public static Day12BoardState RotateR90(Day12BoardState currentState)
        {
            Day12Pos boatPos = currentState.Boat;
            Day12Pos waypointPos = currentState.Waypoint;

            int offsetX = waypointPos.X - boatPos.X;
            int offsetY = waypointPos.Y - boatPos.Y;

            int newOffsetX = offsetY * -1;
            int newOffsetY = offsetX;

            currentState.Waypoint = new Day12Pos
            {
                X = boatPos.X + newOffsetX,
                Y = boatPos.Y + newOffsetY
            };
            return currentState;
        }


        public static Day12BoardState ProcessLCommand(Day12BoardState currentState, int num)
        {
            var currentWaypoint = currentState.Waypoint;
            switch (num)
            {
                case 90:
                    currentState = RotateL90(currentState);
                    break;
                case 180:
                    currentState = RotateL90(currentState);
                    currentState = RotateL90(currentState);
                    break;
                case 270:
                    currentState = RotateL90(currentState);
                    currentState = RotateL90(currentState);
                    currentState = RotateL90(currentState);
                    break;
            }
            return currentState;
        }

        public static Day12BoardState ProcessRCommand(Day12BoardState currentState, int num)
        {
            var currentWaypoint = currentState.Waypoint;
            switch (num)
            {
                case 90:
                    currentState = RotateR90(currentState);
                    break;
                case 180:
                    currentState = RotateR90(currentState);
                    currentState = RotateR90(currentState);
                    break;
                case 270:
                    currentState = RotateR90(currentState);
                    currentState = RotateR90(currentState);
                    currentState = RotateR90(currentState);
                    break;
            }
            return currentState;
        }
    }
}
