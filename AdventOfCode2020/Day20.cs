using AdventOfCode2020.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public static class Day20
    {
        public static string _input = @"Tile 2311:
..##.#..#.
##..#.....
#...##..#.
####.#...#
##.##.###.
##...#.###
.#.#.#..##
..#....#..
###...#.#.
..###..###

Tile 1951:
#.##...##.
#.####...#
.....#..##
#...######
.##.#....#
.###.#####
###.##.##.
.###....#.
..#.#..#.#
#...##.#..

Tile 1171:
####...##.
#..##.#..#
##.#..#.#.
.###.####.
..###.####
.##....##.
.#...####.
#.##.####.
####..#...
.....##...

Tile 1427:
###.##.#..
.#..#.##..
.#.##.#..#
#.#.#.##.#
....#...##
...##..##.
...#.#####
.#.####.#.
..#..###.#
..##.#..#.

Tile 1489:
##.#.#....
..##...#..
.##..##...
..#...#...
#####...#.
#..#.#.#.#
...#.#.#..
##.#...##.
..##.##.##
###.##.#..

Tile 2473:
#....####.
#..#.##...
#.##..#...
######.#.#
.#...#.#.#
.#########
.###.#..#.
########.#
##...##.#.
..###.#.#.

Tile 2971:
..#.#....#
#...###...
#.#.###...
##.##..#..
.#####..##
.#..####.#
#..#.#..#.
..####.###
..#.#.###.
...#.#.#.#

Tile 2729:
...#.#.#.#
####.#....
..#.#.....
....#..#.#
.##..##.#.
.#.####...
####.#.#..
##.####...
##..#.##..
#.##...##.

Tile 3079:
#.#.#####.
.#..######
..#.......
######....
####.#..#.
.#...#.##.
#.#####.##
..#.###...
..#.......
..#.###...";

        public static List<Day20Tile> _tiles = new List<Day20Tile>();

        public static long Run(string input)
        {
            input = _input;
            ParseTiles(input);
            var upperLeft = FindULCorner();
            var upperRight = FindURCorner();
            var downLeft = FindDLCorner();
            var downRight = FindDRCorner();

            long product = (long)upperLeft * (long)upperRight * (long)downLeft * (long)downRight;
            return product;
        }

        public static int FindULCorner()
        {
            for (int i = 0; i < _tiles.Count; i++) //for each tile
            {
                var tile1 = _tiles[i];
                var match = false;
                for(int j = i+1; j < _tiles.Count; j++) //for each other tile
                {
                    var tile2 = _tiles[j];
                    if (IsTopOrLeftMatch(tile1, tile2))
                    {
                        match = true;
                        break;
                    }
                }
                if (!match)
                {
                    return tile1.TileNum;
                }
            }
            return -1;
        }

        public static int FindURCorner()
        {
            for (int i = 0; i < _tiles.Count; i++)
            {
                var tile1 = _tiles[i];
                var match = false;
                for (int j = i + 1; j < _tiles.Count; j++)
                {
                    var tile2 = _tiles[j];
                    if (IsTopOrRightMatch(tile1, tile2))
                    {
                        match = true;
                        break;
                    }
                }
                if (!match)
                {
                    return tile1.TileNum;
                }
            }
            return -1;
        }

        public static int FindDLCorner()
        {
            for (int i = 0; i < _tiles.Count; i++)
            {
                var tile1 = _tiles[i];
                var match = false;
                for (int j = i + 1; j < _tiles.Count; j++)
                {
                    var tile2 = _tiles[j];
                    if (IsBottomOrLeftMatch(tile1, tile2))
                    {
                        match = true;
                        break;
                    }
                }
                if (!match)
                {
                    return tile1.TileNum;
                }
            }
            return -1;
        }

        public static int FindDRCorner()
        {
            for (int i = 0; i < _tiles.Count; i++)
            {
                var tile1 = _tiles[i];
                var match = false;
                for (int j = i + 1; j < _tiles.Count; j++)
                {
                    var tile2 = _tiles[j];
                    if (IsBottomOrRightMatch(tile1, tile2))
                    {
                        match = true;
                        break;
                    }
                }
                if (!match)
                {
                    return tile1.TileNum;
                }
            }
            return -1;
        }

        public static bool IsTopOrLeftMatch(Day20Tile tile1, Day20Tile tile2)
        {
            if (tile1.Top.SequenceEqual(tile2.Bottom))
            {
                return true;
            }
            if (tile1.Left.SequenceEqual(tile2.Right))
            {
                return true;
            }
            return false;
        }

        public static bool IsTopOrRightMatch(Day20Tile tile1, Day20Tile tile2)
        {
            if (tile1.Top.SequenceEqual(tile2.Bottom))
            {
                return true;
            }
            if (tile1.Right.SequenceEqual(tile2.Left))
            {
                return true;
            }
            return false;
        }

        public static bool IsBottomOrLeftMatch(Day20Tile tile1, Day20Tile tile2)
        {
            if (tile1.Bottom.SequenceEqual(tile2.Top))
            {
                return true;
            }
            if (tile1.Left.SequenceEqual(tile2.Right))
            {
                return true;
            }
            return false;
        }

        public static bool IsBottomOrRightMatch(Day20Tile tile1, Day20Tile tile2)
        {
            if (tile1.Bottom.SequenceEqual(tile2.Top))
            {
                return true;
            }
            if (tile1.Right.SequenceEqual(tile2.Left))
            {
                return true;
            }
            return false;
        }

        public static void ParseTiles(string input)
        {
            var tileTokens = InputUtils.SplitInputByBlankLines(input);
            foreach(var tileToken in tileTokens)
            {
                var newTile = new Day20Tile();
                var lines = InputUtils.SplitLinesIntoStringArray(tileToken);
                newTile.TileNum = int.Parse(StringUtils.SplitInOrder(lines[0], new string[] {"Tile ", ":"})[0]);
                for (int i = 0; i < lines[1].Length; i++)
                {
                    newTile.Top.Add(lines[1][i] == '#');
                    newTile.Bottom.Add(lines[lines.Length - 1][i] == '#');
                    newTile.Left.Add(lines[i][0] == '#');
                    newTile.Right.Add(lines[i][lines[1].Length - 1] == '#');
                }
                _tiles.Add(newTile);
            }
        }
    }
}
