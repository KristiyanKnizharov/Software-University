using _03_Telephony.Exceptions;
using _03_Telephony.Interface;
using System.Linq;

namespace _03_Telephony.Models
{
    public class StationaryPhone : ICallable
    {
        public StationaryPhone()
        {

        }
        public string Call(string number)
        {
            if (!number.All(ch => char.IsDigit(ch)))
            {
                throw new InvalidNumberException();
            }
            return $"Dialing... {number}";
        }
    }
}
