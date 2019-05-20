using System;

namespace Jane
{
    [Serializable]
    public class JaneRateLimitException : Exception
    {
        public JaneRateLimitException()
        {
        }

        public JaneRateLimitException(string message)
            : base(message)
        {
        }

        public JaneRateLimitException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}