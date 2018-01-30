using System;
using System.Runtime.Serialization;
using Jane.Logging;

namespace Jane
{
    /// <summary>
    /// This exception is thrown on an unauthorized request.
    /// </summary>
    [Serializable]
    public class JaneAuthorizationException : JaneException, IHasLogSeverity
    {
        /// <summary>
        /// Creates a new <see cref="JaneAuthorizationException"/> object.
        /// </summary>
        public JaneAuthorizationException()
        {
            Severity = LogSeverity.Warn;
        }

        /// <summary>
        /// Creates a new <see cref="JaneAuthorizationException"/> object.
        /// </summary>
        public JaneAuthorizationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }

        /// <summary>
        /// Creates a new <see cref="JaneAuthorizationException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public JaneAuthorizationException(string message)
            : base(message)
        {
            Severity = LogSeverity.Warn;
        }

        /// <summary>
        /// Creates a new <see cref="JaneAuthorizationException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public JaneAuthorizationException(string message, Exception innerException)
            : base(message, innerException)
        {
            Severity = LogSeverity.Warn;
        }

        /// <summary>
        /// Severity of the exception.
        /// Default: Warn.
        /// </summary>
        public LogSeverity Severity { get; set; }
    }
}