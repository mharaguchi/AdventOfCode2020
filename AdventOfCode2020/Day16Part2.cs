using AdventOfCode2020.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day16Part2
    {
        public static string _input = @"class: 0-1 or 4-19
row: 0-5 or 8-19
seat: 0-13 or 16-19

your ticket:
11,12,13

nearby tickets:
3,9,18
15,1,5
5,14,9";

        public static bool[] valid = new bool[999];
        public static Dictionary<string, List<int>> positions = new Dictionary<string, List<int>>();
        public static List<Day16Rule> rules = new List<Day16Rule>();

        public static long Run(string input)
        {
            //input = _input;
            var tokens = InputUtils.SplitInputByBlankLines(input);
            var myTicketTokens = InputUtils.SplitLinesIntoStringArray(tokens[1]);
            var myTicket = myTicketTokens[1];
            MarkValidAndSetRules(InputUtils.SplitLinesIntoStringArray(tokens[0]));
            var ticketLines = InputUtils.SplitLinesIntoStringArray(tokens[2]);
            var validTickets = GetValidTickets(ticketLines);
            SetPossiblePositions(validTickets);
            SetDefinitePositions();
            return GetProduct(myTicket);
        }

        public static long GetProduct(string myTicket)
        {
            var product = (long)1;
            var myTicketNums = InputUtils.SplitLineIntoIntList(myTicket, ",");
            var departureRules = rules.Where(x => x.Name.StartsWith("departure"));
            foreach(var departureRule in departureRules)
            {
                var pos = positions[departureRule.Name][0];
                var thisVal = myTicketNums[pos];
                product *= thisVal;
            }
            return product;
        }

        public static void SetDefinitePositions()
        {
            var notFinished = true;
            while (notFinished)
            {
                var confirmedPositions = positions.Where(x => x.Value.Count == 1);
                var confirmed = confirmedPositions.Select(x => x.Value[0]);
                var multiplePositions = positions.Where(x => x.Value.Count > 1);
                foreach(var thisConfirmed in confirmed)
                {
                    foreach(var thisRule in multiplePositions)
                    {
                        if (thisRule.Value.Contains(thisConfirmed))
                        {
                            thisRule.Value.Remove(thisConfirmed);
                        }
                    }
                }
                notFinished = positions.Where(x => x.Value.Count > 1).Any();
            }
        }

        public static void SetPossiblePositions(string[] validTickets)
        {
            foreach(var rule in rules)
            {
                var possiblePos = new List<int>();
                var nums = InputUtils.SplitLineIntoIntList(validTickets[0], ",");

                for (int i = 0; i < nums.Count; i++)
                {
                    if (IsPossiblePosition(rule, i, validTickets))
                    {
                        possiblePos.Add(i);
                    }
                }
                positions.Add(rule.Name, possiblePos);
            }
        }

        public static bool IsPossiblePosition(Day16Rule rule, int pos, string[] validTickets)
        {
            for (int i = 0; i < validTickets.Length; i++)
            {
                var nums = InputUtils.SplitLineIntoIntList(validTickets[i], ",");
                var n = nums[pos];
                if ((n < rule.Min1 || n > rule.Max1) && (n < rule.Min2 || n > rule.Max2))
                {
                    return false;
                }
            }
            return true;
        }

        public static void MarkValidAndSetRules(string[] rulesLines)
        {
            foreach(var ruleLine in rulesLines)
            {
                var tokens = StringUtils.SplitInOrder(ruleLine, new string[] { ": ", "-", " or ", "-" });
                var min1 = int.Parse(tokens[1]);
                var max1 = int.Parse(tokens[2]);
                var min2 = int.Parse(tokens[3]);
                var max2 = int.Parse(tokens[4]);
                for (int i = min1; i <= max1; i++)
                {
                    valid[i] = true;
                }
                for (int j = min2; j <= max2; j++)
                {
                    valid[j] = true;
                }
                rules.Add(new Day16Rule() { Name = tokens[0], Min1 = min1, Max1 = max1, Min2 = min2, Max2 = max2 });
            }
        }

        public static string[] GetValidTickets(string[] ticketLines)
        {
            long sum = (long)0;
            var validTickets = new List<string>();

            for (int i = 1; i < ticketLines.Length; i++) //skip "nearby tickets" line
            {
                var isValid = true;
                if (ticketLines[i].Length == 0)
                {
                    continue;
                }
                var nums = InputUtils.SplitLineIntoIntList(ticketLines[i], ",");
                foreach(var num in nums)
                {
                    if (!valid[num])
                    {
                        isValid = false;
                        sum += num;
                    }
                }
                if (isValid)
                {
                    validTickets.Add(ticketLines[i]);
                }
            }
            return validTickets.ToArray();
        }
    }
}
