using _03_Telephony.Interface;
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using _03_Telephony.Exceptions;

namespace _03_Telephony.Models
{
    public class Smartphone : ICallable, IBrowsable
    {
        public string Call(string number)
        {
            if (!number.All(ch => char.IsDigit(ch)))
            {
                throw new InvalidNumberException();
            }
            return $"Calling... {number}";
        }
        public string Browse(string url)
        {
            if (url.Any(ch => char.IsDigit(ch)))
            {
                throw new InvalidUrlException();
            };
            return $"Browsing: {url}!";
        }

        
    }
}
