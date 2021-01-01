using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day21
    {
        public static string _input = @"mxmxvkd kfcds sqjhc nhms (contains dairy, fish)
trh fvjkl sbzzf mxmxvkd (contains dairy)
sqjhc fvjkl (contains soy)
sqjhc mxmxvkd sbzzf (contains fish)";

        public static List<string> _allergens = new List<string>();
        public static Dictionary<string, string> _allergenNameDict = new Dictionary<string, string>();
        public static Dictionary<string, List<List<string>>> _ingredientAllergenDict = new Dictionary<string, List<List<string>>>();
        public static List<List<string>> _ingredientLists = new List<List<string>>();

        public static string Run(string input)
        {
            //input = _input;
            ParseInput(input);
            SetAllergenDict();

            /* Part 1 */
            //var nonAllergenIngredients = from ingredientList in _ingredientLists
            //                             from ingredient in ingredientList
            //                             where !_allergenNameDict.ContainsValue(ingredient)
            //                             select ingredient;


            //return nonAllergenIngredients.Count(); //Part 1 answer

            /* Part 2 */
            var answerBuilder = new StringBuilder();
            var keys = _allergenNameDict.Keys.ToList();
            keys.Sort();
            foreach(var key in keys)
            {
                answerBuilder.Append(_allergenNameDict[key] + ",");
            }
            answerBuilder = answerBuilder.Remove(answerBuilder.Length - 1, 1);
            return answerBuilder.ToString();
        }

        public static void ParseInput(string input)
        {
            var lines = InputUtils.SplitLinesIntoStringArray(input);
            foreach(var line in lines)
            {
                var tokens = StringUtils.SplitInOrder(line, new string[] { "(contains " });
                var allergens = tokens[1].Remove(tokens[1].Length - 1).Split( ", " ).ToList();
                _allergens.AddRange(allergens);
                var ingredientList = tokens[0].Trim().Split(" ").ToList();
                _ingredientLists.Add(ingredientList);
                foreach (var allergen in allergens)
                {
                    if (_ingredientAllergenDict.ContainsKey(allergen))
                    {
                        _ingredientAllergenDict[allergen].Add(ingredientList);
                    }
                    else
                    {
                        _ingredientAllergenDict.Add(allergen, new List<List<string>> { ingredientList });
                    }
                }
            }
            _allergens = _allergens.Distinct().ToList();
        }

        public static void SetAllergenDict()
        {
            var foundCount = 0;
            while (foundCount < _allergens.Count)
            {
                foreach(var allergen in _allergens)
                {
                    if (_allergenNameDict.ContainsKey(allergen))
                    {
                        continue;
                    }
                    var ingredientLists = _ingredientAllergenDict[allergen];
                    var possibles = from list in ingredientLists
                            from ingredient in list
                            where ingredientLists.All(l => l.Any(i => i == ingredient))
                            select ingredient;
                    possibles = possibles.Distinct();
                    possibles = possibles.Where(x => !_allergenNameDict.ContainsValue(x));
                    if (possibles.Count() == 1)
                    {
                        _allergenNameDict.Add(allergen, possibles.First());
                        foundCount++;
                    }
                }
            }
        }
    }
}
