using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public class Day24
    {
        public static long Solution1(string fileName = @".\..\..\..\Day24\Test1.txt")
        {
            var instructions = ReadInput(fileName);

            var map = new HexTileMap(instructions.Max(x => x.Directions.Count)*2);
            foreach (var i in instructions)
            {
                map.NavigateToTileAndFlip(i);
            }
            return map.CountFlipped() ;
        }

        public static long Solution2(string fileName = @".\..\..\..\Day21\Input.txt")
        {
            return 0;
        }

        public static List<TileInstruction> ReadInput(string fileName)
        {
            List<TileInstruction> tileInstructions = new List<TileInstruction>();
            using StreamReader inputFile = new StreamReader(fileName);
            string line;
            while (!string.IsNullOrEmpty(line = inputFile.ReadLine()))
            {
                tileInstructions.Add(new TileInstruction(line));
            }
            return tileInstructions;
        }

    }

    public class TileInstruction
    {
        public List<HexDirection> Directions { get; private set; }
        public TileInstruction(string input)
        {
            Directions = new List<HexDirection>();
            for (int i = 0; i < input.Length; i++)
            {
                var s = input[i];
                string hexDirectionString;
                if (s == 's' || s == 'n')
                {
                    hexDirectionString = (s.ToString() + input[i + 1].ToString());
                    i++;
                }
                else
                {
                    hexDirectionString = s.ToString();
                }
                var hexDir = (HexDirection)Enum.Parse(typeof(HexDirection), hexDirectionString, true);
                Directions.Add(hexDir);
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            Directions.ForEach(x => sb.Append($"{x} "));
            return sb.ToString();
        }
    }

    public enum HexDirection
    {
        E,
        SE,
        SW,
        W,
        NW,
        NE,
    }

    public class HexTile
    {
        public bool Color { get; private set; }

        public HexTile()
        {
            Color = false;
        }

        public void Flip()
        {
            Color = !Color;
        }
    }

    public class HexTileMap
    {
        public static Dictionary<HexDirection, Point> axialCoordinateByHexDirection = new Dictionary<HexDirection, Point>() {
           { HexDirection.E,  new Point(+1, 0) },
           { HexDirection.NE, new Point(+1, -1) },
           { HexDirection.NW, new Point(0, -1) },
           { HexDirection.W,  new Point(-1, 0) },
           { HexDirection.SW, new Point(-1, +1) },
           { HexDirection.SE, new Point(0, +1) }
        };

        public HexTile[,] HexTiles;

        public HexTile Center;
        public int MapSize;
        public Point CenterCoordinate;
        public HexTileMap(int size)
        {
            HexTiles = new HexTile[size, size];
            Center = new HexTile();
            CenterCoordinate = new Point(size / 2, size / 2);
            HexTiles[CenterCoordinate.X, CenterCoordinate.Y] = Center;
            MapSize = size;
        }

        public long CountFlipped()
        {
            long total = 0;
            for (int i = 0; i < MapSize; i++)
            {
                for (int j = 0; j < MapSize; j++)
                {
                    var t = HexTiles[i, j];
                    if (t != null && t.Color)
                    {
                        total++;
                    }
                }
            }
            return total;
        }

        public void NavigateToTileAndFlip(TileInstruction tileInstruction)
        {
            GoToTile(tileInstruction).Flip();
        }

        public HexTile GoToTile(TileInstruction tileInstruction)
        {
            var destinationCoordinate = CenterCoordinate;

            
            foreach (var hexDirection in tileInstruction.Directions)
            {
                var p = axialCoordinateByHexDirection[hexDirection];
                destinationCoordinate.X += p.X;
                destinationCoordinate.Y += p.Y;
            }

            if(HexTiles[destinationCoordinate.X, destinationCoordinate.Y] == null)
            {
                HexTiles[destinationCoordinate.X, destinationCoordinate.Y] = new HexTile();
            }

            return HexTiles[destinationCoordinate.X, destinationCoordinate.Y];
        }

    }
}
