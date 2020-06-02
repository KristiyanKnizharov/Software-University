using System;
using System.IO;

namespace P02._LineNumbers
{
    class StartUp
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("./text.txt");
            
            for (int i = 0; i < lines.Length; i++)
            {
                int counterChars = 0;
                int counterMarks = 0;
                string currLines = lines[i];
                foreach (char item in currLines)
                {
                    if (char.IsLetter(item))
                    {
                        counterChars++;
                    }
                    else if (char.IsPunctuation(item))
                    {
                        counterMarks++;
                    }
                }
                Console.WriteLine($"Line {i + 1}:{currLines} ({counterChars}) ({counterMarks})");
            }


        }
    }
}
