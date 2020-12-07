using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Day7
    {
        public static int Day7_1Solution()
        {
            var rules = CreateRules();
            return CountOuterBags(rules["shiny gold"], new HashSet<Bag>()) - 1;
        }

        public static int Day7_2Solution()
        {

            var rules = CreateRules();
            return CountInnerBags(rules["shiny gold"]);
        }

        public static int CountOuterBags(Bag bag, HashSet<Bag> visited)
        {
            if (!bag.Parents.Any()) return 1;
            var total = 0;
            foreach (var b in bag.Parents)
            {
                if (!visited.Contains(b))
                {
                    visited.Add(b);
                    total += CountOuterBags(b, visited);
                }
            }
            return total;
        }

        public static int CountInnerBags(Bag bag)
        {
            if (!bag.InnerBags.Any()) return 0;
            var total = 0;
            foreach (var tuple in bag.InnerBags)
            {
                var b = tuple.Item1;
                var count = tuple.Item2;
                total += (CountInnerBags(b) + 1) * count;
            }
            return total;
        }

        private static Dictionary<string, Bag> CreateRules()
        {
            Dictionary<string, Bag> rules = new Dictionary<string, Bag>();
            //bright white bags contain 5 muted tomato bags, 4 dotted gray bags, 3 posh gold bags.
            using StreamReader inputFile = new StreamReader(@".\..\..\..\Day7\Input.txt");
            string line;
            while ((line = inputFile.ReadLine()) != null)
            {
                var tokens = line[0..^1].Split("contain");
                var parentToken = tokens[0][0..^6];
                var innerBagsToken = tokens[1].Split(',').Select(x => x.Trim()).Select(x => { if (x.StartsWith("1")) { return x[0..^4]; } else { return x[0..^5]; } }).ToList();
                Bag b;
                if (rules.TryGetValue(parentToken, out Bag value))
                {
                    b = value;
                    b.AddInnerBags(innerBagsToken, rules);
                }
                else
                {
                    b = new Bag(parentToken, innerBagsToken, rules);
                    rules.Add(b.Name, b);
                }
                foreach (var tuple in b.InnerBags)
                {
                    Bag bag = tuple.Item1;
                    rules.TryAdd(bag.Name, bag);
                }
            }
            return rules;
        }
    }

    public class Bag
    {
        public List<Bag> Parents;

        public List<Tuple<Bag, int>> InnerBags;

        public string Name { get; private set; }
        public Bag(string bagName, List<string> innerbags, Dictionary<string, Bag> rules)
        {
            Name = bagName;
            Parents = new List<Bag>();
            InnerBags = new List<Tuple<Bag, int>>();
            AddInnerBags(innerbags, rules);
        }

        public void AddParent(Bag parent)
        {
            Parents.Add(parent);
        }

        internal void AddInnerBags(List<string> innerbags, Dictionary<string, Bag> rules)
        {
            if (innerbags != null && !(innerbags.Count == 1 && innerbags.First() == "no other"))
            {
                foreach (var b in innerbags)
                {
                    var innerBagName = b[2..];
                    Bag innerBag;
                    if (rules.TryGetValue(innerBagName, out Bag existingBag))
                    {
                        innerBag = existingBag;
                    }
                    else
                    {
                        innerBag = new Bag(innerBagName, null, rules);
                    }
                    InnerBags.Add(new Tuple<Bag, int>(innerBag, Convert.ToInt32(b[0].ToString())));
                    innerBag.AddParent(this);
                }
            }
        }
    }
}
