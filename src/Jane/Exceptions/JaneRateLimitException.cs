using System;

namespace Jane
{
    [Serializable]
    public class JaneRateLimitException : Exception
    {
        public JaneRateLimitException()
        {
        }

        public JaneRateLimitException(int limit, DateTimeOffset reset)
        {
            Limit = limit;
            Reset = reset;
        }

        public JaneRateLimitException(int limit, DateTimeOffset reset, string message)
            : base(message)
        {
            Limit = limit;
            Reset = reset;
        }

        public JaneRateLimitException(int limit, DateTimeOffset reset, string message, Exception innerException)
            : base(message, innerException)
        {
            Limit = limit;
            Reset = reset;
        }

        public int Limit { get; set; }

        public DateTimeOffset Reset { get; set; }
    }
}