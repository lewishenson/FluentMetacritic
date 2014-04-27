using System;
using System.Runtime.Serialization;
using System.Security;

namespace FluentMetacritic
{
    [Serializable]
    public class MetacriticUnavailableException : InvalidOperationException
    {
        public MetacriticUnavailableException()
        {
        }

        public MetacriticUnavailableException(string message)
            : base(message)
        {
        }

        [SecuritySafeCritical]
        protected MetacriticUnavailableException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public MetacriticUnavailableException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}