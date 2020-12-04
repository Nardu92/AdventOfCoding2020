using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

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

            for (int i = 0; i < tokens.Length; i++)
            {
                var fields = tokens[i].Split(':');
                Fields.Add(fields[0], fields[1]);
            }
        }

        public static List<string> ValidFields = new List<string> { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid", "cid" };
        public static Dictionary<string, Rule> Rules { get; set; } = new Dictionary<string, Rule>();


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
                return HasRequiredFields && Fields.All(x => Rules[x.Key].Validation(x.Value));
            }
        }
    }

    public class Rule
    {
        public string Field { get; set; }

        public Func<string, bool> Validation { get; set; }

        public Rule(string field, Func<string, bool> validation)
        {
            this.Field = field;
            this.Validation = validation;
        }

        public bool Validate(string value)
        {
            return Validation(value);


        }
    }

    public class Day4
    {

        public static int Day4_1Solution()
        {
            List<Passport> dt = BuildDataStructure();

            return dt.Count(x => x.HasRequiredFields);
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
                while (!string.IsNullOrEmpty(line))
                {
                    passportInput += line + " ";
                    line = inputFile.ReadLine();
                }
                dt.Add(new Passport(passportInput));
            }
            return dt;
        }

        public static long Day4_2Solution()
        {

            // byr (Birth Year) - four digits; at least 1920 and at most 2002.
            Rule byrRule = new Rule("byr", (value) =>
            {
                if (value.Length != 4)
                    return false;
                var converted = int.TryParse(value, out int intValue);

                if (!converted)
                    return false;
                if (intValue < 1920 || intValue > 2002)
                    return false;
                return true;
            });
            Passport.Rules.Add(byrRule.Field, byrRule);

            //iyr (Issue Year) - four digits; at least 2010 and at most 2020.
            Rule iyrRule = new Rule("iyr", (value) =>
            {
                if (value.Length != 4)
                    return false;
                var converted = int.TryParse(value, out int intValue);

                if (!converted)
                    return false;
                if (intValue < 2010 || intValue > 2020)
                    return false;
                return true;
            });
            Passport.Rules.Add(iyrRule.Field, iyrRule);

            // eyr (Expiration Year) - four digits; at least 2020 and at most 2030.
            Rule eyrRule = new Rule("eyr", (value) =>
            {
                if (value.Length != 4)
                    return false;
                var converted = int.TryParse(value, out int intValue);

                if (!converted)
                    return false;
                if (intValue < 2020 || intValue > 2030)
                    return false;
                return true;
            });
            Passport.Rules.Add(eyrRule.Field, eyrRule);

            //hgt (Height) - a number followed by either cm or in:
            //If cm, the number must be at least 150 and at most 193.
            //If in, the number must be at least 59 and at most 76.
            Rule hgtRule = new Rule("hgt", (value) =>
            {
                if (value.Length < 4 || value.Length > 5)
                    return false;

                var converted = int.TryParse(value[0..^2], out int intValue);

                if (!converted)
                    return false;

                var unit = value.Substring(value.Length - 2, 2);
                if (unit == "cm")
                {
                    if (intValue < 150 || intValue > 193)
                        return false;
                }
                else if (unit == "in")
                {
                    if (intValue < 59 || intValue > 76)
                        return false;
                }
                else
                {
                    return false;
                }
                return true;
            });
            Passport.Rules.Add(hgtRule.Field, hgtRule);

            if (!hgtRule.Validation("150cm"))
            {
                throw new Exception("wrong rule");
            }

            if (!hgtRule.Validation("193cm"))
            {
                throw new Exception("wrong rule");
            }
            if (!hgtRule.Validation("76in"))
            {
                throw new Exception("wrong rule");
            }
            if (!hgtRule.Validation("59in"))
            {
                throw new Exception("wrong rule");
            }

            if (hgtRule.Validation("58in"))
            {
                throw new Exception("wrong rule");
            }

            if (hgtRule.Validation("100cm"))
            {
                throw new Exception("wrong rule");
            }
            if (hgtRule.Validation("1000cm"))
            {
                throw new Exception("wrong rule");
            }
            if (hgtRule.Validation("1000mm"))
            {
                throw new Exception("wrong rule");
            }


            //hcl (Hair Color) - a # followed by exactly six characters 0-9 or a-f.
            Rule hclRule = new Rule("hcl", (value) =>
            {
                if (value.Length != 7)
                    return false;

                Regex rx = new Regex(@"^#(?:[0-9a-fA-F]{3}){1,2}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                var m = rx.Matches(value);
                if (m.Count != 1)
                    return false;

                return true;
            });
            Passport.Rules.Add(hclRule.Field, hclRule);
            if (!hclRule.Validation("#FFFFFF"))
            {
                throw new Exception("wrong rule");
            }

            if (!hclRule.Validation("#111111"))
            {
                throw new Exception("wrong rule");
            }
            if (hclRule.Validation("111111"))
            {
                throw new Exception("wrong rule");
            }
            if (hclRule.Validation("#1111GG"))
            {
                throw new Exception("wrong rule");
            }

            //ecl (Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
            Rule eclRule = new Rule("ecl", (value) =>
            {
                if (value.Length != 3)
                    return false;
                HashSet<string> allowedColors = new HashSet<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
                return allowedColors.Contains(value);
            });
            Passport.Rules.Add(eclRule.Field, eclRule);
            if (!eclRule.Validation("brn"))
            {
                throw new Exception("wrong rule");
            }
            if (eclRule.Validation("brnn"))
            {
                throw new Exception("wrong rule");
            }
            if (eclRule.Validation("red"))
            {
                throw new Exception("wrong rule");
            }

            //pid (Passport ID) - a nine-digit number, including leading zeroes.
            Rule pidRule = new Rule("pid", (value) =>
            {
                Regex rx = new Regex(@"^(?:[0-9]{9})$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                var m = rx.Matches(value);
                if (m.Count != 1)
                    return false;

                return true;
            });
            Passport.Rules.Add(pidRule.Field, pidRule);

            //cid (Country ID) - ignored, missing or not.
            Rule cidRule = new Rule("cid", (value) =>
            {
                return true;
            });
            Passport.Rules.Add(cidRule.Field, cidRule);


            List<Passport> dt = BuildDataStructure();

            return dt.Count(x => x.IsValid);
        }
    }
}
