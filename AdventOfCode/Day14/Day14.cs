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
        public static long Solution1(string fileName = @".\..\..\..\Day14\Input.txt")
        {
            var l = new MaskProgram(ReadInput(fileName));

            l.ExecuteProgram();

            return l.CalculateTotalValues();
        }


        public static long Solution2()
        {
            return 0;
        }

        private static List<string> ReadInput(string fileName)
        {
            using StreamReader inputFile = new StreamReader(fileName);
            string line;
            List<string> lines = new List<string>();
            while ((line = inputFile.ReadLine()) != null)
            {
                lines.Add(line);
            }
            return lines;
        }

    }

    public class MaskProgram
    {
        public List<ProgramSection> ProgramSections { get; private set; }
        Dictionary<long, long> ValuesByMemoryAddress;
        public  MaskProgram(List<string> input)
        {
            ProgramSections = new List<ProgramSection>();
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
                    ProgramSections.Add(programSection);
                }
            }
        }

        public void ExecuteProgram()
        {
            ValuesByMemoryAddress = new Dictionary<long, long>();
            foreach (var program in ProgramSections)
            {
                
                var partialMem = program.ApplyChanges();
                foreach (var item in partialMem)
                {
                    ValuesByMemoryAddress[item.Key] = item.Value;
                }
            }
        }

        public long CalculateTotalValues()
        {
            return ValuesByMemoryAddress.Values.Sum();
        }
    }
    public class Mask
    {
        private Dictionary<int, bool> Bits;

        public Mask(string maskValue)
        {
            Bits = new Dictionary<int, bool>();
            for (int i = maskValue.Length - 1; i >= 0; i--)
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
            for (int i = binarystring.Length - 1; i >= 0 ; i--)
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
        public long MemoryAddress;
        public long ValueToSet;
        public MemoryAccess(long memoryAddress, long valueToSet)
        {
            this.MemoryAddress = memoryAddress;
            this.ValueToSet = valueToSet;
        }

        public MemoryAccess(string input)
        {
            //mem[22316] = 982
            var lastIndexOfMA = input.IndexOf(']');
            var firsIndexOfValue = input.IndexOf('=') + 2;
            this.MemoryAddress = Convert.ToInt64(input[4..lastIndexOfMA]);
            this.ValueToSet = Convert.ToInt64(input[firsIndexOfValue..]);
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

        public Dictionary<long, long> ApplyChanges()
        {
            Dictionary<long, long> valueByMemoryAddress = new Dictionary<long, long>();
            foreach (var mem in MemoryAccesses)
            {
                valueByMemoryAddress[Convert.ToInt64(mem.MemoryAddress)] = Convert.ToInt64(Mask.ApplyMask(mem.ValueToSet));
            }
            return valueByMemoryAddress;
        }

    }
}
