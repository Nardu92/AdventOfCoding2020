using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day13
    {
        public static long Solution1()
        {
            var lines = ReadInput();
            var earliest = Convert.ToInt32(lines.First());
            var s = Schedule(earliest, lines.Last());
            var actualDepartureTime = s.Keys.Min();
            var earliestId = s[actualDepartureTime];
            var waiting = actualDepartureTime - earliest;
            return waiting * earliestId;
        }

        public static long Solution2()
        {
            //Ugliest code ever? 
            // I'll change it to use a nicer algorithm
            var line = ReadInput().Last();
            var myDT = GetIds(line);
            var high = myDT.Max(x => x.Item1);
            var pos = myDT.Single(x => x.Item1 == high).Item2;
            long t = high - pos;
            return CheckUntilFound(myDT, high, t);
        }

        private static long CheckUntilFound(List<Tuple<int, int>> myDT, int high, long startT)
        {
            List<Task<long>> list = new List<Task<long>>();
            for (int i = 1; i <= 8; i++)
            {
                var a = i;
                list.Add(Task.Run(() => { return Single(myDT, high, startT + high * a); }));
            }
            var tasksArray = list.ToArray();
            int index = Task.WaitAny(tasksArray);

            return tasksArray[index].Result;
        }

        private static long Single(List<Tuple<int, int>> myDT, int high, long startT)
        {
            var found = false;
            while (!found)
            {
                startT += high * 8;
                found = IsPerfectTime(startT, myDT);
            }
            return startT;
        }

        private static bool IsPerfectTime(long time, List<Tuple<int, int>> busses)
        {
            foreach (var bus in busses)
            {
                if (!((time + bus.Item2) % bus.Item1 == 0))
                {
                    return false;
                }
            }
            return true;
        }


        private static List<Tuple<int, int>> GetIds(string line)
        {
            var result = new List<Tuple<int, int>>();
            var position = 0;
            foreach (var i in line.Split(','))
            {
                if (i != "x")
                {
                    result.Add(new Tuple<int, int>(Convert.ToInt32(i), position));
                }
                position++;
            }
            return result;
        }


        private static List<string> ReadInput()
        {
            using StreamReader inputFile = new StreamReader(@".\..\..\..\Day13\Input.txt");
            string line;
            List<string> lines = new List<string>();
            while ((line = inputFile.ReadLine()) != null)
            {
                lines.Add(line);
            }
            return lines;
        }

        public static Dictionary<int, int> Schedule(int earliestDepartureTime, string ids)
        {
            Dictionary<int, int> timesAfter = new Dictionary<int, int>();
            var intIds = ids.Split(',').Where(x => x != "x").Select(x => Convert.ToInt32(x));
            foreach (var id in intIds)
            {
                int multiplier = 1;
                var departureTime = id;
                while (departureTime < earliestDepartureTime)
                {
                    departureTime = id * multiplier;
                    multiplier++;
                }
                timesAfter.TryAdd(departureTime, id);
            }
            return timesAfter;
        }
    }
}
