using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace AdventOfCode
{
    public class Passport
    {

        public Passport(string input)
        {
            Fields = new Dictionary<string, string>();
            /*ecl: gry pid:860033327 eyr: 2020 hcl:#fffffd
    byr: 1937 iyr: 2017 cid: 147 hgt: 183cm*/
            var tokens = input.Trim().Split(' ');

            for (int i = 0; i < tokens.Length; i ++)
            {
                var fields = tokens[i].Split(':');
                Fields.Add(fields[0], fields[1]);
            }
        }

        public static List<string> ValidFields = new List<string> { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid", "cid" };
       

        public static List<string> RequiredFields = new List<string> { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
        public Dictionary<string, string> Fields { get; private set; }
        public bool HasRequiredFields
        {
            get
            {
                return RequiredFields.All(x => Fields.Keys.Contains(x));
            }
        }

        public bool IsValid
        {
            get
            {
                return RequiredFields.All(x => );
            }
        }
    }

    public class Day4
    {

        public static int Day4_1Solution()
        {
         List<Passport> dt = BuildDataStructure();

            return dt.Count(x=> x.HasRequiredFields);
        }

        private static List<Passport> BuildDataStructure()
        {
            StreamReader inputFile = new StreamReader(@".\..\..\..\Day4\Input4.txt");
            string line;
            List<Passport> dt = new List<Passport>();
            while ((line = inputFile.ReadLine()) != null)
            {
                //ecl:gry pid:860033327 eyr:2020 hcl:#fffffd
                //byr: 1937 iyr: 2017 cid: 147 hgt: 183cm
                ///r/n
                var passportInput = "";
                while(!string.IsNullOrEmpty(line))
                {
                    passportInput += line + " ";
                    line = inputFile.ReadLine();
                }
                dt.Add(new Passport(passportInput));
            }
            return dt;
        }

        public static long Day3_2Solution()
        {
            return 0;
        }
    }
}
