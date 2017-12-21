using System;

namespace Jane.Logging
{
    /// <summary>Represents a logger interface.
    /// </summary>
    public interface ILogger
    {
        /// <summary>Represents whether the debug log level is enabled.
        /// </summary>
        bool IsDebugEnabled { get; }

        /// <summary>
        /// Represents whether the error log level is enabled.
        /// </summary>
        bool IsErrorEnabled { get; }

        /// <summary>
        /// Represents whether the fatal log level is enabled.
        /// </summary>
        bool IsFatalEnabled { get; }

        /// <summary>
        ///  Represents whether the info log level is enabled.
        /// </summary>
        bool IsInfoEnabled { get; }

        /// <summary>
        ///  Represents whether the warn log level is enabled.
        /// </summary>
        bool IsWarnEnabled { get; }

        /// <summary>Write a debug level log message.
        /// </summary>
        /// <param name="message"></param>
        void Debug(object message);

        /// <summary>Write a debug level log message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        void Debug(object message, Exception exception);

        /// <summary>Write a debug level log message.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void DebugFormat(string format, params object[] args);

        /// <summary>Write an error level log message.
        /// </summary>
        /// <param name="message"></param>
        void Error(object message);

        /// <summary>Write an error level log message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        void Error(object message, Exception exception);

        /// <summary>Write an error level log message.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void ErrorFormat(string format, params object[] args);

        /// <summary>Write a fatal level log message.
        /// </summary>
        /// <param name="message"></param>
        void Fatal(object message);

        /// <summary>Write a fatal level log message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        void Fatal(object message, Exception exception);

        /// <summary>Write a fatal level log message.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void FatalFormat(string format, params object[] args);

        /// <summary>Write a info level log message.
        /// </summary>
        /// <param name="message"></param>
        void Info(object message);

        /// <summary>Write a info level log message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        void Info(object message, Exception exception);

        /// <summary>Write a info level log message.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void InfoFormat(string format, params object[] args);

        /// <summary>Write a warnning level log message.
        /// </summary>
        /// <param name="message"></param>
        void Warn(object message);

        /// <summary>Write a warnning level log message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        void Warn(object message, Exception exception);

        /// <summary>Write a warnning level log message.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void WarnFormat(string format, params object[] args);
    }
}