﻿using System;
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
            using StreamReader inputFile = new StreamReader(@".\..\..\..\Day5\Input5.txt");
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


        public static int Day5_2Solution()
        {
            var set = CreateSet();
            var max = set.Max();
            var min = set.Min();
            for (int i = min + 1; i < max; i++)
            {
                if(!set.TryGetValue(i, out int _))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
