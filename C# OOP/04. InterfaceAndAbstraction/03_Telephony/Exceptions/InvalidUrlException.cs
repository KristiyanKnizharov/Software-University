using System;
using System.Collections.Generic;
using System.Text;

namespace _03_Telephony.Exceptions
{
    public class InvalidUrlException : Exception
    {
        private const string INVALID_URL_EXC = "Invalid URL!";
        public InvalidUrlException()
            : base(INVALID_URL_EXC)
        {

        }
        public InvalidUrlException(string message)
            : base(message)
        {
            
        }
    }
}
