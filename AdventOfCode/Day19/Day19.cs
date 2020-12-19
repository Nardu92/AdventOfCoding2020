using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public class Day19
    {
        public static long Solution1(string fileName = @".\..\..\..\Day19\Input.txt")
        {
            var lines = ReadInput(fileName, true);
            Dictionary<int, Day19Rule> ruleById = CreateRules(lines.Item1);
            
            var rule0 = GenerateMessages(ruleById, 0);
            //var flattenedRules = FlattenRules(ruleById);
            long total = 0;
            foreach (var item in lines.Item2)
            {
                if (rule0.Contains(item))
                {
                    total++;
                }
            }

            return total;

        }



        public static long Solution2(string fileName = @".\..\..\..\Day19\Input.txt")
        {
            var lines = ReadInput(fileName, true);
            long total = 0;

            return total;

        }


        public static Tuple<List<string>, List<string>> ReadInput(string fileName, bool rule1)
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

        public static Dictionary<int, Day19Rule> CreateRules(List<string> input)
        {
            //89: 57 127 | 33 116
            Dictionary<int, Day19Rule> ruleById = new Dictionary<int, Day19Rule>();
            foreach (var stringrule in input)
            {
                var rule = new Day19Rule(stringrule);
                ruleById[rule.Id] = rule;
            }

            return ruleById;
        }

        private static HashSet<string> FlattenRules(Dictionary<int, Day19Rule> ruleById)
        {
            var rules = new List<string>();
            foreach (var kvp in ruleById)
            {
                rules.AddRange(GenerateMessages(ruleById, kvp.Key));
            }
            return rules.ToHashSet();
        }

        public static HashSet<string> GenerateMessages(Dictionary<int, Day19Rule> ruleById, int id)
        {
            var rule = ruleById[id];
            var list = new List<string>();
            if (rule.Group1.LeafRule)
            {
                list.Add(rule.Group1.Text);
            }
            else
            {
                var g1r1 = GenerateMessages(ruleById, rule.Group1.Rule1Id);
                if (rule.Group1.Rule2Id != -1)
                {
                    var g1r2 = GenerateMessages(ruleById, rule.Group1.Rule2Id);
                    foreach (var r1 in g1r1)
                        foreach (var r2 in g1r2)
                        {
                            list.Add($"{r1}{r2}");
                        }
                }
                else
                {
                    list.AddRange(g1r1);
                }
            }
            if (rule.Group2 != null)
            {
                if (rule.Group2.LeafRule)
                {
                    list.Add(rule.Group2.Text);
                }
                else
                {
                    var g2r1 = GenerateMessages(ruleById, rule.Group2.Rule1Id);
                    if (rule.Group2.Rule2Id != -1)
                    {
                        var g2r2 = GenerateMessages(ruleById, rule.Group2.Rule2Id);
                        foreach (var r1 in g2r1)
                            foreach (var r2 in g2r2)
                            {
                                list.Add($"{r1}{r2}");
                            }
                    }
                    else
                    {
                        list.AddRange(g2r1);
                    }
                }
            }
            return list.ToHashSet();
        }

    }

    public class Day19Rule
    {
        public int Id { get; private set; }

        public Group Group1 { get; private set; }

        public Group Group2 { get; private set; }


        public Day19Rule(string input)
        {
            var tokens = input.Split(':');
            Id = Convert.ToInt32(tokens[0]);


            if (tokens[1].Contains("|"))
            {
                var groupTokens = tokens[1].Split("|");
                Group1 = new Group(groupTokens[0]);
                Group2 = new Group(groupTokens[1]);
            }
            else
            {
                Group1 = new Group(tokens[1]);
                Group2 = null;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Id);
            sb.Append(": ");
            if (Group1.Rule1Id != -1)
            {
                sb.Append(Group1.Rule1Id);
                if (Group1.Rule2Id != -1)
                {
                    sb.Append(" ");
                    sb.Append(Group1.Rule2Id);
                }
            }
            if (Group2 != null && Group2.Rule1Id != -1)
            {
                sb.Append(" | ");
                sb.Append(Group2.Rule1Id);
                if (Group2.Rule2Id != -1)
                {
                    sb.Append(" ");
                    sb.Append(Group2.Rule2Id);
                }
            }

            if (Group1.LeafRule)
            {
                sb.Append('"');
                sb.Append(Group1.Text);
                sb.Append('"');
            }
            if (Group2 != null && Group2.LeafRule)
            {
                sb.Append(" | ");
                sb.Append('"');
                sb.Append(Group1.Text);
                sb.Append('"');
            }
            return sb.ToString();
        }
    }
    public class Group
    {
        public int Rule1Id { get; private set; }
        public int Rule2Id { get; private set; }

        public bool LeafRule { get; private set; }

        public string Text { get; private set; }

        public Group(string input)
        {
            input = input.Trim();
            if (input.Contains("\""))
            {
                LeafRule = true;
                var s = input.IndexOf('"') + 1;
                var e = input.LastIndexOf('"');
                Text = input[s..e];
                Rule1Id = -1;
                Rule2Id = -1;
            }
            else
            {

                if (input.Contains(' '))
                {
                    var tokens = input.Split(" ");
                    Rule1Id = Convert.ToInt32(tokens[0]);
                    Rule2Id = Convert.ToInt32(tokens[1]);
                }
                else
                {
                    Rule1Id = Convert.ToInt32(input);
                    Rule2Id = -1;
                }
                LeafRule = false;
                Text = string.Empty;
            }
        }
    }
}
