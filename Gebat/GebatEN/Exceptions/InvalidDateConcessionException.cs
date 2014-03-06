using System;

namespace GebatEN.Exceptions
{
    [Serializable()]
    public class InvalidDateConcessionException : System.Exception
    {
        public InvalidDateConcessionException() { }
        public InvalidDateConcessionException(string message) { }
        public InvalidDateConcessionException(string message, System.Exception inner) { }
        protected InvalidDateConcessionException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
        }
    }
}
