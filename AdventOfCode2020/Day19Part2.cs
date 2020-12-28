using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day19Part2
    {
                public static string _input = @"42: 9 14 | 10 1
9: 14 27 | 1 26
10: 23 14 | 28 1
1: ""a""
11: 42 31
5: 1 14 | 15 1
19: 14 1 | 14 14
12: 24 14 | 19 1
16: 15 1 | 14 14
31: 14 17 | 1 13
6: 14 14 | 1 14
2: 1 24 | 14 4
0: 8 11
13: 14 3 | 1 12
15: 1 | 14
17: 14 2 | 1 7
23: 25 1 | 22 14
28: 16 1
4: 1 1
20: 14 14 | 1 15
3: 5 14 | 16 1
27: 1 6 | 14 18
14: ""b""
21: 14 1 | 1 14
25: 1 1 | 1 14
22: 14 14
8: 42
26: 14 22 | 1 20
18: 15 15
7: 14 5 | 1 21
24: 14 1

abbbbbabbbaaaababbaabbbbabababbbabbbbbbabaaaa
bbabbbbaabaabba
babbbbaabbbbbabbbbbbaabaaabaaa
aaabbbbbbaaaabaababaabababbabaaabbababababaaa
bbbbbbbaaaabbbbaaabbabaaa
bbbababbbbaaaaaaaabbababaaababaabab
ababaaaaaabaaab
ababaaaaabbbaba
baabbaaaabbaaaababbaababb
abbbbabbbbaaaababbbbbbaaaababb
aaaaabbaabaaaaababaa
aaaabbaaaabbaaa
aaaabbaabbaaaaaaabbbabbbaaabbaabaaa
babaaabbbaaabaababbaabababaaab
aabbbbbaabbbaaaaaabbbbbababaaaaabbaaabba";


        public static Dictionary<int, List<List<int>>> rulesDict = new Dictionary<int, List<List<int>>>();
        public static Dictionary<int, string> lettersDict = new Dictionary<int, string>();
        
        public static Dictionary<int, List<string>> resultDict = new Dictionary<int, List<string>>();
        
        public static List<string> validMessages = new List<string>();

        private static int aRule;
        private static int bRule;

        public static int Run(string input)
        {
            aRule = 110; //rule number of the "a" rule
            bRule = 92; //rule number of the "b" rule

            //aRule = 1; //rule number of the "a" rule
            //bRule = 14; //rule number of the "b" rule
            //input = _input; //switch input from input file to local variable

            var inputSections = InputUtils.SplitInputByBlankLines(input);
            PopulateRulesDict(inputSections[0]);

            var validMessages = new List<string>();
            var messages = InputUtils.SplitLinesIntoStringArray(inputSections[1]);

            foreach (var message in messages)
            {
                var rulesList = new List<int>(rulesDict[0][0]);
                rulesList.Reverse();
                var initialStack = new Stack<int>(rulesList);

                if (IsValidMessage(message, initialStack))
                {
                    validMessages.Add(message);
                }
            }

            return validMessages.Count();
        }

        public static bool IsValidMessage(string remainingMessage, Stack<int> rules)
        {
            var newMessages = new List<string>();
            if (remainingMessage.Length == 0 && rules.Count > 0)
            {
                return false;
            }
            if (rules.Count == 0) // No more rules
            {
                if (remainingMessage.Length == 0)
                {
                    return true;
                }
                return false;
            }
            var ruleNum = rules.Pop();
            if (ruleNum == aRule || ruleNum == bRule) //
            {
                var thisChar = ruleNum == aRule ? 'a' : 'b';
                if (thisChar == remainingMessage[0])
                {
                    var newRemainingMessage = remainingMessage[1..];
                    var passedRuleSet = new Stack<int>(rules);
                    var reversePassedRuleSet = new Stack<int>(passedRuleSet);
                    return IsValidMessage(newRemainingMessage, reversePassedRuleSet);
                }
                return false;
            }
            else //New rule
            {
                var newRuleSets = rulesDict[ruleNum];
                foreach (var newRuleSet in newRuleSets)
                {
                    var thisRuleSet = new List<int>(newRuleSet);
                    thisRuleSet.Reverse();
                    var passedRuleSet = new Stack<int>(rules);
                    var newRulesStack = new Stack<int>(passedRuleSet);
                    for (int i = 0; i < thisRuleSet.Count; i++)
                    {
                        newRulesStack.Push(thisRuleSet[i]);
                    }
                    if (IsValidMessage(remainingMessage, newRulesStack))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        //public static List<string> AddToMessages(List<string> messagesSoFar, List<string> additions)
        //{
        //    var messages = new List<string>();
        //    foreach (var messageSoFar in messagesSoFar)
        //    {
        //        foreach (var addition in additions)
        //        {
        //            messages.Add(messageSoFar + addition);
        //        }
        //    }
        //    return messages;
        //}

        //public static List<string> GetPossibleMessages(List<string> messagesSoFar, int ruleNum)
        //{
        //    var theseValidMessages = new List<string>();
        //    if (resultDict.ContainsKey(ruleNum))
        //    {
        //        foreach (var endString in resultDict[ruleNum])
        //        {
        //            foreach (var messageSoFar in messagesSoFar)
        //            {
        //                theseValidMessages.Add(messageSoFar + endString);
        //            }
        //        }
        //        return theseValidMessages;
        //    }
        //    var ruleLists = rulesDict[ruleNum];
        //    foreach(var ruleList in ruleLists)
        //    {
        //        foreach(var rule in ruleList)
        //        {
        //            var ends = GetPossibleMessages(messagesSoFar, rule);
        //            foreach (var end in ends)
        //            {
        //                for(int i = 0; i < messagesSoFar.Count; i++)
        //                {
        //                    var newMessageSoFar = messagesSoFar[i];
        //                    newMessageSoFar += end;
        //                    theseValidMessages.Add(newMessageSoFar);
        //                }
        //            }
        //        }
        //    }
        //    resultDict.Add(ruleNum, theseValidMessages);
        //    return theseValidMessages;
        //}

        //public static void CreateAllValidMessages(int ruleNum)
        //{
        //    foreach(var ruleList in rulesDict[ruleNum])
        //    {
        //        string thisMessage = "";
        //        foreach (var rule in ruleList) {
        //            thisMessage += GetMessage("", rule);
        //        }
        //        if (!validMessages.Contains(thisMessage))
        //        {
        //            validMessages.Add(thisMessage);
        //        }
        //    }
        //}

        //public static string GetMessage(string messageSoFar, int ruleNum)
        //{
        //    if (messageSoFar)
        //}

        //public static bool IsMatch(string message, int ruleNum)
        //{
        //    var rulesList = rulesDict[ruleNum];
        //    foreach(var ruleList in rulesList)
        //    {
        //        if (IsMatch(message, ruleList))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        //public static bool IsMatch(string message, List<int> ruleList)
        //{
        //    if (ruleList.Count == 0)
        //    {
        //        if (message.Length == 0)
        //        {
        //            return true;
        //        }
        //    }
        //    else 
        //    {
        //        for (int i = 0; i < ruleList.Count; i++)
        //        {
        //            var thisRule = ruleList[0];
        //            if (rulesDict[thisRule].Count == 0)
        //            {
        //                var letterToMatch = lettersDict[thisRule];
        //                if (message[0] == letterToMatch[0])
        //                {
        //                    message = message.Substring(1);
        //                    //continue checking
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            }
        //            //return false;
        //        }
        //        if ()
        //    }
        //}

        public static void PopulateRulesDict(string input)
        {
            var lines = InputUtils.SplitLinesIntoStringArray(input);
            foreach(var line in lines)
            {
                var tokens = StringUtils.SplitInOrder(line, new string[] { ": " });
                var ruleNum = int.Parse(tokens[0]);
                if (tokens[1].StartsWith("\"a\""))
                {
                    rulesDict.Add(ruleNum, new List<List<int>>());
                    resultDict.Add(ruleNum, new List<string> { "a" });
                    continue;
                }
                if (tokens[1].StartsWith("\"b\""))
                {
                    rulesDict.Add(ruleNum, new List<List<int>>());
                    resultDict.Add(ruleNum, new List<string> { "b" });
                    continue;
                }
                var ruleListTokens = tokens[1].Split( " | " );
                var ruleSets = new List<List<int>>();
                foreach(var ruleToken in ruleListTokens)
                {
                    var ruleNums = InputUtils.SplitLineIntoIntList(ruleToken, " ");
                    ruleSets.Add(ruleNums);
                }
                rulesDict.Add(ruleNum, ruleSets);
            }
        }
    }
}
