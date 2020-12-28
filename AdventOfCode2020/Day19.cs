using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day19
    {
        //        public static string _input = @"0: 1 2
        //1: ""a""
        //2: 1 3 | 3 1
        //3: ""b""";

        public static string _input2 = @"0: 4 1 5
1: 2 3 | 3 2
2: 4 4 | 5 5
3: 4 5 | 5 4
4: ""a""
5: ""b""";

        public static Dictionary<int, List<List<int>>> rulesDict = new Dictionary<int, List<List<int>>>();
        public static Dictionary<int, string> lettersDict = new Dictionary<int, string>();
        
        public static Dictionary<int, List<string>> resultDict = new Dictionary<int, List<string>>();
        
        public static List<string> validMessages = new List<string>();

        public static int Run(string input)
        {
            //input = _input2;
            //var matches = 0;
            var inputSections = InputUtils.SplitInputByBlankLines(input);
            PopulateRulesDict(inputSections[0]);

            var validMessages = new List<string>();
            var rulesList = new List<int>(rulesDict[0][0]);
            rulesList.Reverse();
            var initialStack = new Stack<int>(rulesList);
            validMessages.AddRange(GetPossibleMessages(new List<string> { "" }, initialStack));
            //var validMessages = GetPossibleMessages(new List<string> { "" }, 0);

            //CreateAllValidMessages(0);
            var messages = InputUtils.SplitLinesIntoStringArray(inputSections[1]);

            return messages.Where(x => validMessages.Contains(x)).Count();
        }

        public static List<string> GetPossibleMessages(List<string> messagesSoFar, Stack<int> rules)
        {
            var newMessages = new List<string>();
            if (rules.Count == 0) // No more rules
            {
                return messagesSoFar;
            }
            var ruleNum = rules.Pop();
            if (resultDict.ContainsKey(ruleNum)) //Dynamic dictionary has all possible combos for that rule already
            {
                newMessages = AddToMessages(messagesSoFar, resultDict[ruleNum]);
                return GetPossibleMessages(newMessages, rules);
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
                    newMessages.AddRange(GetPossibleMessages(messagesSoFar, newRulesStack));
                    newMessages = newMessages.Distinct().ToList();
                }
                //resultDict[ruleNum] = newMessages;
                return newMessages;
            }
        }

        public static List<string> AddToMessages(List<string> messagesSoFar, List<string> additions)
        {
            var messages = new List<string>();
            foreach (var messageSoFar in messagesSoFar)
            {
                foreach (var addition in additions)
                {
                    messages.Add(messageSoFar + addition);
                }
            }
            return messages;
        }

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
