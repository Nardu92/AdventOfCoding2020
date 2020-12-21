using AdventOfCode;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace AdventOfCode2020_Tests
{
    public class Day21_Tests
    {

        [Fact]
        private static void TestSolution1()
        {
            var actual = Day21.Solution1(@".\..\..\..\..\AdventOfCode\Day21\Input.txt");
            var expected = 2203;
            Assert.Equal(expected, actual);
        }

        [Fact]
        private static void TestSolution2()
        {
            var actual = Day21.Solution2(@".\..\..\..\..\AdventOfCode\Day21\Input.txt");
            var expected = "fqfm,kxjttzg,ldm,mnzbc,zjmdst,ndvrq,fkjmz,kjkrm";
            Assert.Equal(expected, actual);
        }
        
    }
}
