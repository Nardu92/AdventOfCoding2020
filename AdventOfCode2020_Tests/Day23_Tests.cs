using AdventOfCode;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace AdventOfCode2020_Tests
{
    public class Day23_Tests
    {

        [Fact]
        private static void TestSolution1()
        {
            var actual = Day23.Solution1("942387615");
            var expected = "36542897";
            Assert.Equal(expected, actual);
        }

        [Fact]
        private static void TestSolution1_Test()
        {
            var actual = Day23.Solution1("389125467");
            var expected = "67384529";
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        private static void TestSolution2_Test()
        {
            var actual = Day23.Solution2("389125467");
            var expected = 149245887792;
            Assert.Equal(expected, actual);
        }

        [Fact]
        private static void TestSolution2()
        {
            var actual = Day23.Solution2("942387615");
            var expected = 562136730660;
            Assert.Equal(expected, actual);
        }

    }
}
