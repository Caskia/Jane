using Jane.Dependency;
using StackExchange.Redis;

namespace Jane.Runtime.Caching.Redis
{
    /// <summary>
    /// Used to get <see cref="IDatabase"/> for Redis cache.
    /// </summary>
    public interface IRedisCacheDatabaseProvider : ISingletonDependency
    {
        /// <summary>
        /// Gets the database connection.
        /// </summary>
        IDatabase GetDatabase();
    }
}