
namespace stackAndQueue.cs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            int[] arrInput = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            
            Console.WriteLine(MinValue(arrInput));
            
        }
        static int MinValue(int[] arrInput)
        {
            int minValue = int.MaxValue;

            foreach (var item in arrInput)
            {
                if (item < minValue)
                {
                    minValue = item;
                }
            }
            return minValue;
        }
    }
}
