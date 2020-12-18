using AdventOfCode;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace AdventOfCode2020_Tests
{
    public class Day18_Tests
    {
        [Fact]
        private static void TestOperation1()
        {
            
            var actual = Day18.SolveOperation("1 + 2");
            long expected = 3;
            Assert.Equal(expected, actual);
        }

        [Fact]
        private static void TestOperation2()
        {

            var actual = Day18.SolveOperation("1 + 2 * 3 + 4 * 5 + 6");
            long expected = 71;
            Assert.Equal(expected, actual);
        }
        [Fact]
        private static void TestOperation3()
        {

            var actual = Day18.SolveOperation("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2");
            long expected = 13632;
            Assert.Equal(expected, actual);
        }

        [Fact]
        private static void TestOperation4()
        {

            var actual = Day18.SolveOperation("2 * 3 + (4 * 5)");
            long expected = 26;
            Assert.Equal(expected, actual);
        }

        [Fact]
        private static void TestOperation5()
        {

            var actual = Day18.SolveOperation("5 + (8 * 3 + 9 + 3 * 4 * 3)");
            long expected = 437;
            Assert.Equal(expected, actual);
        }
        [Fact]
        private static void TestOperation6()
        {

            var actual = Day18.SolveOperation("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))");
            long expected = 12240;
            Assert.Equal(expected, actual);
        }

    }
}
