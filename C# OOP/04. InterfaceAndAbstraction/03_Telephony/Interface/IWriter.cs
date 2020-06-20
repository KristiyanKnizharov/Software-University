using System;
using System.Collections.Generic;
using System.Text;

namespace _03_Telephony.Interface
{
    public interface IWriter
    {
        void Write(string text);
        void WriteLine(string text);
    }
}
