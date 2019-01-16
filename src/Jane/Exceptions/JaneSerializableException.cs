using Jane.Logging;
using Newtonsoft.Json;
using System;

namespace Jane
{
    [Serializable]
    public class JaneSerializableException : JaneException, IHasLogSeverity
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public JaneSerializableException()
        {
            Severity = LogSeverity.Warn;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        public JaneSerializableException(string message)
            : base(SerializeMessageToJson(default(int), message))
        {
            Severity = LogSeverity.Warn;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="severity">Exception severity</param>
        public JaneSerializableException(string message, LogSeverity severity)
            : base(SerializeMessageToJson(default(int), message))
        {
            Severity = severity;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="message">Exception message</param>
        public JaneSerializableException(int code, string message)
            : base(SerializeMessageToJson(code, message))
        {
            Severity = LogSeverity.Warn;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="details">Additional information about the exception</param>
        public JaneSerializableException(string message, string details)
            : base(SerializeMessageToJson(default(int), message))
        {
            Severity = LogSeverity.Warn;
            Details = details;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="message">Exception message</param>
        /// <param name="details">Additional information about the exception</param>
        public JaneSerializableException(int code, string message, string details)
            : base(SerializeMessageToJson(code, message))
        {
            Severity = LogSeverity.Warn;
            Details = details;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public JaneSerializableException(string message, Exception innerException)
            : base(SerializeMessageToJson(default(int), message), innerException)
        {
            Severity = LogSeverity.Warn;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="details">Additional information about the exception</param>
        /// <param name="innerException">Inner exception</param>
        public JaneSerializableException(string message, string details, Exception innerException)
            : base(message, innerException)
        {
            Severity = LogSeverity.Warn;
            Details = details;
        }

        /// <summary>
        /// Additional information about the exception.
        /// </summary>
        public string Details { get; private set; }

        /// <summary>
        /// Severity of the exception.
        /// Default: Warn.
        /// </summary>
        public LogSeverity Severity { get; set; }

        private static string SerializeMessageToJson(int code, string message)
        {
            return SerializableExceptionWrapper.Serialize(new SerializableExceptionWrapper()
            {
                Message = message,
                Code = code
            });
        }
    }
}