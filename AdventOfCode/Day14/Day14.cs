using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day14
    {
        public static long Solution1()
        {
            var l = CreateProgramSections(ReadInput());

            var res = ExecuteProgram(l);

            return CalculateTotalValues(res);
        }


        public static long Solution2()
        {
            return 0;
        }

        private static List<string> ReadInput()
        {
            using StreamReader inputFile = new StreamReader(@".\..\..\..\Day14\Input.txt");
            string line;
            List<string> lines = new List<string>();
            while ((line = inputFile.ReadLine()) != null)
            {
                lines.Add(line);
            }
            return lines;
        }

        private static List<ProgramSection> CreateProgramSections(List<string> input)
        {
            List<ProgramSection> programSections = new List<ProgramSection>();
            Mask mask;
            ProgramSection programSection = null;
            foreach (var str in input)
            {
                if (str.Contains('['))
                {
                    programSection.AddMemory(new MemoryAccess(str));
                }
                else
                {
                    mask = new Mask(str);
                    programSection = new ProgramSection(mask);
                    programSections.Add(programSection);
                }
            }
            return programSections;
        }

        private static Dictionary<int, long> ExecuteProgram(List<ProgramSection> programSections)
        {
            Dictionary<int, long> valuesByMemoryAddress = new Dictionary<int, long>();
            foreach (var program in programSections)
            {
                var partialMem = program.ApplyChanges();
                foreach (var item in partialMem)
                {
                    valuesByMemoryAddress[item.Key] = item.Value;
                }
            }
            return valuesByMemoryAddress;
        }

        private static long CalculateTotalValues(Dictionary<int, long> valuesByMemoryAddress)
        {
            return valuesByMemoryAddress.Sum(x => x.Value);
        }

        private static void TestMask1()
        {
            Mask m = new Mask("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X");
            var actual = m.ApplyMask(11);
            var expected = 73;
            if (actual != expected)
            {
                throw new Exception();
            }
        }

        private static void TestMask2()
        {
            Mask m = new Mask("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X");
            var actual = m.ApplyMask(101);
            var expected = 101;
            if (actual != expected)
            {
                throw new Exception();
            }
        }

        private static void TestMask3()
        {
            Mask m = new Mask("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X");
            var actual = m.ApplyMask(0);
            var expected = 64;
            if (actual != expected)
            {
                throw new Exception();
            }
        }

        private static void TestMemoryAddress1()
        {
            MemoryAccess m = new MemoryAccess("mem[22316] = 982");
            if (m.ValueToSet != 982)
            {
                throw new Exception();
            }
            if (m.MemoryAddress != 22316)
            {
                throw new Exception();
            }
        }

    }

    public class Mask
    {
        private Dictionary<int, bool> Bits;

        public Mask(string maskValue)
        {
            Bits = new Dictionary<int, bool>();
            for (int i = maskValue.Length - 1; i > 0; i--)
            {
                char c = maskValue[i];
                if (c == 'X')
                {
                    continue;
                }else if(c == '0')
                {
                    Bits[maskValue.Length - i - 1] = false;
                } else if(c == '1')
                {
                    Bits[maskValue.Length - i - 1] = true;
                }
            }
        }

        public long ApplyMask(long value)
        {
            List<char> reversedNewString = new List<char>();
            var binarystring  = Convert.ToString(value, toBase: 2).PadLeft(36, '0');
            for (int i = binarystring.Length - 1; i > 0 ; i--)
            {
                if(Bits.TryGetValue(binarystring.Length - i - 1, out bool maskbit))
                {
                    reversedNewString.Add(maskbit ? '1' : '0');
                }
                else
                {
                    reversedNewString.Add(binarystring[i]);
                }
            }
            var sb = new StringBuilder();
            reversedNewString.Reverse();
            reversedNewString.ForEach(x => sb.Append(x));
            return Convert.ToInt64(sb.ToString(), fromBase: 2);
        }
    }

    public class MemoryAccess
    {
        public int MemoryAddress;
        public long ValueToSet;
        public MemoryAccess(int memoryAddress, long valueToSet)
        {
            this.MemoryAddress = memoryAddress;
            this.ValueToSet = valueToSet;
        }

        public MemoryAccess(string input)
        {
            //mem[22316] = 982
            var lastIndexOfMA = input.IndexOf(']');
            var firsIndexOfValue = input.IndexOf('=') + 2;
            this.MemoryAddress = Convert.ToInt32(input[4..lastIndexOfMA]);
            this.ValueToSet = Convert.ToInt32(input[firsIndexOfValue..]);
        }
    }

    public class ProgramSection
    {
        List<MemoryAccess> MemoryAccesses;
        Mask Mask;

        public ProgramSection(Mask mask, List<MemoryAccess> memoryAccesses)
        {
            this.MemoryAccesses = memoryAccesses;
            this.Mask = mask;
        }

        public ProgramSection(Mask mask)
        {
            this.MemoryAccesses = new List<MemoryAccess>();
            this.Mask = mask;
        }

        public void AddMemory(MemoryAccess ma)
        {
            this.MemoryAccesses.Add(ma);
        }

        public Dictionary<int, long> ApplyChanges()
        {
            Dictionary<int, long> valueByMemoryAddress = new Dictionary<int, long>();
            foreach (var mem in MemoryAccesses)
            {
                valueByMemoryAddress[mem.MemoryAddress] = Mask.ApplyMask(mem.ValueToSet);
            }
            return valueByMemoryAddress;
        }

    }
}
