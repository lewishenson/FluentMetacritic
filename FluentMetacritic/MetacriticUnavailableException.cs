using System;

namespace FluentMetacritic
{
    public class MetacriticUnavailableException : InvalidOperationException
    {
        public MetacriticUnavailableException()
        {
        }

        public MetacriticUnavailableException(string message)
            : base(message)
        {
        }
        
        public MetacriticUnavailableException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}