using System;
using System.Linq;

namespace _01_SortingAndSearching
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            int[] input = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var watch = System.Diagnostics.Stopwatch.StartNew();

            //Remove the slashes for which algorithm you want
            // and see it's execution time.
            //SelectionSort(input);
            //BubbleSort(input);
            //InsertionSort(input);


            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            //Console.WriteLine($"Time take: {elapsedMs}ms.");

        }

        private static void InsertionSort(int[] input)
        {
            for (int i = 1; i < input.Length; ++i)
            {
                int currElement = input[i];
                int prevElPos = i - 1;

                while (prevElPos >= 0 
                        &&
                       input[prevElPos] > currElement)
                {
                    input[prevElPos + 1] = input[prevElPos];
                    prevElPos = prevElPos - 1;
                }
                input[prevElPos + 1] = currElement;
            }
            Console.WriteLine(string.Join(" ", input));
        }

        private static void SelectionSort(int[] input)
        {
            int currNumber;
            int minNumber = int.MaxValue;
            int minPosition = 0;

            for (int i = 0; i < input.Length; i++)
            {
                currNumber = input[i];
                for (int j = i; j < input.Length; j++)
                {
                    if (minNumber > input[j])
                    {
                        minNumber = input[j];
                        minPosition = j;
                    }
                }
                input[minPosition] = currNumber;
                input[i] = minNumber;
                minNumber = int.MaxValue;
            }
            Console.WriteLine(string.Join(" ", input));
        }

        private static void BubbleSort(int[] input)
        {
            int counter = 0;
            int currNumber;
            int nextNumber;

            for (int i = 0; i < input.Length-1; i++)
            {
                currNumber = input[i];
                nextNumber = input[i + 1];
                if (nextNumber < currNumber)
                {
                    input[i] = nextNumber;
                    input[i + 1] = currNumber;
                    counter++;
                }
            }
            if (counter == 0)
            {
                Console.WriteLine(string.Join(" ", input));

            }
            else
            {
                BubbleSort(input);
            }
        }

    }
}
