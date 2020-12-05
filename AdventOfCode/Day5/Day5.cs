using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day5
    {
        public static int Day5_1Solution()
        {

            return CreateSet().Max();
        }

        private static HashSet<int> CreateSet()
        {
            HashSet<int> numberSet = new HashSet<int>();
            StreamReader inputFile = new StreamReader(@".\..\..\..\Day5\Input5.txt");
            string line;
            while ((line = inputFile.ReadLine()) != null)
            {
                var value = ConvertToSeatId(line);

                numberSet.Add(value);
            }
            return numberSet;
        }
        
        private static int ConvertToSeatId(string line)
        {
            var binaryText = line.Replace('F', '0').Replace('B', '1').Replace('L', '0').Replace('R', '1');
            return Convert.ToInt32(binaryText, 2);
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
