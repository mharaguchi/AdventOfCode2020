using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020.Models
{
    public class Day20Tile
    {
        public int TileNum { get; set; }
        public List<bool> Top { get; set; } = new List<bool>();
        public List<bool> Bottom { get; set; } = new List<bool>();
        public List<bool> Left { get; set; } = new List<bool>();
        public List<bool> Right { get; set; } = new List<bool>();
        public bool Used { get; set; } = false;

        public Day20Tile(Day20Tile src)
        {
            TileNum = src.TileNum;
            Top = new List<bool>(src.Top);
            Bottom = new List<bool>(src.Bottom);
            Left = new List<bool>(src.Left);
            Right = new List<bool>(src.Right);
            Used = src.Used;
        }

        public Day20Tile()
        {

        }
    }
}
