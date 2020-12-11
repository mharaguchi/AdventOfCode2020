using AdventOfCode2020.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day8
    {

        public static string _input = @"nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6";

        public static int _accumulator = 0;
        public static int _index = 0;

        public static int Run(string input)
        {
            var commands = new List<Day8Command>();
            //input = _input;
            var lines = InputUtils.SplitLinesIntoStringArray(input);
            foreach(var line in lines)
            {
                var tokens = line.Trim().Split(" ");
                var command = new Day8Command() { Command = tokens[0], Value = int.Parse(tokens[1]) };
                commands.Add(command);
            }

            for(int i = 0; i < commands.Count; i++)
            {
                var commandsCopy = commands.ConvertAll(command => new Day8Command() { Command = command.Command, Value = command.Value });
                if (commandsCopy[i].Command == "acc")
                {
                    continue;
                }
                else
                {
                    if (commandsCopy[i].Command == "nop")
                    {
                        commandsCopy[i].Command = "jmp";
                    }
                    else
                    {
                        commandsCopy[i].Command = "nop";
                    }
                }
                var result = new Day8Program().RunProgram(commandsCopy);
                if (result != -999999)
                {
                    return result;
                }
            }
            return -1;
        }
    }
}
