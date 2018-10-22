using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Jane.BackgroundWorkers
{
    /// <summary>
    /// Implements <see cref="IBackgroundWorkerManager"/>.
    /// </summary>
    public class BackgroundWorkerManager : IBackgroundWorkerManager, IDisposable
    {
        private readonly List<IBackgroundWorker> _backgroundWorkers;
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundWorkerManager"/> class.
        /// </summary>
        public BackgroundWorkerManager()
        {
            _backgroundWorkers = new List<IBackgroundWorker>();
        }

        protected bool IsRunning { get; private set; }

        public void Add(IBackgroundWorker worker)
        {
            _backgroundWorkers.Add(worker);

            if (IsRunning)
            {
                worker.StartAsync();
            }
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;

            //TODO: ???
        }

        public async Task StartAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            IsRunning = true;

            foreach (var worker in _backgroundWorkers)
            {
                await worker.StartAsync(cancellationToken);
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            IsRunning = false;

            foreach (var worker in _backgroundWorkers)
            {
                await worker.StopAsync(cancellationToken);
            }
        }
    }
}