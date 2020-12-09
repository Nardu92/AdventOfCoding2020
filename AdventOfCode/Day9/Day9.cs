using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day9
    {
        public static long Day9_1Solution()
        {
            var nums = ReadInput();
            return FindWeakness1(nums);
        }

        private static long FindWeakness1(long[] nums, int preambleLenght = 25)
        {
            var currentindex = preambleLenght;
            bool condition = false;
            do
            {
                var start = currentindex - preambleLenght;
                var finish = currentindex;
                var pair = FindPair(nums[currentindex], nums[start..finish].ToHashSet());
                if (pair.Key == -1)
                {
                    condition = true;
                }
                else
                {
                    currentindex++;
                }

            } while (!condition);

            return nums[currentindex];
        }

        public static long Day9_2Solution()
        {
            var nums = ReadInput();
            var sum = FindWeakness1(nums);
            var condition = false;
            var currentLenght = 2;
            long[] solution;
            do
            {
                solution = FindSubSet(sum, nums, currentLenght);
                if (solution == null)
                {
                    currentLenght++;
                }
                else
                {
                    condition = true;
                }
            } while (!condition);
            return solution.Min() + solution.Max();
        }

        private static long[] FindSubSet(long sum, long[]numbers, int lenghtOfTheSubset)
        {

            for (int i = 0; i < numbers.Length - lenghtOfTheSubset; i++)
            {
                long partialsum = 0;
                for (int j = i; j < i + lenghtOfTheSubset; j++)
                {
                    partialsum += numbers[j];
                }
                if(partialsum> sum)
                {
                    break;
                }
                if (partialsum == sum)
                {
                    var endRange = i + lenghtOfTheSubset;
                    return numbers[i..endRange];
                }
            }
            return null;
        }

        private static KeyValuePair<long, long> FindPair(long sum, HashSet<long> set)
        {
            foreach (var number in set)
            {
                var diff = sum - number;

                if (diff != number && set.TryGetValue(diff, out long actualValue))
                {
                    return new KeyValuePair<long, long>(number, actualValue);
                }
            }
            return new KeyValuePair<long, long>(-1, -1);
        }

        private static long[] ReadInput()
        {
            //bright white bags contain 5 muted tomato bags, 4 dotted gray bags, 3 posh gold bags.
            using StreamReader inputFile = new StreamReader(@".\..\..\..\Day9\Input.txt");
            string line;
            List<long> nums = new List<long>();
            while ((line = inputFile.ReadLine()) != null)
            {
                nums.Add(Convert.ToInt64(line));
            }
            return nums.ToArray();
        }
    }
}
