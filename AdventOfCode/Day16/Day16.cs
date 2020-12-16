using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day16
    {
        public static long Solution1(string fileName = @".\..\..\..\Day16\Input.txt")
        {
            long total = 0;
            var input = ReadInput(fileName);
            foreach (var passport in input.CryptPassports)
            {
                foreach (var value in passport.Values)
                {
                    var a = input.ValueValidForField(value);
                    if (string.IsNullOrEmpty(a))
                    {
                        total += value;
                    }
                }
            }
            return total;
        }


        public static long Solution2(string fileName = @".\..\..\..\Day16\Input.txt")
        {

            var input = ReadInput(fileName);
            List<CryptPassport> validPasses = GetValidPassports(input);

            Dictionary<int, List<int>> valuesByFieldPositionInPassport = GetValuesGroupedByPosition(validPasses);
            Dictionary<int, HashSet<string>> validFieldsByPosition = GetListOfPossibleFieldsByPosition(input, valuesByFieldPositionInPassport);
            Dictionary<string, int> positionByFieldName = GetPositionByFieldName(validPasses, validFieldsByPosition);

            long answer = 1;
            foreach (KeyValuePair<string, int> field in positionByFieldName.Where(x => x.Key.StartsWith("departure")))
            {
                answer *= input.MyPassport.Values.ElementAt(field.Value);
            }
            return answer;
        }

        private static Dictionary<string, int> GetPositionByFieldName(List<CryptPassport> validPasses, Dictionary<int, HashSet<string>> validFieldsByPosition)
        {
            var positionByFieldName = new Dictionary<string, int>();
            var numberOfFieldsInAPassport = validPasses.First().Values.Count;
            for (int i = 0; i < numberOfFieldsInAPassport; i++)
            {
                var sureGuess = validFieldsByPosition.Single(x => x.Value.Count == 1);
                var passportFieldName = sureGuess.Value.First();
                positionByFieldName[passportFieldName] = sureGuess.Key;
                foreach (KeyValuePair<int, HashSet<string>> kvp in validFieldsByPosition)
                {
                    kvp.Value.Remove(passportFieldName);
                }
            }

            return positionByFieldName;
        }

        private static Dictionary<int, HashSet<string>> GetListOfPossibleFieldsByPosition(InputClass input, Dictionary<int, List<int>> valuesByFieldPositionInPassport)
        {
            //All the fields key
            var allFieldsKeys = input.PassportFields.Select(x => x.Name).ToList();
            Dictionary<int, HashSet<string>> validFieldsByPosition = new Dictionary<int, HashSet<string>>();
            foreach (KeyValuePair<int, List<int>> kvp in valuesByFieldPositionInPassport)
            {
                var validFieldsAtPosition = allFieldsKeys.ToHashSet();
                foreach (var value in kvp.Value)
                {
                    var validFieldsForValue = input.ForWhichFieldsIsThisValueValid(value);
                    validFieldsAtPosition.IntersectWith(validFieldsForValue);
                }
                validFieldsByPosition[kvp.Key] = validFieldsAtPosition;
            }

            return validFieldsByPosition;
        }

        private static Dictionary<int, List<int>> GetValuesGroupedByPosition(List<CryptPassport> validPasses)
        {
            var numberOfFieldsInAPassport = validPasses.First().Values.Count;
            Dictionary<int, List<int>> valuesByFieldPositionInPassport = new Dictionary<int, List<int>>();
            for (int i = 0; i < numberOfFieldsInAPassport; i++)
            {
                //retrieve all the fields at position i
                var list = new List<int>();
                foreach (var p in validPasses)
                {
                    list.Add(p.Values.ElementAt(i));
                }
                valuesByFieldPositionInPassport[i] = list;
            }

            return valuesByFieldPositionInPassport;
        }

        private static List<CryptPassport> GetValidPassports(InputClass input)
        {
            var validPasses = new List<CryptPassport>();
            foreach (var passport in input.CryptPassports)
            {
                var valid = true;
                foreach (var value in passport.Values)
                {
                    var a = input.ValueValidForField(value);
                    if (string.IsNullOrEmpty(a))
                    {
                        valid = false;
                        break;
                    }
                }
                if (valid)
                {
                    validPasses.Add(passport);
                }
            }

            return validPasses;
        }

        private static InputClass ReadInput(string fileName)
        {
            using StreamReader inputFile = new StreamReader(fileName);
            string line;
            List<PassportField> fields = new List<PassportField>();
            while (!string.IsNullOrEmpty(line = inputFile.ReadLine()))
            {
                fields.Add(new PassportField(line));
            }

            line = inputFile.ReadLine();
            line = inputFile.ReadLine();
            var myPassport = new CryptPassport(line);

            line = inputFile.ReadLine();
            line = inputFile.ReadLine();
            List<CryptPassport> otherPassports = new List<CryptPassport>();
            while (!string.IsNullOrEmpty(line = inputFile.ReadLine()))
            {
                otherPassports.Add(new CryptPassport(line));
            }

            return new InputClass(fields, otherPassports, myPassport);
        }
    }

    public class PassportField
    {
        public string Name { get; set; }
        List<ValueRange> ValidRanges { get; set; }


        public PassportField(string input)
        {
            ValidRanges = new List<ValueRange>();
            var s = input.Split(':');
            var name = s[0];
            Name = name;
            var stringRanges = s[1].Split("or");

            foreach (var r in stringRanges)
            {
                var minAndMax = r.Split("-");
                var t = new ValueRange(Convert.ToInt32(minAndMax[0]), Convert.ToInt32(minAndMax[1]));
                ValidRanges.Add(t);
            }
        }

        public bool ValueValidForField(int value)
        {
            foreach (var range in this.ValidRanges)
            {
                if (range.ValueInRange(value))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class ValueRange
    {
        int Min;
        int Max;
        public ValueRange(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public bool ValueInRange(int value)
        {
            return value <= Max && value >= Min;
        }
    }

    public class CryptPassport
    {
        public List<int> Values { get; set; }
        public CryptPassport(string input)
        {
            Values = input.Split(',').Select(x => Convert.ToInt32(x)).ToList();
        }
    }

    public class InputClass
    {
        public List<PassportField> PassportFields;
        public List<CryptPassport> CryptPassports;
        public CryptPassport MyPassport;
        public InputClass(List<PassportField> passportFields, List<CryptPassport> cryptPassports, CryptPassport myPassport)
        {
            MyPassport = myPassport;
            CryptPassports = cryptPassports;
            PassportFields = passportFields;
        }

        public string ValueValidForField(int value)
        {
            foreach (var field in PassportFields)
            {
                if (field.ValueValidForField(value))
                {
                    return field.Name;
                }
            }
            return string.Empty;
        }

        public List<string> ForWhichFieldsIsThisValueValid(int value)
        {
            var list = new List<string>();
            foreach (var field in PassportFields)
            {
                if (field.ValueValidForField(value))
                {
                    list.Add(field.Name);
                }
            }
            return list;
        }
    }
}
