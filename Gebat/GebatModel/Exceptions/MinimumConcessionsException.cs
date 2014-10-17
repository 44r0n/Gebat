using System;

namespace GebatModel
{
    public class MinimumConcessionsException : Exception
    {
        public MinimumConcessionsException() { }

        public MinimumConcessionsException(string message) : base(message) { }

        public MinimumConcessionsException(string message, Exception inner) : base(message, inner) { }
    }
}
