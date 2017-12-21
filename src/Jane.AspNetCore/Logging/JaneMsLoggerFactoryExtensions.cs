using Microsoft.Extensions.Logging;

namespace Jane.AspNetCore.Logging
{
    public static class JaneMsLoggerFactoryExtensions
    {
        public static ILoggerFactory AddJaneLogger(this ILoggerFactory factory, Jane.Logging.ILoggerFactory loggerFactory)
        {
            factory.AddProvider(new JaneMsLoggerProvider(loggerFactory));
            return factory;
        }
    }
}