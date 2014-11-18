using System;
namespace GebatModel
{
    public class ConcessionDateException : Exception
    {
        public ConcessionDateException() { }

        public ConcessionDateException(string message) : base(message) { }

        public ConcessionDateException(string message, Exception inner) : base(message, inner) { }
    }
}
