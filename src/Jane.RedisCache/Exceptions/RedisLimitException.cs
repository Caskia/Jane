using System;

namespace Jane
{
    [Serializable]
    public class RedisLimitException : Exception
    {
        public RedisLimitException()
        {
        }

        public RedisLimitException(string message)
            : base(message)
        {
        }

        public RedisLimitException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}