namespace Jane.BackgroundJobs
{
    public interface IBackgroundJobExecuter
    {
        void Execute(JobExecutionContext context);
    }
}