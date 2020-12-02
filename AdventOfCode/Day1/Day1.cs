using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode.Day1
{
    public class Day1
    {
        public static int Day1_1Solution()
        {
            HashSet<int> foundNum = new HashSet<int>();
            using StreamReader inputFile = new StreamReader(@"C:\Users\nicna\source\repos\AdventOfCode\AdventOfCode\Day1\Input1.1.txt");
            string line;
            while ((line = inputFile.ReadLine()) != null)
            {
                var value = Convert.ToInt32(line);
                foundNum.Add(value);
                var diff = 2020 - value;
                if (foundNum.TryGetValue(diff, out int actualValue))
                {
                    return diff * value;
                }
            }
            return -1;
        }

        public static int Day1_2Solution()
        {
            HashSet<int> foundNum = new HashSet<int>();
            using StreamReader inputFile = new StreamReader(@"C:\Users\nicna\source\repos\AdventOfCode\AdventOfCode\Day1\Input1.1.txt");
            string line;
            while ((line = inputFile.ReadLine()) != null)
            {
                var value = Convert.ToInt32(line);
                foundNum.Add(value);
            }

            foreach( int i in foundNum)
            {
                foreach (int j in foundNum)
                {
                    foreach (int k in foundNum)
                    {
                        if(i+j+k == 2020)
                        {
                            return i * j * k;
                        }
                    }
                }
            }
            return -1;
        }
    }
}
