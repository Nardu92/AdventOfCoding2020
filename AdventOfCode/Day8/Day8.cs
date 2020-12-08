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
            return ReadProgram().Run() ;
        }

        public static int Day8_2Solution()
        {
            return 0;
        }

        private static ProgramDay8 ReadProgram()
        {
            //bright white bags contain 5 muted tomato bags, 4 dotted gray bags, 3 posh gold bags.
            using StreamReader inputFile = new StreamReader(@".\..\..\..\Day8\Input.txt");
            string line;
            ProgramDay8 p = new ProgramDay8();
            while ((line = inputFile.ReadLine()) != null) {
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

        public ProgramDay8()
        {
            Instructions = new List<Instruction>();
            Output = 0;
            Executed = new HashSet<int>();
            InstructionPointer = 0;
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
            var instr = Instructions.ElementAt(InstructionPointer);

            switch (instr.InstructionCode)
            {
                case InstructionCode.Nop:
                    InstructionPointer++;
                    break;
                case InstructionCode.Acc:
                    Output += instr.InstructionValue;
                    break;
                case InstructionCode.Jump:
                    InstructionPointer += instr.InstructionValue;
                    break;
                default:
                    break;
            }
            return true;
        }

        public int Run()
        {
            while (ExecuteOne()) { 
            }
            return Output;
        }

    }

    public class Instruction {

        public InstructionCode InstructionCode { get; private set; }
        public int InstructionValue { get; private set; }
        public Instruction(string input)
        {
            /*
            nop +0
            acc +1*/
            var tokens = input.Split(' ');

            InstructionCode = /*ParseEnum*/"";
            InstructionValue = Convert.ToInt32(tokens[1]);
        }
    }

    public enum InstructionCode
    {
        Nop,
        Acc,
        Jump
    }

}
