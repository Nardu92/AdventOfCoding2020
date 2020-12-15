using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day15
    {
        public static long Solution1()
        {
            var input = "0,13,1,8,6,15";
            return Find2020(input,2020);
        }

        public static long Find2020(string input, long lastTurn)
        {
            var startingNumbers = input.Split(',').Select(x=> Convert.ToInt64(x)).ToArray();
            long turn = 1;
            var turnByNumber = new Dictionary<long, long>();
            for (; turn < startingNumbers.Length; turn++)
            {
                turnByNumber.Add(startingNumbers[turn - 1], turn );
            }
            var lastNumberSpoken = startingNumbers.Last();

            while (turn < lastTurn) {
                long turnsAgo = 0;
                if (turnByNumber.ContainsKey(lastNumberSpoken))
                {
                    turnsAgo = turn - turnByNumber[lastNumberSpoken] ;
                }

                turnByNumber[lastNumberSpoken] = turn;
                lastNumberSpoken = turnsAgo;
                turn++;
            }
            return lastNumberSpoken;
        }


        public static long Solution2()
        {
            var input = "0,13,1,8,6,15";
            return Find2020(input, 30000000);
        }
    }
}
