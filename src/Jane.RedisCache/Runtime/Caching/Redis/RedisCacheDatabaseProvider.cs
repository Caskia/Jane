using StackExchange.Redis;
using System;

namespace Jane.Runtime.Caching.Redis
{
    /// <summary>
    /// Implements <see cref="IRedisCacheDatabaseProvider"/>.
    /// </summary>
    public class RedisCacheDatabaseProvider : IRedisCacheDatabaseProvider
    {
        private readonly Lazy<ConnectionMultiplexer> _connectionMultiplexer;
        private readonly IRedisCacheOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisCacheDatabaseProvider"/> class.
        /// </summary>
        public RedisCacheDatabaseProvider(IRedisCacheOptions options)
        {
            _options = options;
            _connectionMultiplexer = new Lazy<ConnectionMultiplexer>(CreateConnectionMultiplexer);
        }

        /// <summary>
        /// Gets the database connection.
        /// </summary>
        public IDatabase GetDatabase()
        {
            return _connectionMultiplexer.Value.GetDatabase(_options.DatabaseId);
        }

        private ConnectionMultiplexer CreateConnectionMultiplexer()
        {
            return ConnectionMultiplexer.Connect(_options.ConnectionString);
        }
    }
}