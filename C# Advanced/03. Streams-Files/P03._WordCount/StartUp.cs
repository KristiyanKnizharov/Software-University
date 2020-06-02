using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace P03._WordCount
{
    class StartUp
    {
        static void Main(string[] args)
        {
            string[] linesFromWordstxt = File.ReadAllLines(@"./words.txt");
            string[] linesFromTexttxt = File.ReadAllLines(@"./text.txt");
            Dictionary<string, int> dict = new Dictionary<string, int>();

            foreach (string line in linesFromWordstxt)
            {
                dict.Add(line, 0);
            }
            foreach (string item in linesFromTexttxt)
            {
                string[] line = item.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                foreach (string word in line)
                {
                    string add = word.ToLower();
                    if (dict.ContainsKey(add))
                    {
                        dict[add] += 1;
                    }
                }
            }

            File.Create(@"./actualResults.txt");
            ///that is for actualResults
            foreach (var item in dict)
            {
                string inserter = item.Key + " - " + item.Value;
                File.WriteAllText(@"./actualResults.txt", inserter);
            }

            File.Create(@"./expectedResult.txt");
            ///that is for expectedResult
            foreach (var item in dict.OrderByDescending(x => x.Value))
            {
                string inserter = item.Key + " - " + item.Value;
                File.WriteAllText(@"./expectedResult.txt", inserter);
            }
        }
    }
}
