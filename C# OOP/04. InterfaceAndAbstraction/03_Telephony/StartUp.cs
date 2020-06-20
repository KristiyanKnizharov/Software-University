using _03_Telephony.Core;
using _03_Telephony.Interface;
using _03_Telephony.IO.Contracts;
using System;

namespace _03_Telephony
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();

            IEngine engine = new Engine(reader, writer);
            engine.Run();
        }
    }
}
