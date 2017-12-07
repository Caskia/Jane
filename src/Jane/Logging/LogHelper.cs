using Jane.Dependency;
using System;

namespace Jane.Logging
{
    /// <summary>
    /// This class can be used to write logs from somewhere where it's a hard to get a reference to the <see cref="ILogger"/>.
    /// Normally, use <see cref="ILogger"/> with property injection wherever it's possible.
    /// </summary>
    public static class LogHelper
    {
        static LogHelper()
        {
            if (ObjectContainer.Current.TryResolve<ILoggerFactory>(out var loggerFactory))
            {
                Logger = loggerFactory.Create(typeof(LogHelper));
            }
            else
            {
                Logger = new EmptyLoggerFactory().Create(typeof(LogHelper));
            }
        }

        /// <summary>
        /// A reference to the logger.
        /// </summary>
        public static ILogger Logger { get; private set; }

        public static void LogException(Exception ex)
        {
            LogException(Logger, ex);
        }

        public static void LogException(ILogger logger, Exception ex)
        {
            var severity = (ex as IHasLogSeverity)?.Severity ?? LogSeverity.Error;

            logger.Log(severity, ex.Message, ex);
        }
    }
}