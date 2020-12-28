using AdventOfCode;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace AdventOfCode2020_Tests
{
    public class Day19_Tests
    {
        [Fact]
        private static void Test_Sol1()
        {
            var solution = Day19.Solution1(@".\..\..\..\..\AdventOfCode\Day19\Input.txt");
            Assert.Equal(151, solution);
        }

        [Fact]
        private static void Test_SolTestInput1()
        {
            var solution = Day19.Solution1(@".\..\..\..\..\AdventOfCode\Day19\Test1.txt");
            Assert.Equal(3, solution);
        }

        [Fact]
        private static void Test_SolTestInput2()
        {
            var solution = Day19.Solution1(@".\..\..\..\..\AdventOfCode\Day19\Test2.txt");
            Assert.Equal(2, solution);
        }

        [Fact]
        private static void Test_Sol2_TestInput()
        {
            var actual = Day19.Solution2(@".\..\..\..\..\AdventOfCode\Day19\Test1.txt");
            var expected = 12;
            Assert.Equal(expected, actual);
        }

        [Fact]
        private static void Test_Sol2()
        {
            var actual = Day19.Solution2(@".\..\..\..\..\AdventOfCode\Day19\Input.txt");
            var expected = 386;
            Assert.Equal(expected, actual);
        }

        [Fact]
        private static void Test_CreateRule8()
        {
            var rule8 = CreateRule8();

            var expected = 9;
            Assert.Equal(expected, rule8.Alternatives.Count);

            for (int i = 1; i <= rule8.Alternatives.Count; i++)
            {
                Assert.Equal(i, rule8.Alternatives.ElementAt(i - 1).Rules.Count);
            }
        }

        [Theory]
        [InlineData("a", true)]
        [InlineData("aa", true)]
        [InlineData("aaa", true)]
        [InlineData("aaaa", true)]
        [InlineData("aaaaa", true)]
        [InlineData("aaaaaa", true)]
        [InlineData("aaaaaaa", true)]
        [InlineData("aaaaaaaa", true)]
        [InlineData("aaaaaaaaa", true)]
        [InlineData("baaaaaaaa", false)]
        [InlineData("aaaaaaaab", false)]
        [InlineData("aaaaaaaaaaaaaa", false)]
        private static void Test_Match8(string message, bool expectedMatch)
        {
            var ruleById = new Dictionary<int, MessaggeRule>()
            {
                [42] = new MessaggeRule("42: \"a\""),
                [8] = CreateRule8(),
            };
            Assert.Equal(expectedMatch, ruleById[8].MessageMatchesRule(message, ruleById));
        }

        [Theory]
        [InlineData("ab", true)]
        [InlineData("aabb", true)]
        [InlineData("aaabbb", true)]
        [InlineData("aaaabbbb", true)]
        [InlineData("aaaaabbbbb", true)]
        [InlineData("aaaaaabbbbbb", true)]
        [InlineData("aaaaaaabbbbbbb", true)]
        [InlineData("aaaaaaaabbbbbbbb", true)]
        [InlineData("aaaaaaaaabbbbbbbbb", true)]
        [InlineData("aaaaaabbbbb", false)]
        [InlineData("aaaaaabbbbbbb", false)]
        [InlineData("aaabbbbb", false)]
        private static void Test_Match11(string message, bool expectedMatch)
        {
            var ruleById = new Dictionary<int, MessaggeRule>()
            {
                [42] = new MessaggeRule("42: \"a\""),
                [31] = new MessaggeRule("31: \"b\""),
                [11] = CreateRule11(),
            };
            Assert.Equal(expectedMatch, ruleById[11].MessageMatchesRule(message, ruleById));
        }

        [Theory]
        [InlineData("ab", true)]
        [InlineData("aabb", true)]
        [InlineData("aaabbb", true)]
        [InlineData("aaaabbbb", true)]
        [InlineData("aaaaabbbbb", true)]
        [InlineData("aaaaaabbbbbb", true)]
        [InlineData("aaaaaaabbbbbbb", true)]
        [InlineData("aaaaaaaabbbbbbbb", true)]
        [InlineData("aaaaaaaaabbbbbbbbb", true)]
        [InlineData("aaaaaabbbbb", false)]
        [InlineData("aaaaaabbbbbbb", false)]
        [InlineData("aaabbbbb", false)]
        [InlineData("a", true)]
        [InlineData("aa", true)]
        [InlineData("aaa", true)]
        [InlineData("aaaa", true)]
        [InlineData("aaaaa", true)]
        [InlineData("aaaaaa", true)]
        [InlineData("aaaaaaa", true)]
        [InlineData("aaaaaaaa", true)]
        [InlineData("aaaaaaaaa", true)]
        [InlineData("baaaaaaaa", false)]
        [InlineData("aaaaaaaab", false)]
        [InlineData("aaaaaaaaaaaaaa", false)]
        private static void Test_Match11And8_WithSpecialRule(string message, bool expectedMatch)
        {
            var ruleById = new Dictionary<int, MessaggeRule>()
            {
                [42] = new MessaggeRule("42: \"a\""),
                [31] = new MessaggeRule("31: \"b\""),
                [11] = CreateRule11(),
                [8] = CreateRule8(),
                [0] = new MessaggeRule("0: 8 | 11"),
            };

            Assert.Equal(expectedMatch, ruleById[0].MessageMatchesRule(message, ruleById));
        }

        [Theory]
        [InlineData("ab", true)]
        [InlineData("aabb", true)]
        [InlineData("aaabbb", true)]
        [InlineData("aaaabbbb", true)]
        [InlineData("aaaaabbbbb", true)]
        [InlineData("aaaaaabbbbbb", true)]
        [InlineData("aaaaaaabbbbbbb", true)]
        [InlineData("aaaaaaaabbbbbbbb", true)]
        [InlineData("aaaaaaaaabbbbbbbbb", true)]
        [InlineData("aaaaaabbbbb", false)]
        [InlineData("aaaaaabbbbbbb", false)]
        [InlineData("aaabbbbb", false)]
        [InlineData("a", true)]
        [InlineData("aa", true)]
        [InlineData("aaa", true)]
        [InlineData("aaaa", true)]
        [InlineData("aaaaa", true)]
        [InlineData("aaaaaa", true)]
        [InlineData("aaaaaaa", true)]
        [InlineData("aaaaaaaa", true)]
        [InlineData("aaaaaaaaa", true)]
        [InlineData("baaaaaaaa", false)]
        [InlineData("aaaaaaaab", false)]
        [InlineData("aaaaaaaaaaaaaa", true)]
        private static void Test_Match11And8(string message, bool expectedMatch)
        {
            var ruleById = new Dictionary<int, MessaggeRule>()
            {
                [42] = new MessaggeRule("42: \"a\""),
                [31] = new MessaggeRule("31: \"b\""),
                [8] = new MessaggeRule("8: 42 | 42 8"),
                [11] = new MessaggeRule("11: 42 31 | 42 11 31"),
                [0] = new MessaggeRule("0: 8 | 11"),
            };

            Assert.Equal(expectedMatch, ruleById[0].MessageMatchesRule(message, ruleById));
        }

        [Theory]
        [InlineData("cbcbaa", true)]
        [InlineData("cb", true)]
        [InlineData("aa", true)]
        [InlineData("cbca", false)]
        [InlineData("cbcbdd", true)]
        [InlineData("aacbd", false)]
        [InlineData("cbcbd", false)]
        private static void Test_SomeRecursiveRules(string message, bool expectedMatch)
        {
            var ruleById = new Dictionary<int, MessaggeRule>()
            {
                [9] = new MessaggeRule("9: \"c\""),
                [14] = new MessaggeRule("14: \"b\""),
                [1] = new MessaggeRule("1: \"a\""),
                [42] = new MessaggeRule("42: 9 14 | 1 1"),
                [31] = new MessaggeRule("31: \"d\""),
                [8] = new MessaggeRule("8: 42 | 42 8"),
                [11] = new MessaggeRule("11: 42 31 | 42 11 31"),
                [0] = new MessaggeRule("0: 8 | 11"),
            };

            Assert.Equal(expectedMatch, ruleById[0].MessageMatchesRule(message, ruleById));
        }

        [Theory]
        [InlineData("cbcbaa", false)]
        [InlineData("cbcbaad", true)]
        [InlineData("cb", false)]
        [InlineData("aa", false)]
        [InlineData("cbca", false)]
        [InlineData("aacbcbdd", true)]
        [InlineData("aacbd", true)]
        [InlineData("cbcbd", true)]
        private static void Test_SomeRecursiveRules_2(string message, bool expectedMatch)
        {
            var ruleById = new Dictionary<int, MessaggeRule>()
            {
                [9] = new MessaggeRule("9: \"c\""),
                [14] = new MessaggeRule("14: \"b\""),
                [1] = new MessaggeRule("1: \"a\""),
                [42] = new MessaggeRule("42: 9 14 | 1 1"),
                [31] = new MessaggeRule("31: \"d\""),
                [8] = new MessaggeRule("8: 42 | 42 8"),
                [11] = new MessaggeRule("11: 42 31 | 42 11 31"),
                [0] = new MessaggeRule("0: 8 11"),
            };

            Assert.Equal(expectedMatch, ruleById[0].MessageMatchesRule(message, ruleById));
        }

        [Theory]
        [InlineData("cbcbaa", false)]
        [InlineData("cbcbaad", true)]
        [InlineData("cb", false)]
        [InlineData("aa", false)]
        [InlineData("cbca", false)]
        [InlineData("aacbcbdd", true)]
        [InlineData("aacbd", true)]
        [InlineData("cbcbd", true)]
        private static void Test_SomeRecursiveRules_WithCreatedRules(string message, bool expectedMatch)
        {
            var ruleById = new Dictionary<int, MessaggeRule>()
            {
                [9] = new MessaggeRule("9: \"c\""),
                [14] = new MessaggeRule("14: \"b\""),
                [1] = new MessaggeRule("1: \"a\""),
                [42] = new MessaggeRule("42: 9 14 | 1 1"),
                [31] = new MessaggeRule("31: \"d\""),
                [8] = CreateRule8(),
                [11] = CreateRule11(),
                [0] = new MessaggeRule("0: 8 11"),
            };

            Assert.Equal(expectedMatch, ruleById[0].MessageMatchesRule(message, ruleById));
        }

        [Fact]
        private static void Test_GroupsConstructor()
        {
            var group = new GroupOfRules("1 3");
            Assert.Equal(2, group.Rules.Count);
            Assert.Contains(1, group.Rules);
            Assert.Contains(3, group.Rules);
        }

        [Fact]
        private static void Test_GroupsConstructor_2()
        {
            var group = new GroupOfRules("1");
            Assert.Single(group.Rules);
            Assert.Contains(1, group.Rules);
        }

        [Fact]
        private static void Test_GroupsConstructor_3()
        {
            var group = new GroupOfRules("1 2 3 4");
            Assert.Equal(4, group.Rules.Count);
            Assert.Contains(1, group.Rules);
            Assert.Contains(2, group.Rules);
            Assert.Contains(3, group.Rules);
            Assert.Contains(4, group.Rules);
        }

        [Fact]
        private static void Test_RuleConstructor()
        {
            var rule = new MessaggeRule("2: 1 3 | 3 1");
            Assert.Equal(2, rule.Alternatives.Count);
            var g1 = rule.Alternatives.First();
            var g2 = rule.Alternatives.Last();
            Assert.Equal(2, g1.Rules.Count);
            Assert.Equal(1, g1.Rules.First());
            Assert.Equal(3, g1.Rules.Last());

            Assert.Equal(2, g2.Rules.Count);
            Assert.Equal(3, g2.Rules.First());
            Assert.Equal(1, g2.Rules.Last());

        }

        [Fact]
        private static void Test_RuleConstructor_2()
        {
            var rule = new MessaggeRule("2: 1 3 | 3 1 | 1 2 3 4");
            Assert.Equal(3, rule.Alternatives.Count);
            var g1 = rule.Alternatives.First();
            var g3 = rule.Alternatives.Last();
            Assert.Equal(2, g1.Rules.Count);
            Assert.Equal(1, g1.Rules.First());
            Assert.Equal(3, g1.Rules.Last());

            Assert.Equal(4, g3.Rules.Count);
            Assert.Equal(1, g3.Rules.First());
            Assert.Equal(4, g3.Rules.Last());

        }

        [Fact]
        private static void Test_RuleConstructor_3()
        {
            var rule = new MessaggeRule("2: \"a\"");

            Assert.True(rule.Leaf);
            Assert.Equal('a', rule.Text);
        }

        [Fact]
        private static void Test_Rule_Match()
        {
            var ruleById = new Dictionary<int, MessaggeRule>()
            {
                [2] = new MessaggeRule("2: \"a\""),
            };
            Assert.True(ruleById[2].Leaf);
            var matches = ruleById[2].DoesRuleMatch("a", 0, ruleById);
            Assert.Single(matches);
            var match = matches.First();
            Assert.True(match.Match);
            Assert.Equal(0, match.Index);
        }

        [Fact]
        private static void Test_Group_Match()
        {
            var ruleById = new Dictionary<int, MessaggeRule>()
            {
                [2] = new MessaggeRule("2: \"a\""),
                [3] = new MessaggeRule("3: 2 | 2 2"),
            };
            var matches = ruleById[3].DoesRuleMatch("a", 0, ruleById);
            Assert.Single(matches);
            var match = matches.First();
            Assert.True(match.Match);
            Assert.Equal(0, match.Index);
        }

        [Fact]
        private static void Test_Match_FromTest2File()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day19\Test2.txt";
            var input = Day19.ReadInput(fileName);
            Dictionary<int, MessaggeRule> ruleById = Day19.CreateRulesFromFile(input.Item1);
            var messages = input.Item2;
            var total = 0;
            foreach (var message in messages)
            {
                var matches = ruleById[0].DoesRuleMatch(message, 0, ruleById);
                if (matches.Any(x => x.Match && x.Index == message.Length - 1))
                {
                    total++;
                }
            }
            Assert.Equal(2, total);
        }

        public static MessaggeRule CreateRule11()
        {
            var rule42 = "42 ";
            var rule31 = "31 ";
            List<string> rule11s = new List<string>();
            for (int i = 1; i < 10; i++)
            {
                rule11s.Add(rule42.Repeat(i) + rule31.Repeat(i));
            }
            var rule11 = new MessaggeRule($"11: {string.Join("| ", rule11s)}");
            return rule11;
        }

        public static MessaggeRule CreateRule8()
        {
            var rule42 = "42 ";
            List<string> rule42s = new List<string>();
            for (int i = 1; i < 10; i++)
            {
                rule42s.Add(rule42.Repeat(i));
            }
            return new MessaggeRule($"8: {string.Join("| ", rule42s)}");
        }
    }
}
