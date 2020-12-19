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
        private static void Test_Rule127()
        {
            Dictionary<int, Day19Rule> ruleById = InitRules();
            var messages = Day19.GenerateMessages(ruleById, 127);
            Assert.Single(messages);
            Assert.Equal("b", messages.First());
        }

        [Fact]
        private static void Test_Rule116()
        {
            Dictionary<int, Day19Rule> ruleById = InitRules();
            var messages = Day19.GenerateMessages(ruleById, 116);
            Assert.Single(messages);
            Assert.Equal("a", messages.First());
        }

        [Fact]
        private static void Test_Rule78()
        {
            Dictionary<int, Day19Rule> ruleById = InitRules();
            var messages = Day19.GenerateMessages(ruleById, 78);
            Assert.Single(messages);
            Assert.Equal("ab", messages.First());
        }

        [Fact]
        private static void Test_Rule59()
        {
            Dictionary<int, Day19Rule> ruleById = InitRules();
            var messages = Day19.GenerateMessages(ruleById, 59);
            Assert.Equal(2, messages.Count);
            Assert.Equal("ab", messages.First());
            Assert.Equal("bb", messages.Last());
        }

        [Fact]
        private static void Test_Rule7()
        {
            Dictionary<int, Day19Rule> ruleById = InitRules();
            var messages = Day19.GenerateMessages(ruleById, 7);
            Assert.Equal(2, messages.Count);
            Assert.Equal("aaa", messages.First());
            Assert.Equal("aab", messages.Last());
        }


        [Fact]
        private static void Test_Rule100()
        {
            Dictionary<int, Day19Rule> ruleById = InitRules();
            var messages = Day19.GenerateMessages(ruleById, 100);
            Assert.Equal(2, messages.Count);
            Assert.Equal("aa", messages.First());
            Assert.Equal("ab", messages.Last());
        }


        //15: 127 59 | 116 78
        [Fact]
        private static void Test_Rule15()
        {
            Dictionary<int, Day19Rule> ruleById = InitRules();
            var messages = Day19.GenerateMessages(ruleById, 15);
            Assert.Equal(3, messages.Count);
            Assert.Equal("bab", messages.ElementAt(0));
            Assert.Equal("bbb", messages.ElementAt(1));
            Assert.Equal("aab", messages.ElementAt(2));
        }

        //50: 15 116 | 7 127
        [Fact]
        private static void Test_Rule50()
        {
            Dictionary<int, Day19Rule> ruleById = InitRules();
            var messages = Day19.GenerateMessages(ruleById, 50);
            Assert.Equal(5, messages.Count);
            Assert.Equal("baba", messages.ElementAt(0));
            Assert.Equal("bbba", messages.ElementAt(1));
            Assert.Equal("aaba", messages.ElementAt(2));
            Assert.Equal("aaab", messages.ElementAt(3));
            Assert.Equal("aabb", messages.ElementAt(4));
        }

        private static Dictionary<int, Day19Rule> InitRules()
        {
            string fileName = @".\..\..\..\..\AdventOfCode\Day19\Input.txt";
            var lines = Day19.ReadInput(fileName, true);
            Dictionary<int, Day19Rule> ruleById = Day19.CreateRules(lines.Item1);
            return ruleById;
        }
    }
}
