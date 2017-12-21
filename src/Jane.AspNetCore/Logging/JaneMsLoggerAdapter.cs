using Microsoft.Extensions.Logging;
using System;

namespace Jane.AspNetCore.Logging
{
    public class JaneMsLoggerAdapter : ILogger
    {
        private readonly Jane.Logging.ILogger _logger;

        public JaneMsLoggerAdapter(
            Jane.Logging.ILogger logger
            )
        {
            _logger = logger;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return NullDisposable.Instance;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Critical:
                    return _logger.IsFatalEnabled;

                case LogLevel.Error:
                    return _logger.IsErrorEnabled;

                case LogLevel.Warning:
                    return _logger.IsWarnEnabled;

                case LogLevel.Information:
                    return _logger.IsInfoEnabled;

                case LogLevel.Debug:
                case LogLevel.Trace: //Trace is not included in ILogger
                    return _logger.IsDebugEnabled;

                case LogLevel.None:
                    return false;

                default:
                    throw new ArgumentException($"{nameof(logLevel)} value is not implemented: " + logLevel);
            }
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            var message = formatter(state, exception);

            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            _logger.Log(logLevel, message, exception);
        }

        private class NullDisposable : IDisposable
        {
            public static readonly NullDisposable Instance = new NullDisposable();

            public void Dispose()
            {
            }
        }
    }
}