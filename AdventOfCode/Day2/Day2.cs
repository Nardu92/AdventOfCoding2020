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

                var first = password[minOccurrency-1]== letter;
                var second = password[maxOccurrency-1]== letter;

                if(first ^ second)
                {
                    validPasswords++;
                }

            }
            return validPasswords;
        }
    }
}
