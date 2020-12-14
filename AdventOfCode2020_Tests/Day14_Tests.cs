using AdventOfCode;
using System;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCode2020_Tests
{
    public class Day14_Tests
    {
        [Fact]
        private static void TestMask1()
        {
            Mask m = new Mask("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X");
            var actual = m.ApplyMask(11);
            var expected = 73;
            Assert.Equal(expected, actual);
        }

        [Fact]
        private static void TestMask2()
        {
            Mask m = new Mask("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X");
            var actual = m.ApplyMask(101);
            var expected = 101;
            Assert.Equal(expected, actual);
        }

        [Fact]
        private static void TestMask3()
        {
            Mask m = new Mask("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X");
            var actual = m.ApplyMask(0);
            var expected = 64;
            Assert.Equal(expected, actual);
        }

        [Fact]
        private static void TestMask4()
        {
            var stringMask = "X11001110001101XX01111X1001X01101111";
            Mask m = new Mask(stringMask);
            var actual = m.ApplyMask(0);
            var expected = Convert.ToInt64(stringMask.Replace('X', '0'), 2);
            Assert.Equal(expected, actual);
        }

        [Fact]
        private static void TestCreateProgramSections()
        {
            List<string> input = new List<string>(){
                "mask = X11001110001101XX01111X1001X01101111",
                "mem[32163] = 23587",
                "mem[59015] = 3487205",
                "mem[25831] = 33360",
                "mem[62711] = 224797",
                "mem[41307] = 1818" 
            };

            var maskProgram = new MaskProgram(input);
            Assert.Single(maskProgram.ProgramSections);
        }

        [Fact]
        private static void TestCreateProgramSections2()
        {
            List<string> input = new List<string>(){
                "mask = X11001110001101XX01111X1001X01101111",
                "mem[32163] = 23587",
                "mask = X11001110001101XX01111X1001X01101111",
                "mem[59015] = 3487205",
                "mask = X11001110001101XX01111X1001X01101111",
                "mem[25831] = 33360",
                "mask = X11001110001101XX01111X1001X01101111",
                "mem[62711] = 224797",
                "mask = X11001110001101XX01111X1001X01101111",
                "mem[41307] = 1818" 
            };


            var maskProgram = new MaskProgram(input);
            Assert.Equal(5, maskProgram.ProgramSections.Count);
        }

        [Fact]
        private static void TestMemoryAddressDeserialization()
        {
            MemoryAccess m = new MemoryAccess("mem[22316] = 982");
            Assert.Equal(982, m.ValueToSet);
            Assert.Equal(22316, m.MemoryAddress);
        }

    }
}
