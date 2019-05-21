using System;

namespace Jane
{
    [Serializable]
    public class JaneRateLimitException : Exception
    {
        public JaneRateLimitException()
        {
        }

        public JaneRateLimitException(int limit, DateTime reset)
        {
            Limit = limit;
            Reset = reset;
        }

        public JaneRateLimitException(int limit, DateTime reset, string message)
            : base(message)
        {
            Limit = limit;
            Reset = reset;
        }

        public JaneRateLimitException(int limit, DateTime reset, string message, Exception innerException)
            : base(message, innerException)
        {
            Limit = limit;
            Reset = reset;
        }

        public int Limit { get; set; }

        public DateTime Reset { get; set; }
    }
}