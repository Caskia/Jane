using Jane.Timing;
using System;
using System.Threading.Tasks;

namespace Jane.BackgroundJobs
{
    /// <summary>
    /// Default implementation of <see cref="IBackgroundJobManager"/>.
    /// </summary>
    public class DefaultBackgroundJobManager : IBackgroundJobManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultBackgroundJobManager"/> class.
        /// </summary>
        public DefaultBackgroundJobManager(
            IBackgroundJobSerializer serializer,
            IBackgroundJobStore store,
            IIdGenerator idGenerator)
        {
            Serializer = serializer;
            IdGenerator = idGenerator;
            Store = store;
        }

        protected IIdGenerator IdGenerator { get; }
        protected IBackgroundJobSerializer Serializer { get; }
        protected IBackgroundJobStore Store { get; }

        public virtual async Task<string> EnqueueAsync<TArgs>(TArgs args, BackgroundJobPriority priority = BackgroundJobPriority.Normal, TimeSpan? delay = null)
        {
            var jobName = BackgroundJobNameAttribute.GetName<TArgs>();
            var jobId = await EnqueueAsync(jobName, args, priority, delay);
            return jobId.ToString();
        }

        protected virtual async Task<Guid> EnqueueAsync(string jobName, object args, BackgroundJobPriority priority = BackgroundJobPriority.Normal, TimeSpan? delay = null)
        {
            var jobInfo = new BackgroundJobInfo
            {
                Id = IdGenerator.Guid(),
                JobName = jobName,
                JobArgs = Serializer.Serialize(args),
                Priority = priority,
                CreationTime = Clock.Now,
                NextTryTime = Clock.Now
            };

            if (delay.HasValue)
            {
                jobInfo.NextTryTime = Clock.Now.Add(delay.Value);
            }

            await Store.InsertAsync(jobInfo);

            return jobInfo.Id;
        }
    }
}