using System;

namespace Jane.Events.Bus.Exceptions
{
    /// <summary>
    /// This type of events are used to notify for exceptions handled by Jane infrastructure.
    /// </summary>
    public class EventHandledExceptionData : ExceptionData
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="exception">Exception object</param>
        public EventHandledExceptionData(Exception exception)
            : base(exception)
        {
        }
    }
}