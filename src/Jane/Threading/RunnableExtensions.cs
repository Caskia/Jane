using System;

namespace Jane.Threading
{
    /// <summary>
    /// Some extension methods for <see cref="IRunnable"/>.
    /// </summary>
    public static class RunnableExtensions
    {
        public static void Start(this IRunnable runnable)
        {
            if (runnable == null)
            {
                throw new ArgumentNullException(nameof(runnable));
            }

            AsyncHelper.RunSync(() => runnable.StartAsync());
        }

        public static void Stop(this IRunnable runnable)
        {
            if (runnable == null)
            {
                throw new ArgumentNullException(nameof(runnable));
            }

            AsyncHelper.RunSync(() => runnable.StopAsync());
        }
    }
}