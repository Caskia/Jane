using System;
using System.Runtime.Serialization;

namespace Jane.BackgroundJobs
{
    [Serializable]
    public class BackgroundJobExecutionException : JaneException
    {
        public string JobType { get; set; }

        public object JobArgs { get; set; }

        public BackgroundJobExecutionException()
        {

        }

        /// <summary>
        /// Creates a new <see cref="BackgroundJobExecutionException"/> object.
        /// </summary>
        public BackgroundJobExecutionException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Creates a new <see cref="BackgroundJobExecutionException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public BackgroundJobExecutionException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
