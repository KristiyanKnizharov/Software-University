namespace _03_Telephony.Core
{
    using _03_Telephony.Exceptions;
    using _03_Telephony.Interface;
    using _03_Telephony.Models;
    using System;
    using System.Linq;

    class Engine : IEngine
    {
        private IReader reader;
        private IWriter writer;

        private StationaryPhone stationaryPhone;
        private Smartphone smartphone;
        private Engine()
        {
            this.stationaryPhone = new StationaryPhone();
            this.smartphone = new Smartphone();
        }
        public Engine(IReader reader, IWriter writer)
            : this()
        {
            this.reader = reader;
            this.writer = writer;
        }
        public void Run()
        {
            string[] phoneNumbers = reader.ReadLine()
                .Split(' ').ToArray();
            string[] sites = reader.ReadLine()
                .Split(' ').ToArray();
            
            foreach (var number in phoneNumbers)
            {
                try
                {
                    if (number.Length == 7)
                    {
                        writer.WriteLine(stationaryPhone.Call(number));
                    }
                    else if (number.Length == 10)
                    {
                        writer.WriteLine(smartphone.Call(number));
                    }
                    else
                    {
                        throw new InvalidNumberException();
                    }
                }
                catch (InvalidNumberException ine)
                {

                    writer.WriteLine(ine.Message);
                }
            }

            foreach (var url in sites)
            {
                try
                {
                    writer.WriteLine(smartphone.Browse(url));
                }
                catch (InvalidUrlException iue)
                {

                    writer.WriteLine(iue.Message);
                }
            }


        }
    }
}
