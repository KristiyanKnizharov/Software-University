using System;
using System.Linq;

namespace GeneratingPermutations
{
    public class PermutationsWithoutRepetition
    {
        static char[] elements = Console.ReadLine()
            .Split().Select(char.Parse).ToArray();
        
        //static bool[] used;
        //static string[] permutations;

        static void Main(string[] args)
        {
            //used = new bool[elements.Length];
            //permutations = new string[elements.Length];

            PermutationsWithoutRepetitions(0);
        }

        public static void PermutationsWithoutRepetitions(int index)
        {
            if (index >= elements.Length)
            {
                Console.WriteLine(string.Join(" ", elements));
            }
            else
            {
                PermutationsWithoutRepetitions(index + 1);
                for (int i = index + 1; i < elements.Length; i++)
                {
                    Swap(index, i);
                    PermutationsWithoutRepetitions(index + 1);
                    Swap(index, i);
                }
                // Use a lot of memory
                //for (int i = 0; i < elements.Length; i++)
                //{
                //    if (!used[i])
                //    {
                //        var currNumber = elements[i];
                //        used[i] = true;
                //        permutations[index] = currNumber.ToString();
                //        PermutationsWithoutRepetitions(index + 1);
                //        used[i] = false;
                //    }
                //}
            }
        }

        static void Swap(int first, int second)
        {
            var temp = elements[first];
            elements[first] = elements[second];
            elements[second] = temp;
        }


    }
}
