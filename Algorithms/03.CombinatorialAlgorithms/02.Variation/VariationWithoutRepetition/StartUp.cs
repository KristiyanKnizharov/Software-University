using System;
using System.Linq;

namespace VariationWithoutRepetition
{
    public class StartUp
    {
        static char[] variations;
        static char[] elements;
        static bool[] used;

        static void Variations(int index)
        {
            if(index >= variations.Length)
            {
                Console.WriteLine(string.Join(" ", variations));
            }
            else
            {
                for (int i = 0; i < elements.Length; i++)
                {
                    if (!used[i])
                    {
                        used[i] = true;
                        variations[index] = elements[i];
                        Variations(index + 1);
                        used[i] = false;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            elements = Console.ReadLine()
                .Split()
                .Select(char.Parse)
                .ToArray();
            var k = int.Parse(Console.ReadLine());

            variations = new char[k];
            used = new bool[elements.Length];
            Variations(0);

        }


        public static void Swap(int first, int second)
        {
            var temp = elements[first];
            elements[first] = elements[second];
            elements[second] = temp;
        }
    }
}
