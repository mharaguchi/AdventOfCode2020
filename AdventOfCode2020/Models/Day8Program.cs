using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020.Models
{
    public class Day8Program
    {
        public int Accumulator { get; set; }
        private int Index { get; set; }

        public int RunProgram(List<Day8Command> commands)
        {
            this.Accumulator = 0;
            this.Index = 0;
            var usedCommands = new List<int>();
            while (!usedCommands.Contains(Index) && this.Index < commands.Count)
            {
                usedCommands.Add(this.Index);
                ProcessCommand(commands[this.Index]);
            }
            if (this.Index == commands.Count)
            {
                return Accumulator;
            }
            return -999999;
        }

        public void ProcessCommand(Day8Command command)
        {
            switch (command.Command)
            {
                case "nop":
                    this.Index += 1;
                    break;
                case "acc":
                    this.Index += 1;
                    this.Accumulator += command.Value;
                    break;
                case "jmp":
                    this.Index += command.Value;
                    break;
            }
        }
    }
}
