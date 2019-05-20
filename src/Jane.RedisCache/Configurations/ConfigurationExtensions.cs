using Jane.Dependency;
using Jane.Limits;
using Jane.Runtime.Caching;
using Jane.Runtime.Caching.Redis;
using System.Reflection;

namespace Jane.Configurations
{
    /// <summary>
    /// configuration class redis extensions.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// use redis as the cache
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static Configuration UseRedis(this Configuration configuration)
        {
            var assemblies = new[]
            {
                Assembly.GetAssembly(typeof(RedisCacheOptions))
            };
            configuration.RegisterAssemblies(assemblies);

            configuration.SetDefault<RedisCache, RedisCache>(null, DependencyLifeStyle.Transient);
            configuration.SetDefault<ICacheManager, RedisCacheManager>();
            configuration.SetDefault<IRateLimiter, RedisRateLimiter>();

            return configuration;
        }
    }
}