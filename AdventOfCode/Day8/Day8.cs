using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day8
    {
        public static int Day8_1Solution()
        {
            return ReadProgram().Run();
        }

        public static int Day8_2Solution()
        {
            ProgramDay8 p = ReadProgram(); 
            while (!p.Completed)
            {
                p.ResetAndRepair();
                p.Run();
            }
            return p.Output;
        }

        private static ProgramDay8 ReadProgram()
        {
            //bright white bags contain 5 muted tomato bags, 4 dotted gray bags, 3 posh gold bags.
            using StreamReader inputFile = new StreamReader(@".\..\..\..\Day8\Input.txt");
            string line;
            ProgramDay8 p = new ProgramDay8();
            while ((line = inputFile.ReadLine()) != null)
            {
                var instr = new Instruction(line);
                p.AddInstr(instr);
            }
            return p;
        }
    }

    public class ProgramDay8
    {
        public List<Instruction> Instructions { get; private set; }

        public int Output { get; private set; }

        private HashSet<int> Executed;

        private int InstructionPointer;

        public bool Completed { get; private set; } 

        public ProgramDay8()
        {
            Instructions = new List<Instruction>();
            Reset();
        }

        public void AddInstr(Instruction instruction)
        {
            Instructions.Add(instruction);
        }

        private bool ExecuteOne()
        {
            if (Executed.Contains(InstructionPointer))
            {
                return false;
            }

            if (InstructionPointer >= Instructions.Count())
            {
                Completed = true;
                return false;
            }

            Executed.Add(InstructionPointer);
            var instr = Instructions.ElementAt(InstructionPointer);

            switch (instr.InstructionCode)
            {
                case InstructionCode.Nop:
                    InstructionPointer++;
                    break;
                case InstructionCode.Acc:
                    Output += instr.InstructionValue;
                    InstructionPointer++;
                    break;
                case InstructionCode.Jmp:
                    InstructionPointer += instr.InstructionValue;
                    break;
                default:
                    break;
            }

            return true;
        }

        private int LastRepaired = -1;

        public int Repair(int toRepair)
        {
            var instr = Instructions.ElementAt(toRepair);
            switch (instr.InstructionCode)
            {
                case InstructionCode.Nop:
                    instr.InstructionCode = InstructionCode.Jmp;
                    return toRepair;
                case InstructionCode.Acc:
                    return Repair(toRepair + 1);
                case InstructionCode.Jmp:
                    instr.InstructionCode = InstructionCode.Nop;
                    return toRepair;
                default:
                    return -1;
            }
        }

        private void Restore()
        {
            var instr = Instructions.ElementAt(LastRepaired);
            switch (instr.InstructionCode)
            {
                case InstructionCode.Nop:
                    instr.InstructionCode = InstructionCode.Jmp;
                    break;
                case InstructionCode.Acc:
                    break;
                case InstructionCode.Jmp:
                    instr.InstructionCode = InstructionCode.Nop;
                    break;
                default:
                    break;
            }
        }

        public void Reset()
        {
            Output = 0;
            Executed = new HashSet<int>();
            InstructionPointer = 0;
        }

        public void ResetAndRepair()
        {
            Reset();
            Repair();
        }
        private void Repair()
        {
            if (LastRepaired != -1)
            {
                Restore();
            }
            LastRepaired = Repair(LastRepaired + 1);
        }

        public int Run()
        {
            while (ExecuteOne())
            {
            }
            return Output;
        }

    }

    public class Instruction
    {

        public InstructionCode InstructionCode { get; set; }
        public int InstructionValue { get; private set; }
        public Instruction(string input)
        {
            /*
            nop +0
            acc +1*/
            var tokens = input.Split(' ');

            InstructionCode = (InstructionCode)Enum.Parse(typeof(InstructionCode), tokens[0].ToString(), true);
            InstructionValue = Convert.ToInt32(tokens[1]);
        }
    }

    public enum InstructionCode
    {
        Nop,
        Acc,
        Jmp
    }

}
