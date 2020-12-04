using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day1
    {
        public static int Day1_1Solution()
        {
            var foundNum = CreateSet();

            var pair = FindPair(2020, foundNum);
            return pair.Key * pair.Value;
        }

        private static HashSet<int> CreateSet()
        {
            HashSet<int> numberSet = new HashSet<int>();
            StreamReader inputFile = new StreamReader(@".\..\..\..\Day1\Input1.1.txt");
            string line;
            while ((line = inputFile.ReadLine()) != null)
            {
                var value = Convert.ToInt32(line);
                numberSet.Add(value);
            }
            return numberSet;
        }

        private static KeyValuePair<int, int> FindPair(int sum, HashSet<int> set)
        {
            foreach (var number in set)
            {
                var diff = sum - number;
               
                if (diff!= number && set.TryGetValue(diff, out int actualValue))
                {
                    return new KeyValuePair<int, int>(number, actualValue);
                }
            }
            return new KeyValuePair<int, int>(-1, -1);
        }

        public static int Day1_2Solution()
        {
            var set = CreateSet();

            foreach (int i in set)
            {
                var pair = FindPair(2020 - i, set.Except(new int[1] { i }).ToHashSet());
                if (pair.Key != -1)
                {
                    return i * pair.Key * pair.Value;
                }
            }
            return -1;
        }
    }
}
