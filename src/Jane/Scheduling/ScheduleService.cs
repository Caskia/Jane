using System;
using System.Collections.Generic;
using System.Threading;
using Jane.Logging;

namespace Jane.Scheduling
{
    public class ScheduleService : IScheduleService
    {
        private readonly object _lockObject = new object();
        private readonly ILogger _logger;
        private readonly Dictionary<string, TimerBasedTask> _taskDict = new Dictionary<string, TimerBasedTask>();

        public ScheduleService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.Create(GetType().FullName);
        }

        public void StartTask(string name, Action action, int dueTime, int period)
        {
            lock (_lockObject)
            {
                if (_taskDict.ContainsKey(name)) return;
                var timer = new Timer(TaskCallback, name, Timeout.Infinite, Timeout.Infinite);
                _taskDict.Add(name, new TimerBasedTask { Name = name, Action = action, Timer = timer, DueTime = dueTime, Period = period, Stopped = false });
                timer.Change(dueTime, period);
            }
        }

        public void StopTask(string name)
        {
            lock (_lockObject)
            {
                if (_taskDict.ContainsKey(name))
                {
                    var task = _taskDict[name];
                    task.Stopped = true;
                    task.Timer.Dispose();
                    _taskDict.Remove(name);
                }
            }
        }

        private void TaskCallback(object obj)
        {
            var taskName = (string)obj;
            TimerBasedTask task;

            if (_taskDict.TryGetValue(taskName, out task))
            {
                try
                {
                    if (!task.Stopped)
                    {
                        task.Timer.Change(Timeout.Infinite, Timeout.Infinite);
                        task.Action();
                    }
                }
                catch (ObjectDisposedException) { }
                catch (Exception ex)
                {
                    if (_logger != null)
                    {
                        _logger.Error(string.Format("Task has exception, name: {0}, due: {1}, period: {2}", task.Name, task.DueTime, task.Period), ex);
                    }
                }
                finally
                {
                    try
                    {
                        if (!task.Stopped)
                        {
                            task.Timer.Change(task.Period, task.Period);
                        }
                    }
                    catch (ObjectDisposedException) { }
                    catch (Exception ex)
                    {
                        if (_logger != null)
                        {
                            _logger.Error(string.Format("Timer change has exception, name: {0}, due: {1}, period: {2}", task.Name, task.DueTime, task.Period), ex);
                        }
                    }
                }
            }
        }

        private class TimerBasedTask
        {
            public Action Action;
            public int DueTime;
            public string Name;
            public int Period;
            public bool Stopped;
            public Timer Timer;
        }
    }
}