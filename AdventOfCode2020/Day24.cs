using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day24
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

        public static Dictionary<Point, int> _tiles = new Dictionary<Point, int>();

        public static int Run(string input)
        {
            //input = _input;
            var lines = InputUtils.SplitLinesIntoStringArray(input);

            foreach(var line in lines)
            {
                var directions = ParseLine(line);
                var tileCoordinates = GetCoordinates(directions);
                if (_tiles.ContainsKey(tileCoordinates))
                {
                    _tiles[tileCoordinates] = _tiles[tileCoordinates] + 1;
                }
                else
                {
                    _tiles.Add(tileCoordinates, 1);
                }
            }

            return _tiles.Where(x => x.Value % 2 == 1).Count();
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
