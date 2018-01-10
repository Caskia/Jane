using System;
using System.Runtime.Serialization;

namespace Jane
{
    /// <summary>
    /// Base exception type for those are thrown by system for specific exceptions.
    /// </summary>
    [Serializable]
    public class JaneException : Exception
    {
        /// <summary>
        /// Creates a new <see cref="JaneException"/> object.
        /// </summary>
        public JaneException()
        {
        }

        /// <summary>
        /// Creates a new <see cref="JaneException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public JaneException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Creates a new <see cref="JaneException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public JaneException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Creates a new <see cref="JaneException"/> object.
        /// </summary>
        public JaneException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }
    }
}