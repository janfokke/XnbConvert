using System;

namespace XnbConvert.Exceptions
{
    public class InvalidXnbTypeReaderNameException : Exception
    {
        public InvalidXnbTypeReaderNameException(string xnbTypeReaderName)
        {
            XnbTypeReaderName = xnbTypeReaderName;
        }

        public string XnbTypeReaderName { get; }
    }
}