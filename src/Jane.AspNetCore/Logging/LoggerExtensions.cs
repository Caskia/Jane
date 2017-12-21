using Microsoft.Extensions.Logging;
using System;

namespace Jane.AspNetCore.Logging
{
    public static class LoggerExtensions
    {
        public static void Log(this Jane.Logging.ILogger logger, LogLevel logLevel, string message)
        {
            switch (logLevel)
            {
                case LogLevel.Critical:
                    logger.Fatal(message);
                    break;

                case LogLevel.Error:
                    logger.Error(message);
                    break;

                case LogLevel.Warning:
                    logger.Warn(message);
                    break;

                case LogLevel.Information:
                    logger.Info(message);
                    break;

                case LogLevel.Debug:
                case LogLevel.Trace:
                    logger.Debug(message);
                    break;

                case LogLevel.None:
                    break;

                default:
                    throw new ArgumentException($"{nameof(logLevel)} value is not implemented: " + logLevel);
            }
        }

        public static void Log(this Jane.Logging.ILogger logger, LogLevel logLevel, string message, Exception exception)
        {
            switch (logLevel)
            {
                case LogLevel.Critical:
                    logger.Fatal(message, exception);
                    break;

                case LogLevel.Error:
                    logger.Error(message, exception);
                    break;

                case LogLevel.Warning:
                    logger.Warn(message, exception);
                    break;

                case LogLevel.Information:
                    logger.Info(message, exception);
                    break;

                case LogLevel.Debug:
                case LogLevel.Trace:
                    logger.Debug(message, exception);
                    break;

                case LogLevel.None:
                    break;

                default:
                    throw new ArgumentException($"{nameof(logLevel)} value is not implemented: " + logLevel);
            }
        }

        public static void Log(this Jane.Logging.ILogger logger, LogLevel logLevel, Func<string> messageFactory)
        {
            switch (logLevel)
            {
                case LogLevel.Critical:
                    logger.Fatal(messageFactory);
                    break;

                case LogLevel.Error:
                    logger.Error(messageFactory);
                    break;

                case LogLevel.Warning:
                    logger.Warn(messageFactory);
                    break;

                case LogLevel.Information:
                    logger.Info(messageFactory);
                    break;

                case LogLevel.Debug:
                case LogLevel.Trace:
                    logger.Debug(messageFactory);
                    break;

                case LogLevel.None:
                    break;

                default:
                    throw new ArgumentException($"{nameof(logLevel)} value is not implemented: " + logLevel);
            }
        }
    }
}