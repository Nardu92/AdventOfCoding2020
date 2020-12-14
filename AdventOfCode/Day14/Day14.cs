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
            var prog = new MaskProgram(ReadInput(fileName), true);
            prog.ExecuteProgram();
            return prog.CalculateTotalValues();
        }


        public static long Solution2(string fileName = @".\..\..\..\Day14\Input.txt")
        {
            var prog = new MaskProgram(ReadInput(fileName), false);
            prog.ExecuteProgram();
            return prog.CalculateTotalValues();
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
        public List<IProgramSection> ProgramSections { get; private set; }
        Dictionary<long, long> ValuesByMemoryAddress;
        public MaskProgram(List<string> input, bool sol1)
        {
            ProgramSections = new List<IProgramSection>();
            IProgramSection programSection = null;
            foreach (var str in input)
            {
                if (str.Contains('['))
                {
                    programSection.AddMemory(new MemoryAccess(str));
                }
                else
                {
                    if (sol1)
                    {
                        Mask mask = new Mask(str);
                        programSection = new ProgramSection(mask);
                    }
                    else
                    {
                        var memoryMask = new MemoryMask(str);
                        programSection = new ProgramSectionAddressMask(memoryMask);
                    }
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
                }
                else if (c == '0')
                {
                    Bits[maskValue.Length - i - 1] = false;
                }
                else if (c == '1')
                {
                    Bits[maskValue.Length - i - 1] = true;
                }
            }
        }

        public long ApplyMask(long value)
        {
            List<char> reversedNewString = new List<char>();
            var binarystring = Convert.ToString(value, toBase: 2).PadLeft(36, '0');
            for (int i = binarystring.Length - 1; i >= 0; i--)
            {
                if (Bits.TryGetValue(binarystring.Length - i - 1, out bool maskbit))
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

    public class MemoryMask
    {
        private Dictionary<int, bool> Bits;

        public MemoryMask(string maskValue)
        {
            Bits = new Dictionary<int, bool>();
            for (int i = maskValue.Length - 1; i >= 0; i--)
            {
                char c = maskValue[i];
                if (c == '0')
                {
                    continue;
                }
                else if (c == 'X')
                {
                    Bits[maskValue.Length - i - 1] = false;
                }
                else if (c == '1')
                {
                    Bits[maskValue.Length - i - 1] = true;
                }
            }
        }

        public List<long> ApplyMask(long value)
        {
            List<char> reversedNewString = new List<char>();
            var binarystring = Convert.ToString(value, toBase: 2).PadLeft(36, '0');
            for (int i = binarystring.Length - 1; i >= 0; i--)
            {
                if (Bits.TryGetValue(binarystring.Length - i - 1, out bool maskbit))
                {
                    reversedNewString.Add(maskbit ? '1' : 'X');
                }
                else
                {
                    reversedNewString.Add(binarystring[i]);
                }
            }
            var sb = new StringBuilder();
            reversedNewString.Reverse();
            reversedNewString.ForEach(x => sb.Append(x));
            return TransformFloatingAddress(sb.ToString());
        }

        public List<long> TransformFloatingAddress(string floatingAddress)
        {
            if (!floatingAddress.Contains("X"))
                return new List<long>() { Convert.ToInt64(floatingAddress, 2) };

            var index = floatingAddress.IndexOf("X");

            string withOne = floatingAddress[..index] + "1" + floatingAddress[(index + 1)..];
            string withZero = floatingAddress[..index] + "0" + floatingAddress[(index + 1)..];

            var result = TransformFloatingAddress(withOne);
            result.AddRange(TransformFloatingAddress(withZero));
            return result;
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

    public interface IProgramSection
    {
        void AddMemory(MemoryAccess ma);
        Dictionary<long, long> ApplyChanges();
    }

    public class ProgramSection : IProgramSection
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
                valueByMemoryAddress[mem.MemoryAddress] = Mask.ApplyMask(mem.ValueToSet);
            }
            return valueByMemoryAddress;
        }

    }

    public class ProgramSectionAddressMask : IProgramSection
    {
        List<MemoryAccess> MemoryAccesses;
        MemoryMask Mask;

        public ProgramSectionAddressMask(MemoryMask mask)
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
                var addresses = Mask.ApplyMask(mem.MemoryAddress);
                foreach (var address in addresses)
                {
                    valueByMemoryAddress[address] = mem.ValueToSet;
                }
            }
            return valueByMemoryAddress;
        }

    }
}
