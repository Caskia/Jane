using Jane.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Jane.BackgroundJobs
{
    public class BackgroundJobExecuter : IBackgroundJobExecuter
    {
        public BackgroundJobExecuter(
            IServiceProvider serviceProvider,
            IOptions<BackgroundJobOptions> options)
        {
            ServiceProvider = serviceProvider;
            Options = options.Value;

            Logger = LogHelper.Logger;
        }

        public ILogger Logger { protected get; set; }

        protected BackgroundJobOptions Options { get; }
        protected IServiceProvider ServiceProvider { get; }

        public virtual void Execute(JobExecutionContext context)
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var job = scope.ServiceProvider.GetService(context.JobType);
                if (job == null)
                {
                    throw new JaneException("The job type is not registered to DI: " + context.JobType);
                }

                var jobExecuteMethod = context.JobType.GetMethod(nameof(IBackgroundJob<object>.Execute));
                if (jobExecuteMethod == null)
                {
                    throw new JaneException($"Given job type does not implement {typeof(IBackgroundJob<>).Name}. The job type was: " + context.JobType);
                }

                try
                {
                    jobExecuteMethod.Invoke(job, new[] { context.JobArgs });
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);

                    throw new BackgroundJobExecutionException("A background job execution is failed. See inner exception for details.", ex)
                    {
                        JobType = context.JobType.AssemblyQualifiedName,
                        JobArgs = context.JobArgs
                    };
                }
            }
        }
    }
}