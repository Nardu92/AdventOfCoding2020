using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public class Day23
    {
        public static string Solution1(string input = "942387615")
        {
            var cupIds = ReadInput(input);
            var cups = GetCups(cupIds);
            var rounds = 100;
            var cup1 = PlayTheCrabGame(cups, rounds);
            var looped2 = false;
            var next2 = cup1.NextCup;
            var sb = new StringBuilder();
            while (!looped2)
            {
                sb.Append(next2.Value);
                next2 = next2.NextCup;
                if (next2.Value == cup1.Value)
                {
                    looped2 = true;
                }
            }
            var result = sb.ToString();
            return result;
        }

        public static Cup PlayTheCrabGame(Cup[] cups, int rounds)
        {
            var cupsById = cups.ToDictionary(x => x.Value, x => x);
            var currentCup = cups[0];
            var minValue = cups.Min(x => x.Value);
            var maxValue = cups.Max(x => x.Value);

            for (int i = 0; i < rounds; i++)
            {
                //find the cups to move
                var value = currentCup.Value;
                /* Debug
                Console.Write($"Cups : ({value}) ");
                var looped = false;
                Cup next = currentCup.NextCup;
                while (!looped)
                {
                    Console.Write($"{next.Value} ");
                    next = next.NextCup;
                    if (next.Value == value)
                    {
                        looped = true;
                    }
                }
                Console.WriteLine("");
                */
                var firstToMove = currentCup.NextCup;
                var lastToMove = currentCup.NextCup;
                HashSet<int> idsOfMovedCups = new HashSet<int>();
                idsOfMovedCups.Add(lastToMove.Value);
                var cupsToPicUp = 3;
                for (int j = 0; j < cupsToPicUp - 1; j++)
                {
                    lastToMove = lastToMove.NextCup;
                    idsOfMovedCups.Add(lastToMove.Value);
                }
                //Console.WriteLine($"pick up: {string.Join(' ', idsOfMovedCups)}");
                //remove the moved from the circle
                currentCup.NextCup = lastToMove.NextCup;

                //chose destination
                Cup destination = null;
                var destinationValue = currentCup.Value;
                do
                {
                    destinationValue--;
                    if (destinationValue < minValue)
                    {
                        destinationValue = maxValue;
                    }
                    destination = cupsById[destinationValue];
                } while (idsOfMovedCups.Contains(destination.Value));
                //Console.WriteLine($"destination: {destination.Value}");
                //insert the moved in the circle
                lastToMove.NextCup = destination.NextCup;
                destination.NextCup = firstToMove;
                currentCup = currentCup.NextCup;
            }
            var cup1 = cupsById[1];
            return cup1;
        }

        public static long Solution2(string input = "942387615")
        {
            var cupsIds = ReadInput(input);

            var maxValue = cupsIds.Max();
            var allTheIds = new List<int>();
            allTheIds.AddRange(cupsIds);
            var howManyCups = 1000000;
            for (int i = maxValue + 1; i <= howManyCups; i++)
            {
                allTheIds.Add(i);
            }

            var rounds = 10000000;
            var cups = GetCups(allTheIds.ToArray());
            var cup1 = PlayTheCrabGame(cups, rounds);
            return (long)cup1.NextCup.Value * cup1.NextCup.NextCup.Value;
        }


        public static int[] ReadInput(string input)
        {
            var n = input.Length;
            var cupsIds = new int[n];
            for (int i = 0; i < n; i++)
            {
                cupsIds[i] = Convert.ToInt32(input[i].ToString());
            }
            return cupsIds;
        }

        public static Cup[] GetCups(int[] cupsIds) {
            var n = cupsIds.Length;
            Cup[] cups = new Cup[n];
            Cup nextCup = null;
            for (int i = n - 1; i >= 0; i--)
            {
                if (i + 1 < n)
                {
                    nextCup = cups[i + 1];
                }
                cups[i] = new Cup(nextCup, cupsIds[i]);
            }
            cups[n - 1].NextCup = cups[0];
            return cups;
        }
    }

    public class Cup
    {
        public Cup NextCup { get; set; }
        public int Value { get; private set; }

        public Cup(Cup nextCup, int value)
        {
            NextCup = nextCup;
            Value = value;
        }
    }
}
