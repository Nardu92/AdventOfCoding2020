using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public class Day17
    {
        public static long Solution1(string fileName = @".\..\..\..\Day17\Input.txt")
        {
            var lines = ReadInput(fileName, true);
            var space = new Space3D(lines.ToArray(), false);

            for (int i = 0; i < 6; i++)
            {
                space.CalculateRound();
            }

            return space.GetActiveCubes();
        }

        public static long Solution2(string fileName = @".\..\..\..\Day17\Input.txt")
        {
            var lines = ReadInput(fileName, true);
            var space = new Space4D(lines.ToArray(), false);

            for (int i = 0; i < 6; i++)
            {
                space.CalculateRound();
            }

            return space.GetActiveCubes();
        }


        private static List<string> ReadInput(string fileName, bool rule1)
        {
            using StreamReader inputFile = new StreamReader(fileName);
            string line;
            List<string> lines = new List<string>();
            while ((line = inputFile.ReadLine()) != null)
            {
                lines.Add(line);
            }
            return lines;
        }
    }

    public class Space3D
    {
        Cube[][][] Cubes;
        int SpaceY;
        int SpaceX;
        int SpaceZ;
        bool Rule1;
        public Space3D(string[] spaceLayout, bool rule1)
        {
            var offset = 8;
            SpaceY = spaceLayout.Length + 2 * offset;
            SpaceX = spaceLayout.First().Length + 2 * offset;
            SpaceZ = 2 * offset;

            Cubes = new Cube[SpaceY][][];
            for (int y = 0; y < SpaceY; y++)
            {

                Cubes[y] = new Cube[SpaceX][];
                for (int x = 0; x < SpaceX; x++)
                {
                    Cubes[y][x] = new Cube[SpaceZ];
                    for (int z = 0; z < SpaceZ; z++)
                    {
                        Cubes[y][x][z] = new Cube(false);
                    }
                }
            }

            Rule1 = rule1;

            var spaceHeight = spaceLayout.Length;
            var spaceWidth = spaceLayout.First().Length;
            for (int y = 0; y < spaceHeight; y++)
            {
                for (int x = 0; x < spaceWidth; x++)
                {
                    Cubes[y + offset][x + offset][offset].Init(spaceLayout[y][x]);
                }
            }
            BuildAdjecents();
        }

        private void BuildAdjecents()
        {
            for (int y = 0; y < SpaceY; y++)
            {
                for (int x = 0; x < SpaceX; x++)
                {
                    for (int z = 0; z < SpaceZ; z++)
                    {
                        for (int i = -1; i <= 1; i++)
                        {
                            for (int j = -1; j <= 1; j++)
                            {
                                for (int k = -1; k <= 1; k++)
                                {
                                    if (i == k && i == j && i == 0)
                                    {
                                        //skip because this is same cube
                                        continue;
                                    }
                                    CalculateAdjecent(y, x, z, i, j, k);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void CalculateAdjecent(int y, int x, int z, int i, int j, int k)
        {
            var yi = y + i;
            var xj = x + j;
            var zk = z + k;
            if (yi >= 0 && yi < SpaceY && xj >= 0 && xj < SpaceX && zk >= 0 && zk < SpaceZ)
            {
                //the neighbour has a valid index
                var cube = Cubes[y][x][z];
                cube.AdjecentCubes.Add(Cubes[yi][xj][zk]);
            }
        }

        public bool CalculateRound()
        {
            foreach (var row in Cubes)
            {
                foreach (var column in row)
                {
                    foreach (var cube in column)
                    {
                        cube.CalculateRound(Rule1 ? 4 : 5);
                    }
                }
            }
            bool somethingChanged = false;
            foreach (var row in Cubes)
            {
                foreach (var column in row)
                {
                    foreach (var cube in column)
                    {
                        somethingChanged = cube.ApplyRound() || somethingChanged;
                    }
                }
            }
            return somethingChanged;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var row in Cubes)
            {
                var line = "";
                foreach (var seat in row)
                {
                    line += seat.ToString();
                }
                sb.AppendLine(line);
            }
            return sb.ToString();

        }

        public int GetActiveCubes()
        {
            return Cubes.SelectMany(x => x).SelectMany(x => x).Count(s => s.On);
        }
    }


    public class Space4D
    {
        Cube[][][][] Cubes;
        int SpaceY;
        int SpaceX;
        int SpaceZ;
        int SpaceW;

        bool Rule1;
        public Space4D(string[] spaceLayout, bool rule1)
        {
            var offset = 8;
            SpaceY = spaceLayout.Length + 2 * offset;
            SpaceX = spaceLayout.First().Length + 2 * offset;
            SpaceZ = 2 * offset;
            SpaceW = 2 * offset;

            Cubes = new Cube[SpaceY][][][];
            for (int y = 0; y < SpaceY; y++)
            {

                Cubes[y] = new Cube[SpaceX][][];
                for (int x = 0; x < SpaceX; x++)
                {
                    Cubes[y][x] = new Cube[SpaceZ][];
                    for (int z = 0; z < SpaceZ; z++)
                    {
                        Cubes[y][x][z] = new Cube[SpaceW];
                        for (int w = 0; w < SpaceW; w++)
                        {
                            Cubes[y][x][z][w] = new Cube(false);
                        }
                    }
                }
            }

            Rule1 = rule1;

            var spaceHeight = spaceLayout.Length;
            var spaceWidth = spaceLayout.First().Length;
            for (int y = 0; y < spaceHeight; y++)
            {
                for (int x = 0; x < spaceWidth; x++)
                {
                    Cubes[y + offset][x + offset][offset][offset].Init(spaceLayout[y][x]);
                }
            }
            BuildAdjecents();
        }

        private void BuildAdjecents()
        {
            for (int y = 0; y < SpaceY; y++)
            {
                for (int x = 0; x < SpaceX; x++)
                {
                    for (int z = 0; z < SpaceZ; z++)
                    {
                        for (int w = 0; w < SpaceW; w++)
                        {
                            for (int i = -1; i <= 1; i++)
                            {
                                for (int j = -1; j <= 1; j++)
                                {
                                    for (int k = -1; k <= 1; k++)
                                    {
                                        for (int l = -1; l <= 1; l++)
                                        {
                                            if (i == k && i == j && i == l && i == 0)
                                            {
                                                //skip because this is same cube
                                                continue;
                                            }
                                            CalculateAdjecent(y, x, z, w, i, j, k, l);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void CalculateAdjecent(int y, int x, int z, int w, int i, int j, int k, int l)
        {
            var yi = y + i;
            var xj = x + j;
            var zk = z + k;
            var wl = w + l;
            if (yi >= 0 && yi < SpaceY && xj >= 0 && xj < SpaceX && zk >= 0 && zk < SpaceZ && wl >= 0 && wl < SpaceW)
            {
                //the neighbour has a valid index
                var cube = Cubes[y][x][z][w];
                cube.AdjecentCubes.Add(Cubes[yi][xj][zk][wl]);
            }
        }

        public bool CalculateRound()
        {
            foreach (var row in Cubes)
            {
                foreach (var column in row)
                {
                    foreach (var hyperz in column)
                    {
                        foreach (var cube in hyperz)
                        {
                            cube.CalculateRound(Rule1 ? 4 : 5);
                        }
                    }
                }
            }
            bool somethingChanged = false;
            foreach (var row in Cubes)
            {
                foreach (var column in row)
                {
                    foreach (var hyperz in column)
                    {
                        foreach (var cube in hyperz)
                        {
                            somethingChanged = cube.ApplyRound() || somethingChanged;
                        }
                    }
                }
            }
            return somethingChanged;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var row in Cubes)
            {
                var line = "";
                foreach (var seat in row)
                {
                    line += seat.ToString();
                }
                sb.AppendLine(line);
            }
            return sb.ToString();

        }

        public int GetActiveCubes()
        {
            return Cubes.SelectMany(x => x).SelectMany(x => x).SelectMany(x => x).Count(s => s.On);
        }
    }

    public class Cube
    {
        public List<Cube> AdjecentCubes { get; set; }

        public bool On { get; private set; }

        public Cube(bool on)
        {
            On = on;
            this.AdjecentCubes = new List<Cube>();
        }

        bool nextState = false;
        public void CalculateRound(int limit)
        {
            //If a cube is active and exactly 2 or 3 of its neighbors are also active, the cube remains active. 
            //Otherwise becomes inactive
            var activeAdjecent = AdjecentCubes.Count(x => x.On);
            if (On)
            {
                if (activeAdjecent == 2 || activeAdjecent == 3)
                {
                    nextState = true;
                    return;
                }
                else
                {
                    nextState = false;
                    return;
                }
            }

            //If a cube is inactive but exactly 3 of its neighbors are active, the cube becomes active.
            if (!On && activeAdjecent == 3)
            {
                nextState = true;
                return;
            }
            nextState = On;
            return;
        }

        public bool ApplyRound()
        {
            if (On != nextState)
            {
                On = nextState;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return On ? "#" : ".";
        }

        internal void Init(char v)
        {
            On = v == '#';
        }
    }
}
