using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace AdventOfCode.Day2
{
    public class Day2
    {
        public static int Day2_1Solution()
        {

            using StreamReader inputFile = new StreamReader(@".\..\..\..\Day2\Input2.1.txt");
            string line;
            int validPasswords = 0;
            while ((line = inputFile.ReadLine()) != null)
            {
                var linetokens = line.Split(" ");
                var occurrencyTokens = linetokens[0].Split("-");
                var minOccurrency = Convert.ToInt32(occurrencyTokens[0]);
                var maxOccurrency = Convert.ToInt32(occurrencyTokens[1]);

                char letter = linetokens[1][0];

                var password = linetokens[2];

                var occurrency = password.Count(x => x==letter);

                if (occurrency >= minOccurrency && occurrency <= maxOccurrency)
                {
                    validPasswords++;
                }

            }
            return validPasswords;
        }

        public static int Day2_2Solution()
        {
            HashSet<int> foundNum = new HashSet<int>();
            using StreamReader inputFile = new StreamReader(@".\..\..\..\Input1.1.txt");
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
