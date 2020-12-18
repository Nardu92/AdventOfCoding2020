using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public class Day18
    {
        public static long Solution1(string fileName = @".\..\..\..\Day18\Input.txt")
        {
            var lines = ReadInput(fileName, true);
            long total = 0;
            foreach (var op in lines)
            {
                total += SolveOperation(op);
            }
            return total;
            
        }

        public static long Solution2(string fileName = @".\..\..\..\Day18\Input.txt")
        {
            var lines = ReadInput(fileName, true);
            var space = new Space4D(lines.ToArray(), false);

            for (int i = 0; i < 6; i++)
            {
                space.CalculateRound();
            }

            return space.GetActiveCubes();
        }


        private static List<string> ReadInput(string fileName, bool rule1)
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

        public static long SolveOperation(string operation)
        {
            if (!operation.Contains("(") && !operation.Contains('+') && !operation.Contains('*'))
                return Convert.ToInt64(operation);

            if (operation.Contains("("))
            {
                var firstOpenParentesys = operation.IndexOf('(');
                var closedParentesys = operation.LastIndexOf(')') + 1;
                int level = 0;
                for (int i = firstOpenParentesys + 1; i < closedParentesys - 1; i++)
                {
                    if (operation[i] == '(')
                    {
                        level++;
                    }
                    if (operation[i] == ')')
                    {
                        if (level == 0)
                        {
                            closedParentesys = i + 1;
                            break;
                        }
                        level--;
                    }
                }

                if (firstOpenParentesys == 0 && closedParentesys == operation.Length)
                {
                    firstOpenParentesys++;
                    closedParentesys--;
                }

                string suboperation = operation[(firstOpenParentesys)..(closedParentesys)];
                var parentesysResult = SolveOperation(suboperation);
                var newOperation = operation.Replace(suboperation, parentesysResult.ToString());
                if (newOperation == $"({suboperation})")
                {
                    return parentesysResult;
                }
                else
                {
                    return SolveOperation(newOperation);
                }
            }
            var firstPlusIndex = operation.LastIndexOf('+');
            var firstMulndex = operation.LastIndexOf('*');

            if (firstPlusIndex != -1 && (firstPlusIndex > firstMulndex || firstMulndex == -1))
            {
                return SolveOperation(operation[..(firstPlusIndex - 1)]) + SolveOperation(operation[(firstPlusIndex + 1)..]);
            }

            if (firstMulndex != -1 && (firstMulndex > firstPlusIndex || firstPlusIndex == -1))
            {
                return SolveOperation(operation[..(firstMulndex - 1)]) * SolveOperation(operation[(firstMulndex + 1)..]);
            }

            throw new Exception("Can you get here?");

        }

    }
}
