using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AdventOfCode2020
{
    public static class Day24Part2
    {
        public static string _input = @"sesenwnenenewseeswwswswwnenewsewsw
neeenesenwnwwswnenewnwwsewnenwseswesw
seswneswswsenwwnwse
nwnwneseeswswnenewneswwnewseswneseene
swweswneswnenwsewnwneneseenw
eesenwseswswnenwswnwnwsewwnwsene
sewnenenenesenwsewnenwwwse
wenwwweseeeweswwwnwwe
wsweesenenewnwwnwsenewsenwwsesesenwne
neeswseenwwswnwswswnw
nenwswwsewswnenenewsenwsenwnesesenew
enewnwewneswsewnwswenweswnenwsenwsw
sweneswneswneneenwnewenewwneswswnese
swwesenesewenwneswnwwneseswwne
enesenwswwswneneswsenwnewswseenwsese
wnwnesenesenenwwnenwsewesewsesesew
nenewswnwewswnenesenwnesewesw
eneswnwswnwsenenwnwnwwseeswneewsenese
neswnwewnwnwseenwseesewsenwsweewe
wseweeenwnesenwwwswnew";

        //public static Dictionary<Point, bool> _tiles = new Dictionary<Point, bool>();
        public static List<Point> _blackTiles = new List<Point>();

        public static int Run(string input)
        {
            //input = _input;
            var lines = InputUtils.SplitLinesIntoStringArray(input);
            SetInitialBoard(lines);

            for (int i = 0; i < 100; i++)
            {
                //PrintBoard();
                RunDay(i+1);
            }

            return _blackTiles.Count();
        }

        public static void PrintBoard()
        {
            Console.WriteLine("--- Board ---");
            var minX = _blackTiles.Min(x => x.X);
            var maxX = _blackTiles.Max(x => x.X);
            var minY = _blackTiles.Min(x => x.Y);
            var maxY = _blackTiles.Max(x => x.Y);
            for(int i = minY; i <= maxY; i += 5)
            {
                var offset = 0;
                if (i % 10 != 0)
                {
                    offset = 5;
                    Console.Write(" ");
                }
                for (int j = minX + offset; j <= maxX; j += 10)
                {
                    if (_blackTiles.Contains(new Point(j, i)))
                    {
                        if (j == 0 && i == 0)
                        {
                            Console.Write("X ");
                        }
                        else
                        {
                            Console.Write("B ");
                        }
                    }
                    else
                    {
                        if (j == 0 && i == 0)
                        {
                            Console.Write("O ");
                        }
                        else
                        {
                            Console.Write("W ");
                        }
                    }
                }
                Console.WriteLine();
            }
        }

        public static void RunDay(int dayNum)
        {
            var tilesToFlip = new List<Point>();

            //Calc black tile flips
            foreach(var tile in _blackTiles)
            {
                var blackTileNeighborCount = CalcBlackTileNeighbors(tile);
                if (blackTileNeighborCount == 0 || blackTileNeighborCount > 2)
                {
                    tilesToFlip.Add(tile);
                }
            }

            //Calc white tile flips
            var whiteTilesToFlip = FindWhiteTilesToFlip();
            tilesToFlip.AddRange(whiteTilesToFlip);

            //Flip tiles
            foreach(var tile in tilesToFlip)
            {
                if (_blackTiles.Contains(tile))
                {
                    _blackTiles.Remove(tile);
                }
                else
                {
                    _blackTiles.Add(tile);
                }
            }
            Console.WriteLine($"Day {dayNum}: {_blackTiles.Count}");
        }

        public static List<Point> FindWhiteTilesToFlip()
        {
            var tilesToFlip = new List<Point>();
            var whiteNeighborsOfBlackTiles = new Dictionary<Point, int>();
            foreach(var tile in _blackTiles)
            {
                var neighbors = GetNeighbors(tile);
                foreach(var neighbor in neighbors)
                {
                    if (!_blackTiles.Contains(neighbor))
                    {
                        if (whiteNeighborsOfBlackTiles.ContainsKey(neighbor))
                        {
                            whiteNeighborsOfBlackTiles[neighbor] += 1;
                        }
                        else
                        {
                            whiteNeighborsOfBlackTiles.Add(neighbor, 1);
                        }
                    }
                }
            }
            tilesToFlip = whiteNeighborsOfBlackTiles.Where(x => x.Value == 2).Select(x => x.Key).ToList();

            return tilesToFlip;
        }

        public static List<Point> GetNeighbors(Point tileCoordinates)
        {
            List<Point> neighbors = new List<Point>();
            neighbors.Add(new Point(tileCoordinates.X + 10, tileCoordinates.Y)); //e
            neighbors.Add(new Point(tileCoordinates.X - 10, tileCoordinates.Y)); //w
            neighbors.Add(new Point(tileCoordinates.X + 5, tileCoordinates.Y - 5)); //ne
            neighbors.Add(new Point(tileCoordinates.X - 5, tileCoordinates.Y - 5)); //nw
            neighbors.Add(new Point(tileCoordinates.X + 5, tileCoordinates.Y + 5)); //se
            neighbors.Add(new Point(tileCoordinates.X - 5, tileCoordinates.Y + 5)); //sw
            
            return neighbors;
        }

        public static int CalcBlackTileNeighbors(Point tileCoordinates)
        {
            var count = 0;
            var neighbors = GetNeighbors(tileCoordinates);

            foreach(var neighbor in neighbors)
            {
                if (_blackTiles.Contains(neighbor))
                {
                    count++;
                }
            }

            return count;
        }

        public static void SetInitialBoard(string[] lines)
        {
            foreach (var line in lines)
            {
                var directions = ParseLine(line);
                var tileCoordinates = GetCoordinates(directions);

                if (_blackTiles.Contains(tileCoordinates))
                {
                    _blackTiles.Remove(tileCoordinates);
                }
                else
                {
                    _blackTiles.Add(tileCoordinates);
                }
            }
        }

        public static List<string> ParseLine(string line)
        {
            var index = 0;
            var directions = new List<string>();
            while(index < line.Length)
            {
                switch (line[index])
                {
                    case 'e':
                    case 'w':
                        directions.Add(line[index].ToString());
                        index++;
                        break;
                    case 'n':
                    case 's':
                        directions.Add(line.Substring(index, 2));
                        index += 2;
                        break;
                }
            }
            return directions;
        }

        public static Point GetCoordinates(List<string> directions)
        {
            var loc = new Point(0, 0);
            foreach(var direction in directions)
            {
                switch (direction)
                {
                    case "e":
                        loc.X += 10;
                        break;
                    case "w":
                        loc.X -= 10;
                        break;
                    case "ne":
                        loc.X += 5;
                        loc.Y -= 5;
                        break;
                    case "nw":
                        loc.X -= 5;
                        loc.Y -= 5;
                        break;
                    case "se":
                        loc.X += 5;
                        loc.Y += 5;
                        break;
                    case "sw":
                        loc.X -= 5;
                        loc.Y += 5;
                        break;
                }
            }
            return loc;
        }
    }
}
