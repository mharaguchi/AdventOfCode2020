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
            //input = _input;
            ParseTiles(input);

            var corners = new List<Day20Tile>();
            //Find 4 tiles that have adjacent sides that don't match up with any other tile
            foreach(var tile in _tiles)
            {
                if (IsPossibleCorner(tile))
                {
                    corners.Add(tile);
                }
            }

            //long product = (long)upperLeft * (long)upperRight * (long)downLeft * (long)downRight;
            long product = (long)corners[0].TileNum * (long)corners[1].TileNum;
            product *= corners[2].TileNum;
            product *= corners[3].TileNum;
            return product;
        }


        public static bool IsPossibleCorner(Day20Tile tile)
        {
            var tileCopy = new Day20Tile(tile);
            var otherTiles = _tiles.Where(x => x.TileNum != tile.TileNum);

            for(int i = 0; i < 4; i++)
            {
                if (HasCornerMatch(tileCopy, otherTiles))
                {
                    return true;
                }
                tileCopy = RotateTileClockwise(tileCopy);
            }
            tileCopy = FlipTileHorizontal(tileCopy);
            for (int i = 0; i < 4; i++)
            {
                if (HasCornerMatch(tileCopy, otherTiles))
                {
                    return true;
                }
                tileCopy = RotateTileClockwise(tileCopy);
            }

            //for each flip/rotation combo in the first tile
            //compare against each flip/rotation combo in the second tile against the top and the left of the first tile
            return false;
        }

        public static bool HasCornerMatch(Day20Tile tile, IEnumerable<Day20Tile> otherTiles)
        {
            var topMatches = otherTiles.Where(x => tile.Top.SequenceEqual(x.Top) || tile.Top.SequenceEqual(x.Bottom) || tile.Top.SequenceEqual(x.Left) ||
                tile.Top.SequenceEqual(x.Right) || 
                tile.Top.SequenceEqual(((IEnumerable<bool>)x.Top).Reverse()) ||
                tile.Top.SequenceEqual(((IEnumerable<bool>)x.Right).Reverse()) ||
                tile.Top.SequenceEqual(((IEnumerable<bool>)x.Bottom).Reverse()) ||
                tile.Top.SequenceEqual(((IEnumerable<bool>)x.Left).Reverse())
                );
            var leftMatches = otherTiles.Where(x => tile.Left.SequenceEqual(x.Top) || tile.Left.SequenceEqual(x.Bottom) || tile.Left.SequenceEqual(x.Left) ||
                tile.Left.SequenceEqual(x.Right) ||
                tile.Left.SequenceEqual(((IEnumerable<bool>)x.Top).Reverse()) ||
                tile.Left.SequenceEqual(((IEnumerable<bool>)x.Right).Reverse()) ||
                tile.Left.SequenceEqual(((IEnumerable<bool>)x.Bottom).Reverse()) ||
                tile.Left.SequenceEqual(((IEnumerable<bool>)x.Left).Reverse())
                );
            var rightMatches = otherTiles.Where(x => tile.Right.SequenceEqual(x.Top) || tile.Right.SequenceEqual(x.Bottom) || tile.Right.SequenceEqual(x.Right) ||
    tile.Right.SequenceEqual(x.Left) ||
    tile.Right.SequenceEqual(((IEnumerable<bool>)x.Top).Reverse()) ||
    tile.Right.SequenceEqual(((IEnumerable<bool>)x.Right).Reverse()) ||
    tile.Right.SequenceEqual(((IEnumerable<bool>)x.Bottom).Reverse()) ||
    tile.Right.SequenceEqual(((IEnumerable<bool>)x.Left).Reverse())
    );
            var bottomMatches = otherTiles.Where(x => tile.Bottom.SequenceEqual(x.Top) || tile.Bottom.SequenceEqual(x.Bottom) || tile.Bottom.SequenceEqual(x.Right) ||
tile.Bottom.SequenceEqual(x.Left) ||
tile.Bottom.SequenceEqual(((IEnumerable<bool>)x.Top).Reverse()) ||
tile.Bottom.SequenceEqual(((IEnumerable<bool>)x.Right).Reverse()) ||
tile.Bottom.SequenceEqual(((IEnumerable<bool>)x.Bottom).Reverse()) ||
tile.Bottom.SequenceEqual(((IEnumerable<bool>)x.Left).Reverse())
);
            if (!topMatches.Any() && !leftMatches.Any() && rightMatches.Any() && bottomMatches.Any())
            {
                return true;
            }
            return false;
        }

        public static Day20Tile RotateTileClockwise(Day20Tile tile)
        {
            var newTile = new Day20Tile();
            newTile.TileNum = tile.TileNum;
            newTile.Top = new List<bool>(((IEnumerable<bool>)tile.Left).Reverse());
            newTile.Right = new List<bool>(tile.Top);
            newTile.Bottom = new List<bool>(((IEnumerable<bool>)tile.Right).Reverse());
            newTile.Left = new List<bool>(tile.Bottom);
            return newTile;
        }

        public static Day20Tile FlipTileHorizontal(Day20Tile tile)
        {
            var newTile = new Day20Tile();
            newTile.TileNum = tile.TileNum;
            newTile.Top = new List<bool>(((IEnumerable<bool>)tile.Top).Reverse());
            newTile.Right = new List<bool>(tile.Left);
            newTile.Bottom = new List<bool>(((IEnumerable<bool>)tile.Bottom).Reverse());
            newTile.Left = new List<bool>(tile.Right);
            return newTile;
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
                if (tileToken.Length == 0)
                {
                    continue;
                }
                var newTile = new Day20Tile();
                var lines = InputUtils.SplitLinesIntoStringArray(tileToken);
                newTile.TileNum = int.Parse(StringUtils.SplitInOrder(lines[0], new string[] {"Tile ", ":"})[0]);
                for (int i = 0; i < lines[1].Length; i++)
                {
                    newTile.Top.Add(lines[1][i] == '#');
                    newTile.Bottom.Add(lines[lines.Length - 1][i] == '#');
                    newTile.Left.Add(lines[i+1][0] == '#');
                    newTile.Right.Add(lines[i+1][lines[1].Length - 1] == '#');
                }
                _tiles.Add(newTile);
            }
        }

        public static void PrintTiles()
        {
            foreach (var tile in _tiles)
            {
                Console.WriteLine($"Tile {tile.TileNum}: ");
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (i == 0 && j == 0)
                        {
                            if (tile.Top[0] != tile.Left[0])
                            {
                                Console.Write("X");
                            }
                            else
                            {
                                if (tile.Top[0])
                                {
                                    Console.Write("#");
                                }
                            }
                        }
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}
