using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Jane.AspNetCore.Logging
{
    public class JaneMsLoggerProvider : ILoggerProvider
    {
        private readonly Jane.Logging.ILoggerFactory _loggerFactory;
        private readonly ConcurrentDictionary<string, JaneMsLoggerAdapter> _loggers;

        public JaneMsLoggerProvider(Jane.Logging.ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _loggers = new ConcurrentDictionary<string, JaneMsLoggerAdapter>();
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(
                           categoryName,
                           name => new JaneMsLoggerAdapter(_loggerFactory.Create(name))
                       );
        }

        public void Dispose()
        {
        }
    }
}