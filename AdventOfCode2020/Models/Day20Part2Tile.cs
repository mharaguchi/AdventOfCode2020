using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020.Models
{
    public class Day20Part2Tile
    {
        public int TileNum { get; set; }
        public List<bool> Top { get; set; } = new List<bool>();
        public List<bool> Bottom { get; set; } = new List<bool>();
        public List<bool> Left { get; set; } = new List<bool>();
        public List<bool> Right { get; set; } = new List<bool>();
        public List<List<bool>> Inside { get; set; }

        public Day20Part2Tile(Day20Part2Tile src)
        {
            InitializeInside();
            TileNum = src.TileNum;
            Top = new List<bool>(src.Top);
            Bottom = new List<bool>(src.Bottom);
            Left = new List<bool>(src.Left);
            Right = new List<bool>(src.Right);
            for(int i = 0; i < src.Inside.Count; i++)
            {
                Inside[i] = new List<bool>(src.Inside[i]);
            }
        }

        public Day20Part2Tile()
        {
            InitializeInside();
        }

        public void InitializeInside()
        {
            Inside = new List<List<bool>>();
            for (int i = 0; i < 8; i++)
            {
                Inside.Add(new List<bool>() { false, false, false, false, false, false, false, false });
            }
        }
    }
}
