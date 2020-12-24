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

            var map = new HexTileMap(instructions.Max(x => x.Directions.Count) * 2);
            map.InitBlackTiles(instructions);
            return map.CountFlipped();
        }

        public static long Solution2(string fileName = @".\..\..\..\Day24\Input.txt")
        {
            var instructions = ReadInput(fileName);
            var map = new HexTileMap(instructions.Max(x => x.Directions.Count) * 2 * 5);
            map.SetAllToWhite();
            map.InitBlackTiles(instructions);
            map.ChangeTiles(100);
            return map.CountFlipped();
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
        public bool? NextRoundColor { get; set; }

        public HexTile()
        {
            Color = false;
            NextRoundColor = null;
        }

        public void Flip()
        {
            Color = !Color;
        }

        public void ApplyNextRoundColor()
        {
            if (NextRoundColor.HasValue)
            {
                Color = NextRoundColor.Value;
                NextRoundColor = null;
            }
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

            if (HexTiles[destinationCoordinate.X, destinationCoordinate.Y] == null)
            {
                HexTiles[destinationCoordinate.X, destinationCoordinate.Y] = new HexTile();
            }

            return HexTiles[destinationCoordinate.X, destinationCoordinate.Y];
        }

        public void InitBlackTiles(List<TileInstruction> instructions)
        {

            foreach (var i in instructions)
            {
                NavigateToTileAndFlip(i);
            }
        }

        public void SetAllToWhite()
        {
            for (int i = 0; i < MapSize; i++)
            {
                for (int j = 0; j < MapSize; j++)
                {
                    HexTiles[i, j] = new HexTile();
                }
            }
        }

        public void ChangeTiles(int rounds)
        {
            for (int i = 0; i < rounds; i++)
            {
                CalculateAndApplyRound();
            }
        }

        public void CalculateAndApplyRound()
        {
            CalculateNextRound();
            ApplyNextRound();
        }

        private void CalculateNextRound()
        {
            for (int i = 1; i < MapSize - 1; i++)
            {
                for (int j = 1; j < MapSize - 1; j++)
                {
                    var blackTiles = 0;
                    foreach (var adjecent in axialCoordinateByHexDirection)
                    {
                        var adjecentTile = HexTiles[i + adjecent.Value.X, j + adjecent.Value.Y];
                        if (adjecentTile != null && adjecentTile.Color)
                        {
                            blackTiles++;
                        }
                    }
                    if (HexTiles[i, j].Color)
                    {
                        if (blackTiles == 0 || blackTiles > 2)
                        {
                            HexTiles[i, j].NextRoundColor = false;
                        }
                    }
                    else
                    {
                        if (blackTiles == 2)
                        {
                            HexTiles[i, j].NextRoundColor = true;
                        }
                    }
                }
            }
        }

        private void ApplyNextRound()
        {
            for (int i = 1; i < MapSize - 1; i++)
            {
                for (int j = 1; j < MapSize - 1; j++)
                {
                    HexTiles[i, j].ApplyNextRoundColor();
                }
            }
        }
    }
}
