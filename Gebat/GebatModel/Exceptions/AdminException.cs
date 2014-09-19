using System;

namespace GebatModel
{
    public class AdminException : Exception
    {
        public AdminException() { }

        public AdminException(string message) : base(message) { }

        public AdminException(string message, Exception inner) : base(message, inner) { }
    }
}
