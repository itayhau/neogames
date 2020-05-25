using System;
using System.Runtime.Serialization;

namespace CoreProject
{
    [Serializable]
    public class RequestIdNotFound : Exception
    {
        public RequestIdNotFound()
        {
        }

        public RequestIdNotFound(string message) : base(message)
        {
        }

        public RequestIdNotFound(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RequestIdNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}