using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day22
    {
        public static string _input = @"Player 1:
9
2
6
3
1

Player 2:
5
8
4
7
10";

        public static Queue<int> _player1Deck = new Queue<int>();
        public static Queue<int> _player2Deck = new Queue<int>();

        public static long Run(string input)
        {
            //input = _input;
            ParseInput(input);
            while (_player1Deck.Count > 0 && _player2Deck.Count > 0)
            {
                PlayRound();
            }
            var winningScore = CalculateScore();
            return winningScore;
        }

        public static void ParseInput(string input)
        {
            var tokens = InputUtils.SplitInputByBlankLines(input);
            
            var player1lines = InputUtils.SplitLinesIntoStringArray(tokens[0]);
            var player1cards = new string[player1lines.Length - 1];
            Array.Copy(player1lines, 1, player1cards, 0, player1lines.Length - 1);
            var player1cardsList = StringUtils.StringArrayToIntList(player1cards);
            _player1Deck = new Queue<int>(player1cardsList);

            var player2lines = InputUtils.SplitLinesIntoStringArray(tokens[1]);
            var player2cards = new string[player2lines.Length - 1];
            Array.Copy(player2lines, 1, player2cards, 0, player2lines.Length - 1);
            var player2cardsList = StringUtils.StringArrayToIntList(player2cards);
            _player2Deck = new Queue<int>(player2cardsList);
        }

        public static void PlayRound()
        {
            var player1Card = _player1Deck.Dequeue();
            var player2Card = _player2Deck.Dequeue();
            if (player1Card > player2Card)
            {
                _player1Deck.Enqueue(player1Card);
                _player1Deck.Enqueue(player2Card);
            }
            else
            {
                _player2Deck.Enqueue(player2Card);
                _player2Deck.Enqueue(player1Card);
            }
        }

        public static long CalculateScore()
        {
            var score = 0;
            var winningQueue = _player1Deck.Count > 0 ? _player1Deck : _player2Deck;
            for (int i = winningQueue.Count; i > 0; i--)
            {
                var thisCard = winningQueue.Dequeue();
                score += thisCard * i;
            }
            return score;
        }
    }
}
