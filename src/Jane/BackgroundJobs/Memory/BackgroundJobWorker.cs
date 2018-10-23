using Jane.BackgroundWorkers;
using Jane.Threading;
using Jane.Threading.Timers;
using Jane.Timing;
using System;

namespace Jane.BackgroundJobs
{
    public class BackgroundJobWorker : PeriodicBackgroundWorkerBase, IBackgroundJobWorker
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultBackgroundJobManager"/> class.
        /// </summary>
        public BackgroundJobWorker(
            IBackgroundJobStore store,
            JaneTimer timer,
            IBackgroundJobExecuter jobExecuter,
            IBackgroundJobSerializer serializer,
            BackgroundJobOptions jobOptions,
            BackgroundJobWorkerOptions workerOptions)
            : base(timer)
        {
            JobExecuter = jobExecuter;
            Serializer = serializer;
            Store = store;
            WorkerOptions = workerOptions;
            JobOptions = jobOptions;
            Timer.Period = WorkerOptions.JobPollPeriod;
        }

        protected IBackgroundJobExecuter JobExecuter { get; }
        protected BackgroundJobOptions JobOptions { get; }
        protected IBackgroundJobSerializer Serializer { get; }
        protected IBackgroundJobStore Store { get; }
        protected BackgroundJobWorkerOptions WorkerOptions { get; }

        protected virtual DateTime? CalculateNextTryTime(BackgroundJobInfo jobInfo) //TODO: Move to another place to override easier
        {
            var nextWaitDuration = WorkerOptions.DefaultFirstWaitDuration * (Math.Pow(WorkerOptions.DefaultWaitFactor, jobInfo.TryCount - 1));
            var nextTryDate = jobInfo.LastTryTime.HasValue
                ? jobInfo.LastTryTime.Value.AddSeconds(nextWaitDuration)
                : Clock.Now.AddSeconds(nextWaitDuration);

            if (nextTryDate.Subtract(jobInfo.CreationTime).TotalSeconds > WorkerOptions.DefaultTimeout)
            {
                return null;
            }

            return nextTryDate;
        }

        protected override void DoWork()
        {
            var waitingJobs = AsyncHelper.RunSync(() => Store.GetWaitingJobsAsync(WorkerOptions.MaxJobFetchCount));

            foreach (var jobInfo in waitingJobs)
            {
                jobInfo.TryCount++;
                jobInfo.LastTryTime = Clock.Now;

                try
                {
                    var jobConfiguration = JobOptions.GetJob(jobInfo.JobName);
                    var jobArgs = Serializer.Deserialize(jobInfo.JobArgs, jobConfiguration.ArgsType);
                    var context = new JobExecutionContext(jobConfiguration.JobType, jobArgs);

                    try
                    {
                        JobExecuter.Execute(context);
                        AsyncHelper.RunSync(() => Store.DeleteAsync(jobInfo.Id));
                    }
                    catch (BackgroundJobExecutionException)
                    {
                        var nextTryTime = CalculateNextTryTime(jobInfo);
                        if (nextTryTime.HasValue)
                        {
                            jobInfo.NextTryTime = nextTryTime.Value;
                        }
                        else
                        {
                            jobInfo.IsAbandoned = true;
                        }

                        TryUpdate(jobInfo);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    jobInfo.IsAbandoned = true;
                    TryUpdate(jobInfo);
                }
            }
        }

        protected virtual void TryUpdate(BackgroundJobInfo jobInfo)
        {
            try
            {
                Store.UpdateAsync(jobInfo);
            }
            catch (Exception updateEx)
            {
                Logger.Error(updateEx);
            }
        }
    }
}