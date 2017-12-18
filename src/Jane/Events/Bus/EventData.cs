using System;
using Jane.Timing;

namespace Jane.Events.Bus
{
    /// <summary>
    /// Implements <see cref="IEventData"/> and provides a base for event data classes.
    /// </summary>
    [Serializable]
    public abstract class EventData : IEventData
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        protected EventData()
        {
            EventTime = Clock.Now;
        }

        /// <summary>
        /// The object which triggers the event (optional).
        /// </summary>
        public object EventSource { get; set; }

        /// <summary>
        /// The time when the event occurred.
        /// </summary>
        public DateTime EventTime { get; set; }
    }
}