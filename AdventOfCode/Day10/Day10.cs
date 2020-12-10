using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day10
    {
        public static long DaySolution1()
        {
            var nums = ReadInput();
            var diff1 = 1;
            var diff3 = 0;
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i - 1] == nums[i] - 1)
                {
                    diff1++;
                } else if (nums[i - 1] == nums[i] - 2)
                {

                } else if (nums[i - 1] == nums[i] - 3) { 
                    diff3++;
                }else
                {
                    throw new Exception("shouldnt get here");
                }
            }
            
            return diff1 * diff3;
        }

        public static long DaySolution2()
        {
            var nums = ReadInput();
            var cs = new CombinationsSolver(nums.ToHashSet());
            return cs.Solve();
        }


        private static long[] ReadInput()
        {
            using StreamReader inputFile = new StreamReader(@".\..\..\..\Day10\Input.txt");
            string line;
            List<long> nums = new List<long>();
            while ((line = inputFile.ReadLine()) != null)
            {
                nums.Add(Convert.ToInt64(line));
            }
            nums.Add(nums.Max() + 3);
            var array = nums.ToArray();
            Array.Sort(array);

            return array;
        }
    }

    public class CombinationsSolver
    {
        HashSet<long> numbers;
        Dictionary<long, long> lookupDict;
        public CombinationsSolver(HashSet<long> numbers)
        {
            this.numbers = numbers;
            lookupDict = new Dictionary<long, long>();
        }
        public long Solve()
        {
            return Solve(numbers.Max());
        }

        private long Solve(long n)
        {
            if (!numbers.Contains(n)) return 0;
            if (n == 1) return 1;
            if (n == 2) return Solve(1) + 1;
            if (n == 3) return Solve(1) + Solve(2) + 1;

            var n1Key = n - 1;
            if (!lookupDict.TryGetValue(n1Key, out long n1Value))
            {
                n1Value = Solve(n1Key);
                lookupDict.Add(n1Key, n1Value);
            }
            var n2Key = n - 2;
            if (!lookupDict.TryGetValue(n2Key, out long n2Value))
            {
                n2Value = Solve(n2Key);
                lookupDict.Add(n2Key, n2Value);
            }
            var n3Key = n - 3;
            if (!lookupDict.TryGetValue(n3Key, out long n3Value))
            {
                n3Value = Solve(n3Key);
                lookupDict.Add(n3Key, n3Value);
            }
            return n1Value + n2Value + n3Value;
        }

    }
}
