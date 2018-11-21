using Jane.BackgroundJobs;

namespace Jane.Hangfire
{
    public class HangfireJobExecutionAdapter<TArgs>
    {
        public HangfireJobExecutionAdapter(BackgroundJobOptions options, IBackgroundJobExecuter jobExecuter)
        {
            JobExecuter = jobExecuter;
            Options = options;
        }

        protected IBackgroundJobExecuter JobExecuter { get; }
        protected BackgroundJobOptions Options { get; }

        public void Execute(TArgs args)
        {
            var jobType = Options.GetJob(typeof(TArgs)).JobType;
            var context = new JobExecutionContext(jobType, args);
            JobExecuter.Execute(context);
        }
    }
}