using Jane.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Jane.BackgroundWorkers
{
    /// <summary>
    /// Base class that can be used to implement <see cref="IBackgroundWorker"/>.
    /// </summary>
    public abstract class BackgroundWorkerBase : IBackgroundWorker
    {
        //TODO: Add UOW, Localization and other useful properties..?

        protected BackgroundWorkerBase()
        {
            Logger = LogHelper.Logger;
        }

        public ILogger Logger { protected get; set; }

        public virtual Task StartAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            Logger.Debug("Started background worker: " + ToString());
            return Task.CompletedTask;
        }

        public virtual Task StopAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            Logger.Debug("Stopped background worker: " + ToString());
            return Task.CompletedTask;
        }

        public override string ToString()
        {
            return GetType().FullName;
        }
    }
}