using System;

namespace Jane.Scheduling
{
    public interface IScheduleService
    {
        /// <summary>
        /// start a task
        /// </summary>
        /// <param name="name">task name</param>
        /// <param name="action">task</param>
        /// <param name="dueTime">when the first event will be fired</param>
        /// <param name="period">how often after that</param>
        void StartTask(string name, Action action, int dueTime, int period);

        void StopTask(string name);
    }
}