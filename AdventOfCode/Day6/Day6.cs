using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day6
    {
        public static int Day6_1Solution()
        {
            return CreateSet(false).Sum(x => x.Count);
        }

        public static int Day6_2Solution()
        {

            return CreateSet(true).Sum(x => x.Count);
        }

        private static HashSet<char> ConvertToSet(string line)
        {
            return line.ToCharArray().ToHashSet();
        }

        private static List<HashSet<char>> CreateSet(bool withIntersection)
        {
            List<HashSet<char>> groups = new List<HashSet<char>>();
            StreamReader inputFile = new StreamReader(@".\..\..\..\Day6\Input6.txt");
            HashSet<char> value;
            string line;
            while ((line = inputFile.ReadLine()) != null)
            {
                var group = new HashSet<char>();
                groups.Add(group);
                if (withIntersection)
                {
                    group.UnionWith(ConvertToSet(line));
                }
                while (!string.IsNullOrEmpty(line))
                {
                    value = ConvertToSet(line);
                    if (withIntersection)
                    {
                        group.IntersectWith(value);
                    }
                    else 
                    { 
                        group.UnionWith(value);
                    }
                    line = inputFile.ReadLine();
                }
            }
            return groups;
        }
    }
}
