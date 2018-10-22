using Jane.Logging;

namespace Jane.BackgroundJobs
{
    public abstract class BackgroundJob<TArgs> : IBackgroundJob<TArgs>
    {
        //TODO: Add UOW, Localization and other useful properties..?

        protected BackgroundJob()
        {
            Logger = LogHelper.Logger;
        }

        public ILogger Logger { get; set; }

        public abstract void Execute(TArgs args);
    }
}