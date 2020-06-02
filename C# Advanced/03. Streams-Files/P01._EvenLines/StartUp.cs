using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace StreamFilesProject
{
    class StartUp
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(File.ReadAllText(@"./text.txt"));

            StreamReader streamReader = new StreamReader("./text.txt");
            int counter = 0;
            char[] charecterToReplace = { '-', ',', '.', '!', '?' };
            
            while (!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();
                if (line == null)
                {
                    break;
                }
                if (counter % 2 == 0)
                {
                    line = ReplaceAll(charecterToReplace, line);

                    line = ReverseWordsInText(line);
                    Console.WriteLine(line);
                }

                counter++;
            }
        }

        static string ReplaceAll(char[] replace, string str)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                char currSymbol = str[i];
                if (replace.Contains(currSymbol))
                {
                    sb.Append('@');
                }
                else
                {
                    sb.Append(currSymbol);
                }
            }
            return sb.ToString().TrimEnd();

        }

        static string ReverseWordsInText(string str)
        {
            StringBuilder sb = new StringBuilder();

            string[] words = str.Split(' ').ToArray();
            int wordsLen = words.Length;

            for (int i = 0; i < wordsLen; i++)
            {
                sb.Append(words[wordsLen - i - 1]);
                sb.Append(' ');
            }
            return sb.ToString().TrimEnd();
        }
    }
}
