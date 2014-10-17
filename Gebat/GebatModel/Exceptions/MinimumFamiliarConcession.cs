using System;

namespace GebatModel
{
    public class MinimumFamiliarConcession : Exception
    {
        public MinimumFamiliarConcession() { }

        public MinimumFamiliarConcession(string message) : base(message) { }

        public MinimumFamiliarConcession(string message, Exception inner) : base(message, inner) { }
    }
}
