using Hangfire;
using Jane.BackgroundJobs;
using System;
using System.Threading.Tasks;

namespace Jane.Hangfire
{
    public class HangfireBackgroundJobManager : IBackgroundJobManager
    {
        public Task<string> EnqueueAsync<TArgs>(TArgs args, BackgroundJobPriority priority = BackgroundJobPriority.Normal,
            TimeSpan? delay = null)
        {
            if (!delay.HasValue)
            {
                return Task.FromResult(BackgroundJob.Enqueue<HangfireJobExecutionAdapter<TArgs>>(adapter => adapter.Execute(args)));
            }
            else
            {
                return Task.FromResult(BackgroundJob.Schedule<HangfireJobExecutionAdapter<TArgs>>(adapter => adapter.Execute(args), delay.Value));
            }
        }
    }
}