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
        private static void TestMask5()
        {
            var stringMask = "mask = X11001110001101XX01111X1001X01101111";
            Mask m = new Mask(stringMask);
            var actual = m.ApplyMask(0);
            var expected = Convert.ToInt64("X11001110001101XX01111X1001X01101111".Replace('X', '0'), 2);
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
        private static void TestFirstProgramSection()
        {
            List<string> input = new List<string>(){
                "mask = X11001110001101XX01111X1001X01101111",
                "mem[32163] = 23587",
                "mem[59015] = 3487205",
                "mem[25831] = 33360",
                "mem[62711] = 224797",
                "mem[41307] = 1818"
            };
            var mask = new Mask("mask = X11001110001101XX01111X1001X01101111");
            //mask    X11001110001101XX01111X1001X01101111
            //23587   000000000000000000000101110000100011
            //masked  011001110001101000111101001001101111
            var mem = new MemoryAccess("mem[32163] = 23587");
            Assert.Equal(27676365423, mask.ApplyMask(mem.ValueToSet));

            //mask    X11001110001101XX01111X1001X01101111
            //3487205 000000000000001101010011010111100101
            //masked  011001110001101100111111001101101111
            mem = new MemoryAccess("mem[59015] = 3487205");
            Assert.Equal(27677422447, mask.ApplyMask(mem.ValueToSet));

            //mask    X11001110001101XX01111X1001X01101111
            //33360   000000000000000000001000001001010000
            //masked  011001110001101000111101001001101111
            mem = new MemoryAccess("mem[25831] = 33360");
            Assert.Equal(27676365423, mask.ApplyMask(mem.ValueToSet));

            //mask    X11001110001101XX01111X1001X01101111
            //224797  000000000000000000110110111000011101
            //masked  011001110001101000111111001001101111
            mem = new MemoryAccess("mem[62711] = 224797");
            Assert.Equal(27676373615, mask.ApplyMask(mem.ValueToSet));

            //mask    X11001110001101XX01111X1001X01101111
            //1818    000000000000000000000000011100011010
            //masked  011001110001101000111101001101101111
            mem = new MemoryAccess("mem[41307] = 1818");
            Assert.Equal(27676365679, mask.ApplyMask(mem.ValueToSet));
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
