using AdventOfCode2020.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day22Part2
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
        public static string _input2 = @"Player 1:
43
19

Player 2:
2
29
14";

        public static Queue<int> _player1Deck = new Queue<int>();
        public static Queue<int> _player2Deck = new Queue<int>();
        public static int gameCounter = 1;

        public static long Run(string input)
        {
            //input = _input;
            ParseInput(input);
            var firstGame = new Day22Game();
            firstGame.GameNum = gameCounter;
            gameCounter++;
            firstGame.Player1Deck = new Queue<int>(_player1Deck);
            firstGame.Player2Deck = new Queue<int>(_player2Deck);
            Console.WriteLine("=== Game 1 ===");
            Console.WriteLine();
            
            PlayGame(firstGame);

            var winningScore = CalculateScore(firstGame);
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

        public static bool IsSameDeck(Queue<int> deck1, Queue<int> deck2)
        {
            var deck1Copy = new Queue<int>(deck1);
            var deck2Copy = new Queue<int>(deck2);
            if (deck1Copy.Count != deck2Copy.Count)
            {
                return false;
            }
            for (int i = 0; i < deck1.Count; i++)
            {
                var int1 = deck1Copy.Dequeue();
                var int2 = deck2Copy.Dequeue();
                if (int1 != int2)
                {
                    return false;
                }
            }
            return true;
        }

        public static void PlayGame(Day22Game game)
        {
            while (game.Player1Deck.Count > 0 && game.Player2Deck.Count > 0 && game.Winner == -1)
            {
                var player1Hash = GetSequenceHashCode(game.Player1Deck);
                var player2Hash = GetSequenceHashCode(game.Player2Deck);
                for (int i = 0; i < game.Player1Hashes.Count; i++)
                {
                    //if (game.Player1Hashes.Contains(player1Hash) && game.Player2Hashes.Contains(player2Hash))
                    //{
                        if (game.Player1Hashes[i] == player1Hash && game.Player2Hashes[i] == player2Hash)
                        //if (IsSameDeck(game.Player1Deck, game.Player1Rounds[i]) && IsSameDeck(game.Player2Deck, game.Player2Rounds[i]))
                        {
                            game.Winner = 1;
                            return;
                        }
                    //}
                }
                game.Player1Hashes.Add(player1Hash);
                game.Player2Hashes.Add(player2Hash);
                PlayRound(game);
            }
            game.Winner = game.Player1Deck.Count == 0 ? 2 : 1;
        }

        public static void PlayRound(Day22Game game)
        {
            var roundNum = game.Player1Hashes.Count;
            Console.WriteLine($"-- Round {roundNum} (Game {game.GameNum}) --");
            var roundWinner = -1;
            Console.WriteLine("Player 1's deck: " + string.Join(", ", game.Player1Deck));
            Console.WriteLine("Player 2's deck: " + string.Join(", ", game.Player2Deck));
            var player1Card = game.Player1Deck.Dequeue();
            var player2Card = game.Player2Deck.Dequeue();

            Console.WriteLine("Player 1 plays: " + player1Card);
            Console.WriteLine("Player 2 plays: " + player2Card);
            if (game.Player1Deck.Count >= player1Card && game.Player2Deck.Count >= player2Card)
            {
                Console.WriteLine("Playing a sub-game to determine the winner...");
                Console.WriteLine();
                Console.WriteLine($"=== Game {gameCounter} ===");
                Console.WriteLine();
                var newGame = new Day22Game();
                var newPlayer1Deck = new Queue<int>(game.Player1Deck.Take(player1Card));
                var newPlayer2Deck = new Queue<int>(game.Player2Deck.Take(player2Card));
                newGame.Player1Deck = newPlayer1Deck;
                newGame.Player2Deck = newPlayer2Deck;
                newGame.GameNum = gameCounter;
                gameCounter++;
                PlayGame(newGame);
                roundWinner = newGame.Winner;
            }
            else
            {
                roundWinner = player1Card > player2Card ? 1 : 2;
            }
            if (roundWinner == 1)
            {
                Console.WriteLine($"Player 1 wins round {roundNum} of game {game.GameNum}");
                game.Player1Deck.Enqueue(player1Card);
                game.Player1Deck.Enqueue(player2Card);
            }
            else
            {
                Console.WriteLine($"Player 2 wins round {roundNum} of game {game.GameNum}");
                game.Player2Deck.Enqueue(player2Card);
                game.Player2Deck.Enqueue(player1Card);
            }
            Console.WriteLine();
        }

        public static long CalculateScore(Day22Game game)
        {
            var score = 0;
            var winningQueue = game.Winner == 1 ? game.Player1Deck : game.Player2Deck;
            for (int i = winningQueue.Count; i > 0; i--)
            {
                var thisCard = winningQueue.Dequeue();
                score += thisCard * i;
            }
            return score;
        }

        public static int GetSequenceHashCode(Queue<int> sequence)
        {
            const int seed = 17;
            const int modifier = 3;

            unchecked
            {
                return sequence.Aggregate(seed, (current, item) => (current * modifier) + item.GetHashCode());
            }
        }
    }
}
