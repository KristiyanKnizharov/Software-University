using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionalPrograming
{
    class StartUp
    {
        public static void Main(string[] args)
        {

            Func<string, int, bool> func = new Func<string, int, bool>((name, num) =>
            {
                bool isEqualorLarger = false;
                if (name.Length >= num)
                {
                    isEqualorLarger = true;
                }
                return isEqualorLarger;
            });

            int num = int.Parse(Console.ReadLine());
            string[] names = Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            foreach (var item in names)
            {
                if (func(item, num))
                {
                    Console.WriteLine(item);
                    return;
                }
            }
        }
    }
}
