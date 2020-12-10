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
            return 0;

        }


        private static long[] ReadInput()
        {
            //bright white bags contain 5 muted tomato bags, 4 dotted gray bags, 3 posh gold bags.
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
}
