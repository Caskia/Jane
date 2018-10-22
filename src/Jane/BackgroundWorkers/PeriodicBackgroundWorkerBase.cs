using Jane.Threading.Timers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Jane.BackgroundWorkers
{
    /// <summary>
    /// Extends <see cref="BackgroundWorkerBase"/> to add a periodic running Timer.
    /// </summary>
    public abstract class PeriodicBackgroundWorkerBase : BackgroundWorkerBase
    {
        protected readonly JaneTimer Timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="PeriodicBackgroundWorkerBase"/> class.
        /// </summary>
        /// <param name="timer">A timer.</param>
        protected PeriodicBackgroundWorkerBase(JaneTimer timer)
        {
            Timer = timer;
            Timer.Elapsed += Timer_Elapsed;
        }

        public override async Task StartAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await base.StartAsync(cancellationToken);
            await Timer.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await Timer.StopAsync(cancellationToken);
            await base.StopAsync(cancellationToken);
        }

        /// <summary>
        /// Periodic works should be done by implementing this method.
        /// </summary>
        protected abstract void DoWork();

        private void Timer_Elapsed(object sender, System.EventArgs e)
        {
            try
            {
                DoWork();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
    }
}