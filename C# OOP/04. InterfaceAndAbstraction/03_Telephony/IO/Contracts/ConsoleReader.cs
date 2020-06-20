using _03_Telephony.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace _03_Telephony.IO.Contracts
{
    public class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
