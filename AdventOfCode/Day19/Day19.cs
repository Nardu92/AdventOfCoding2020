using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public static class StringExtensions
    {
        public static string Repeat(this string instr, int n)
        {
            var result = string.Empty;

            for (var i = 0; i < n; i++)
                result += instr;

            return result;
        }
    }


    public class Day19
    {
        public static long Solution1(string fileName = @".\..\..\..\Day19\Input.txt")
        {
            var input = ReadInput(fileName);
            Dictionary<int, MessaggeRule> ruleById = Day19.CreateRulesFromFile(input.Item1);
            var messages = input.Item2;
            var total = 0;
            foreach (var message in messages)
            {
                if (ruleById[0].MessageMatchesRule(message, ruleById))
                {
                    total++;
                }
            }
            return total;
        }

        public static long Solution2(string fileName = @".\..\..\..\Day19\Input.txt")
        {
            var input = ReadInput(fileName);
            Dictionary<int, MessaggeRule> ruleById = CreateRulesFromFile(input.Item1);

            //update rule 8
            ruleById[8] = new MessaggeRule("8: 42 | 42 8");
            ruleById[11] = new MessaggeRule("11: 42 31 | 42 11 31");
            var messages = input.Item2;
            var total = 0;
            foreach (var message in messages)
            {
                if (ruleById[0].MessageMatchesRule(message, ruleById))
                {
                    total++;
                }
            }
            return total;

        }

        public static Tuple<List<string>, List<string>> ReadInput(string fileName)
        {
            using StreamReader inputFile = new StreamReader(fileName);
            string line;
            List<string> rules = new List<string>();
            while (!string.IsNullOrEmpty(line = inputFile.ReadLine()))
            {
                rules.Add(line);
            }

            List<string> messages = new List<string>();
            while (!string.IsNullOrEmpty(line = inputFile.ReadLine()))
            {
                messages.Add(line);
            }

            return new Tuple<List<string>, List<string>>(rules, messages);
        }

        public static Dictionary<int, MessaggeRule> CreateRulesFromFile(List<string> input)
        {
            //89: 57 127 | 33 116
            Dictionary<int, MessaggeRule> ruleById = new Dictionary<int, MessaggeRule>();
            foreach (var stringrule in input)
            {
                var rule = new MessaggeRule(stringrule);
                ruleById[rule.Id] = rule;
            }

            return ruleById;
        }
    }

    public class MessaggeRule
    {
        private const string GroupsSeparator = "|";

        public int Id { get; private set; }

        public List<GroupOfRules> Alternatives { get; private set; }

        public bool Leaf { get; private set; }

        public char Text { get; private set; }

        public MessaggeRule(string input)
        {
            var tokens = input.Split(':');
            Id = Convert.ToInt32(tokens[0]);
            if (tokens[1].Contains("\""))
            {
                Leaf = true;
                var s = tokens[1].IndexOf('"') + 1;
                Text = tokens[1][s];
            }
            else
            {
                Alternatives = tokens[1].Split(GroupsSeparator).Select(x => new GroupOfRules(x.Trim())).ToList();
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"{Id} : ");
            if (Leaf)
            {
                sb.Append(Text);
            }
            else
            {
                sb.Append(string.Join($" {GroupsSeparator} ", Alternatives));
            }
            return sb.ToString();
        }

        public bool MessageMatchesRule(string message, Dictionary<int, MessaggeRule> ruleById)
        {
            var matches = DoesRuleMatch(message, 0, ruleById);
            return matches.Any(x => x.Match && x.Index == message.Length - 1);
        }
        public List<MatchResult> DoesRuleMatch(string message, int index, Dictionary<int, MessaggeRule> ruleById)
        {
            if (index >= message.Length)
            {
                return new List<MatchResult>();
            }
            if (Leaf)
            {
                return new List<MatchResult> { new MatchResult(message[index] == Text, index) };
            }
            else
            {
                var matches = new List<MatchResult>();
                foreach (var a in Alternatives)
                {
                    matches.AddRange(a.DoesRuleMatch(message, index, ruleById));
                }
                return matches;
            }
        }

    }

    public class MatchResult
    {
        public bool Match { get; private set; }
        public int Index { get; private set; }

        public MatchResult(bool v, int index)
        {
            this.Match = v;
            this.Index = index;
        }
    }

    public class GroupOfRules
    {
        public List<int> Rules { get; private set; }
        public GroupOfRules(string rules)
        {
            Rules = rules.Split(" ").Select(x => Convert.ToInt32(x)).ToList();
        }

        public override string ToString()
        {
            return string.Join(' ', Rules);
        }

        internal IEnumerable<MatchResult> DoesRuleMatch(string message, int index, Dictionary<int, MessaggeRule> ruleById)
        {
            var ruleId = Rules.First();
            MessaggeRule rule = ruleById[ruleId];
            List<MatchResult> matches = rule.DoesRuleMatch(message, index, ruleById);

            List<MatchResult> nextMatches;
            for (int i = 1; i < Rules.Count; i++)
            {
                nextMatches = new List<MatchResult>();
                var nextRuleId = Rules.ElementAt(i);
                var nextRule = ruleById[nextRuleId];
                foreach (var match in matches)
                {
                    if (match.Match)
                    {
                        nextMatches.AddRange(nextRule.DoesRuleMatch(message, match.Index + 1, ruleById));
                    }
                }
                matches = nextMatches;
            }
            return matches;
        }
    }
}
