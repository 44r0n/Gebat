using System;

namespace GebatModel
{
    public class MinimumFamiliarConcessionException : Exception
    {
        public MinimumFamiliarConcessionException() { }

        public MinimumFamiliarConcessionException(string message) : base(message) { }

        public MinimumFamiliarConcessionException(string message, Exception inner) : base(message, inner) { }
    }
}
