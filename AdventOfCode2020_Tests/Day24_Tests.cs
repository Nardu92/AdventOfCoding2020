using AdventOfCode;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace AdventOfCode2020_Tests
{
    public class Day24_Tests
    {

        [Fact]
        private static void TestSolution1()
        {
            var actual = Day24.Solution1(@".\..\..\..\..\AdventOfCode\Day24\Input.txt");
            var expected = 317;
            Assert.Equal(expected, actual);
        }

        [Fact]
        private static void TestSolution1_Test()
        {
            var actual = Day24.Solution1(@".\..\..\..\..\AdventOfCode\Day24\Test1.txt");
            var expected = 10;
            Assert.Equal(expected, actual);
        }

        [Fact]
        private static void TestSolution2_Test()
        {
            var actual = Day24.Solution2(@".\..\..\..\..\AdventOfCode\Day24\Test1.txt");
            var expected = 2208;
            Assert.Equal(expected, actual);
        }

        [Fact]
        private static void TestSolution2()
        {
            var actual = Day24.Solution2(@".\..\..\..\..\AdventOfCode\Day24\Input.txt");
            var expected = 3804;
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, 10)]
        [InlineData(1, 15)]
        [InlineData(2, 12)]
        [InlineData(3, 25)]
        [InlineData(4, 14)]
        [InlineData(5, 23)]
        [InlineData(6, 28)]
        [InlineData(7, 41)]
        [InlineData(8, 37)]
        [InlineData(9, 49)]
        [InlineData(10, 37)]
        [InlineData(20, 132)]
        [InlineData(30, 259)]
        [InlineData(40, 406)]
        [InlineData(50, 566)]
        [InlineData(60, 788)]
        [InlineData(70, 1106)]
        [InlineData(80, 1373)]
        [InlineData(90, 1844)]
        [InlineData(100, 2208)]
        private static void TestSolution2_AfterRounds(int rounds, int expected)
        {
            HexTileMap map = InitMapWithTestInstruction(rounds);
            map.ChangeTiles(rounds);
            var actual = map.CountFlipped();
            Assert.Equal(expected, actual);
        }

        /*
Day 20: 132
Day 30: 259
Day 40: 406
Day 50: 566
Day 60: 788
Day 70: 1106
Day 80: 1373
Day 90: 1844
Day 100: 2208

         */
        private static HexTileMap InitMapWithTestInstruction(int rounds)
        {
            var instructions = Day24.ReadInput(@".\..\..\..\..\AdventOfCode\Day24\Test1.txt");
            var map = new HexTileMap(instructions.Max(x => x.Directions.Count) * 2 * (rounds/40 + 1));
            map.SetAllToWhite();
            map.InitBlackTiles(instructions);
            return map;
        }
    }
}
