using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace AdventOfCode.Day3
{
    public class Day3
    {
        public static int Day3_1Solution()
        {
            List<bool[]> dt = BuildDataStructure();

            var rSlope = 3;
            var dSlope = 1;
            return CountTrees(dt, rSlope, dSlope);
        }

        private static int CountTrees(List<bool[]> dt, int rSlope, int dSlope)
        {
            var current = 0;
            var width = dt.First().Length;
            int trees = 0;
            for (int i = dSlope; i < dt.Count; i += dSlope)
            {
                current = (current + rSlope) % width;
                var row = dt.ElementAt(i);
                if (row[current])
                {
                    trees++;
                }
            }
            return trees;
        }

        private static List<bool[]> BuildDataStructure()
        {
            using StreamReader inputFile = new StreamReader(@".\..\..\..\Day3\Input3.txt");
            string line;
            List<bool[]> dt = new List<bool[]>();
            while ((line = inputFile.ReadLine()) != null)
            {
                //....#.##.....###...#...##.##...
                var row = new bool[line.Length];

                for (int i = 0; i < line.Length; i++)
                {
                    row[i] = line[i] == '#';
                }
                dt.Add(row);
            }
            return dt;
        }

        public static long Day3_2Solution()
        {
            List<bool[]> dt = BuildDataStructure();

            var a1 = CountTrees(dt, 1, 1);
            var a2 = CountTrees(dt, 3, 1);
            var a3 = CountTrees(dt, 5, 1);
            var a4 = CountTrees(dt, 7, 1);
            var a5 = CountTrees(dt, 1, 2);


            return (long)a1 * a2 * a3 * a4 * a5;
        }
    }
}
