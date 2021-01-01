using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020.Models
{
    public class Day22Game
    {
        public List<Queue<int>> Player1Rounds { get; set; } = new List<Queue<int>>();
        public List<Queue<int>> Player2Rounds { get; set; } = new List<Queue<int>>();
        public List<int> Player1Hashes { get; set; } = new List<int>();
        public List<int> Player2Hashes { get; set; } = new List<int>();
        public Queue<int> Player1Deck { get; set; }
        public Queue<int> Player2Deck { get; set; }
        public int Winner { get; set; } = -1;
        public int GameNum { get; set; }
    }
}
