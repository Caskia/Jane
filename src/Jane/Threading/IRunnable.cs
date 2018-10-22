using System.Threading;
using System.Threading.Tasks;

namespace Jane.Threading
{
    /// <summary>
    /// Interface to start/stop self threaded services.
    /// </summary>
    public interface IRunnable
    {
        /// <summary>
        /// Starts the service.
        /// </summary>
        Task StartAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Sends stop command to the service.
        /// Service may return immediately and stop asynchronously.
        /// A client should then call <see cref="WaitToStop"/> method to ensure it's stopped.
        /// </summary>
        Task StopAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}