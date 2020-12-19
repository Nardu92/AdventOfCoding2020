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
                total += SolveOperation(op, false);
            }
            return total;

        }

        public static long Solution2(string fileName = @".\..\..\..\Day18\Input.txt")
        {
            var lines = ReadInput(fileName, true);
            long total = 0;
            foreach (var op in lines)
            {
                total += SolveOperation(op, true);
            }
            return total;

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

        public static long SolveOperation(string operation, bool additionHasPrecedence)
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
                var parentesysResult = SolveOperation(suboperation, additionHasPrecedence);
                var newOperation = operation.Replace(suboperation, parentesysResult.ToString());
                if (newOperation == $"({suboperation})")
                {
                    return parentesysResult;
                }
                else
                {
                    return SolveOperation(newOperation, additionHasPrecedence);
                }
            }
            if (additionHasPrecedence && operation.Contains("+"))
            {
                var indexOfPlus = operation.IndexOf('+');
                var z = operation.Length;
                for (int i = indexOfPlus + 2; i < operation.Length; i++)
                {
                    if (operation[i] == ' ')
                    {
                        z = i;
                        break;
                    }
                }
                var y = 0;
                for (int i = indexOfPlus - 2; i > 0; i--)
                {
                    if (operation[i] == ' ')
                    {
                        y = i + 1 ;
                        break;
                    }
                }
                var value1String = operation[y..(indexOfPlus-1)];
                var value2String = operation[(indexOfPlus+2)..z];
                var value1 = Convert.ToInt64(value1String);
                var value2 = Convert.ToInt64(value2String);

                return SolveOperation(operation.Replace($"{value1String} + {value2String}", (value1 + value2).ToString()),additionHasPrecedence);

            }
            else
            {
                var firstPlusIndex = operation.LastIndexOf('+');
                var firstMulndex = operation.LastIndexOf('*');

                if (firstPlusIndex != -1 && (firstPlusIndex > firstMulndex || firstMulndex == -1))
                {
                    return SolveOperation(operation[..(firstPlusIndex - 1)], additionHasPrecedence) + SolveOperation(operation[(firstPlusIndex + 1)..], additionHasPrecedence);
                }

                if (firstMulndex != -1 && (firstMulndex > firstPlusIndex || firstPlusIndex == -1))
                {
                    return SolveOperation(operation[..(firstMulndex - 1)], additionHasPrecedence) * SolveOperation(operation[(firstMulndex + 1)..], additionHasPrecedence);
                }
            }
            throw new Exception("Can you get here?");
        }
    }
}
