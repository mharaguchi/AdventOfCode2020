using AdventOfCode2020.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AdventOfCode2020
{
    public static class Day20Part2
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

        public static List<Day20Part2Tile> _tiles = new List<Day20Part2Tile>();
        public static List<int> _usedTiles = new List<int>();
        public static int _sideLength;
        public static Dictionary<Point, Day20Part2Tile> _puzzle = new Dictionary<Point, Day20Part2Tile>();
        public static List<List<bool>> _image = new List<List<bool>>();

        public static long Run(string input)
        {
            //input = _input;
            ParseTiles(input);
            _sideLength = (int)Math.Sqrt(_tiles.Count);

            for(int i = 0; i < _sideLength; i++)
            {
                for(int j = 0; j < _sideLength; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        AddFirstCorner();
                        continue;
                    }
                    if (i == 0) //First row
                    {
                        AddNextFirstRowPiece(j);
                        continue;
                        //Find left match for right side of previous piece
                    }
                    if (j == 0) //First column
                    {
                        AddNextFirstColumnPiece(i);
                        //Find top match for bottom above only
                        continue;
                    }
                    AddNextNormalPiece(j, i);
                    //Find top and left match for placed puzzle pieces above and to the left
                }
            }
            ConstructImage();
            PrintImage();

            var totalPounds = GetTotalPounds();
            var numSeaMonsters = GetSeaMonsterCount();
            var coveredPounds = numSeaMonsters * 15;

            return totalPounds - coveredPounds;
        }

        public static int GetTotalPounds()
        {
            var count = 0;
            for(int i = 0; i < _image.Count; i++)
            {
                count += _image[i].Where(x => x == true).Count();
            }
            return count;
        }

        public static void PrintImage()
        {
            for(int i = 0; i < _image.Count; i++)
            {
                for(int j = 0; j < _image.Count; j++)
                {
                    if (_image[i][j])
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static int GetSeaMonsterCount()
        {
            var numSeaMonsters = CountSeaMonsters();
            if (numSeaMonsters > 0)
            {
                return numSeaMonsters;
            }
            for (int i = 0; i < 4; i++)
            {
                _image = RotateImageClockwise(_image);
                numSeaMonsters = CountSeaMonsters();
                if (numSeaMonsters > 0)
                {
                    return numSeaMonsters;
                }
            }
            _image = FlipImageHorizontal(_image);
            for (int i = 0; i < 4; i++)
            {
                _image = RotateImageClockwise(_image);
                numSeaMonsters = CountSeaMonsters();
                if (numSeaMonsters > 0)
                {
                    return numSeaMonsters;
                }
            }
            return -1;
        }

        public static int CountSeaMonsters()
        {
            var count = 0;
            for(int i = 2; i < _image.Count; i++)
            {
                for(int j = 0; j < _image[0].Count - 19; j++)
                {
                    if (IsSeaMonster(j, i))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public static bool IsSeaMonster(int colNum, int rowNum)
        {
            if (!_image[rowNum][colNum+1] || 
                !_image[rowNum][colNum + 4] || 
                !_image[rowNum][colNum + 7] || 
                !_image[rowNum][colNum + 10] || 
                !_image[rowNum][colNum + 13] || 
                !_image[rowNum][colNum + 16])
            {
                return false;
            }
            if (!_image[rowNum - 1][colNum] ||
                !_image[rowNum - 1][colNum+5] ||
                !_image[rowNum - 1][colNum+6] ||
                !_image[rowNum - 1][colNum+11] ||
                !_image[rowNum - 1][colNum+12] ||
                !_image[rowNum - 1][colNum+17] ||
                !_image[rowNum - 1][colNum+18] ||
                !_image[rowNum - 1][colNum+19]
                )
            {
                return false;
            }
            if (!_image[rowNum - 2][colNum+18])
            {
                return false;
            }
            return true;
        }

        public static void ConstructImage()
        {
            for(int i = 0; i < _sideLength * 8; i++)
            {
                _image.Add(GetImageRow(i));
            }
        }

        public static List<bool> GetImageRow(int rowNum)
        {
            var newRow = new List<bool>();
            for (int i = 0; i < _sideLength; i++)
            {
                newRow.AddRange(_puzzle[new Point(i, rowNum / 8)].Inside[rowNum % 8]);
            }
            return newRow;
        }

        public static void AddNextFirstRowPiece(int x)
        {
            var prevPieceX = _puzzle[new Point(x - 1, 0)];
            var thisPiece = FindLeftMatch(prevPieceX.Right);
            _puzzle.Add(new Point(x, 0), thisPiece);
            _usedTiles.Add(thisPiece.TileNum);
        }

        public static void AddNextFirstColumnPiece(int y)
        {
            var prevPieceY = _puzzle[new Point(0, y-1)];
            var thisPiece = FindTopMatch(prevPieceY.Bottom);
            _puzzle.Add(new Point(0, y), thisPiece);
            _usedTiles.Add(thisPiece.TileNum);
        }

        public static void AddNextNormalPiece(int x, int y)
        {
            var prevPieceX = _puzzle[new Point(x - 1, y)];
            var prevPieceY = _puzzle[new Point(x, y - 1)];
            var thisPiece = FindTopAndLeftMatch(prevPieceY.Bottom, prevPieceX.Right);
            _puzzle.Add(new Point(x, y), thisPiece);
            _usedTiles.Add(thisPiece.TileNum);
        }

        public static Day20Part2Tile FindLeftMatch(List<bool> left)
        {
            var remainingTiles = _tiles.Where(x => !_usedTiles.Contains(x.TileNum));
            foreach (var remainingTile in remainingTiles)
            {
                var tileCopy = new Day20Part2Tile(remainingTile);
                var otherTiles = remainingTiles.Where(x => x.TileNum != tileCopy.TileNum);

                for (int i = 0; i < 4; i++)
                {
                    if (tileCopy.Left.SequenceEqual(left))
                    {
                        return tileCopy;
                    }
                    tileCopy = RotateTileClockwise(tileCopy);
                }
                tileCopy = FlipTileHorizontal(tileCopy);
                for (int i = 0; i < 4; i++)
                {
                    if (tileCopy.Left.SequenceEqual(left))
                    {
                        return tileCopy;
                    }
                    tileCopy = RotateTileClockwise(tileCopy);
                }
            }
            return null;
        }

        public static Day20Part2Tile FindTopMatch(List<bool> top)
        {
            var remainingTiles = _tiles.Where(x => !_usedTiles.Contains(x.TileNum));
            foreach (var remainingTile in remainingTiles)
            {
                var tileCopy = new Day20Part2Tile(remainingTile);

                for (int i = 0; i < 4; i++)
                {
                    if (tileCopy.Top.SequenceEqual(top))
                    {
                        return tileCopy;
                    }
                    tileCopy = RotateTileClockwise(tileCopy);
                }
                tileCopy = FlipTileHorizontal(tileCopy);
                for (int i = 0; i < 4; i++)
                {
                    if (tileCopy.Top.SequenceEqual(top))
                    {
                        return tileCopy;
                    }
                    tileCopy = RotateTileClockwise(tileCopy);
                }
            }
            return null;
        }

        public static Day20Part2Tile FindTopAndLeftMatch(List<bool> top, List<bool> left)
        {
            var remainingTiles = _tiles.Where(x => !_usedTiles.Contains(x.TileNum));
            foreach (var remainingTile in remainingTiles)
            {
                var tileCopy = new Day20Part2Tile(remainingTile);

                for (int i = 0; i < 4; i++)
                {
                    if (tileCopy.Top.SequenceEqual(top) && tileCopy.Left.SequenceEqual(left))
                    {
                        return tileCopy;
                    }
                    tileCopy = RotateTileClockwise(tileCopy);
                }
                tileCopy = FlipTileHorizontal(tileCopy);
                for (int i = 0; i < 4; i++)
                {
                    if (tileCopy.Top.SequenceEqual(top) && tileCopy.Left.SequenceEqual(left))
                    {
                        return tileCopy;
                    }
                    tileCopy = RotateTileClockwise(tileCopy);
                }
            }
            return null;
        }

        public static void AddFirstCorner()
        {
            var firstCorner = GetFirstCorner();
            _puzzle.Add(new Point(0, 0), firstCorner); //First puzzle piece is the first corner
            _usedTiles.Add(firstCorner.TileNum);
        }

        public static Day20Part2Tile GetFirstCorner()
        {
            foreach (var tile in _tiles)
            {
                var tileCopy = GetCorner(tile);
                if (tileCopy != null)
                {
                    return tileCopy;
                }
            }
            return null;
        }

        public static Day20Part2Tile GetCorner(Day20Part2Tile tile)
        {
            var tileCopy = new Day20Part2Tile(tile);
            var otherTiles = _tiles.Where(x => x.TileNum != tile.TileNum);
            tileCopy = FlipTileHorizontal(tileCopy);

            for (int i = 0; i < 4; i++)
            {
                if (HasCornerMatch(tileCopy, otherTiles))
                {
                    return tileCopy;
                }
                tileCopy = RotateTileClockwise(tileCopy);
            }
            tileCopy = FlipTileHorizontal(tileCopy);
            for (int i = 0; i < 4; i++)
            {
                if (HasCornerMatch(tileCopy, otherTiles))
                {
                    return tileCopy;
                }
                tileCopy = RotateTileClockwise(tileCopy);
            }

            //for each flip/rotation combo in the first tile
            //compare against each flip/rotation combo in the second tile against the top and the left of the first tile
            return null;
        }

        public static bool HasCornerMatch(Day20Part2Tile tile, IEnumerable<Day20Part2Tile> otherTiles)
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

        public static Day20Part2Tile RotateTileClockwise(Day20Part2Tile tile)
        {
            var newTile = new Day20Part2Tile();
            newTile.TileNum = tile.TileNum;
            newTile.Top = new List<bool>(((IEnumerable<bool>)tile.Left).Reverse());
            newTile.Right = new List<bool>(tile.Top);
            newTile.Bottom = new List<bool>(((IEnumerable<bool>)tile.Right).Reverse());
            newTile.Left = new List<bool>(tile.Bottom);
            newTile.Inside = RotateInsideClockwise(tile.Inside);
            return newTile;
        }

        public static List<List<bool>> CreateImage(int rows)
        {
            var newInside = new List<List<bool>>();
            for (int i = 0; i <= rows; i++)
            {
                newInside.Add(new List<bool>());
                for(int j = 0; j <= rows; j++)
                {
                    newInside[i].Add(false);
                }
            }
            return newInside;
        }

        public static List<List<bool>> FlipImageHorizontal(List<List<bool>> srcImage)
        {
            var max = srcImage.Count - 1;
            var newImage = CreateImage(max);
            for (int i = 0; i <= max; i++)
            {
                for (int j = 0; j <= max; j++)
                {
                    newImage[i][max - j] = srcImage[i][j];
                }
            }
            return newImage;
        }

        public static List<List<bool>> RotateImageClockwise(List<List<bool>> srcImage)
        {
            var max = srcImage.Count - 1;
            var newImage = CreateImage(max);

            for (int i = 0; i <= max; i++)
            {
                for (int j = 0; j <= max; j++)
                {
                    newImage[max - j][i] = srcImage[i][j];
                }
            }
            return newImage;
        }

        public static List<List<bool>> CreateInside(int rows)
        {
            var newInside = new List<List<bool>>();
            for (int i = 0; i <= rows; i++)
            {
                newInside.Add(new List<bool>() { false, false, false, false, false, false, false, false });
            }
            return newInside;
        }

        public static List<List<bool>> RotateInsideClockwise(List<List<bool>> srcInside) 
        {
            var max = srcInside.Count - 1;
            var newInside = CreateInside(max);
            
            for (int i = 0; i <= max; i++)
            {
                for (int j = 0; j <= max; j++)
                {
                    newInside[i][max - j] = srcInside[j][i];
                }
            }
            return newInside;
        }

        public static Day20Part2Tile FlipTileHorizontal(Day20Part2Tile tile)
        {
            var newTile = new Day20Part2Tile();
            newTile.TileNum = tile.TileNum;
            newTile.Top = new List<bool>(((IEnumerable<bool>)tile.Top).Reverse());
            newTile.Right = new List<bool>(tile.Left);
            newTile.Bottom = new List<bool>(((IEnumerable<bool>)tile.Bottom).Reverse());
            newTile.Left = new List<bool>(tile.Right);
            newTile.Inside = FlipInsideHorizontal(tile.Inside);
            return newTile;
        }

        public static List<List<bool>> FlipInsideHorizontal(List<List<bool>> srcInside)
        {
            var max = srcInside.Count - 1;
            var newInside = CreateInside(max);
            for (int i = 0; i <= max; i++)
            {
                for (int j = 0; j <= max; j++)
                {
                    newInside[i][max-j] = srcInside[i][j];
                }
            }
            return newInside;
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
                var newTile = new Day20Part2Tile();
                var lines = InputUtils.SplitLinesIntoStringArray(tileToken);
                newTile.TileNum = int.Parse(StringUtils.SplitInOrder(lines[0], new string[] {"Tile ", ":"})[0]);
                for (int i = 0; i < lines[1].Length; i++)
                {
                    newTile.Top.Add(lines[1][i] == '#');
                    newTile.Bottom.Add(lines[lines.Length - 1][i] == '#');
                    newTile.Left.Add(lines[i+1][0] == '#');
                    newTile.Right.Add(lines[i+1][lines[1].Length - 1] == '#');
                }
                for (int i = 2; i < lines.Length - 1; i++)
                {
                    for (int j = 1; j < lines[1].Length - 1; j++)
                    {
                        newTile.Inside[i - 2][j-1] = lines[i][j] == '#';
                    }
                }
                _tiles.Add(newTile);
            }
        }
    }
}
