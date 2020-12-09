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
            var currentindex = 25;
            bool condition = false;
            do
            {
                var start = currentindex - 25;
                var finish = currentindex + 25;
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

        public static int Day8_2Solution()
        {
            return 0;
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
