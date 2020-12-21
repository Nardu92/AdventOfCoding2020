using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public class Day21
    {
        public static long Solution1(string fileName = @".\..\..\..\Day21\Input.txt")
        {
            var foods = ReadInput(fileName);
            Dictionary<string, string> ingredientByAllergen = GetIngredientByAllergen(foods);

            long total = 0;
            foreach (var food in foods)
            {
                total += food.Ingredients.Count(x => !ingredientByAllergen.Values.Contains(x));
            }
            return total;

        }

        private static Dictionary<string, string> GetIngredientByAllergen(List<Food> foods)
        {
            var allAllergens = foods.SelectMany(x => x.Allergens).ToHashSet();

            var ingredientsByAllergen = new Dictionary<string, HashSet<string>>();
            foreach (var allergen in allAllergens)
            {
                foreach (var food in foods)
                {
                    if (food.Allergens.Contains(allergen))
                    {
                        if (!ingredientsByAllergen.TryGetValue(allergen, out _))
                        {
                            ingredientsByAllergen.Add(allergen, food.Ingredients.ToHashSet());

                        }
                        ingredientsByAllergen[allergen].IntersectWith(food.Ingredients);
                    }
                }
            }

            var ingredientByAllergen = new Dictionary<string, string>();
            while (ingredientsByAllergen.Count > 0)
            {
                foreach (var kvp in ingredientsByAllergen.Where(x => x.Value.Count == 1))
                {
                    ingredientByAllergen.Add(kvp.Key, kvp.Value.First());
                }

                foreach (var item in ingredientByAllergen)
                {
                    ingredientsByAllergen.Remove(item.Key);
                }

                foreach (var item in ingredientsByAllergen)
                {
                    item.Value.ExceptWith(ingredientByAllergen.Values);
                }
            }

            return ingredientByAllergen;
        }

        public static string Solution2(string fileName = @".\..\..\..\Day21\Input.txt")
        {
            var foods = ReadInput(fileName);
            Dictionary<string, string> ingredientByAllergen = GetIngredientByAllergen(foods);

            string answer = "";
            foreach (var allergen in ingredientByAllergen.Keys.OrderBy(x=>x))
            {
                answer += ingredientByAllergen[allergen] + ",";
            }
            return answer[..^1];
        }


        public static List<Food> ReadInput(string fileName)
        {
            List<Food> foods = new List<Food>();
            using StreamReader inputFile = new StreamReader(fileName);
            string line;
            while (!string.IsNullOrEmpty(line = inputFile.ReadLine()))
            {
                foods.Add(new Food(line));
            }
            return foods;
        }


    }

    public class Food
    {
        public List<string> Ingredients { get; private set; }
        public List<string> Allergens { get; private set; }
        public Food(List<string> ingredients, List<string> allergens)
        {
            Ingredients = ingredients;
            Allergens = allergens;
        }
        public Food(string input)
        {
            //mxmxvkd kfcds sqjhc nhms (contains dairy, fish)
            var tokens = input.Split(" (contains ");
            Ingredients = tokens[0].Split(' ').ToList();
            Allergens = tokens[1][..^1].Split(", ").ToList();

        }
    }
}
