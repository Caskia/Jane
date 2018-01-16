using Jane.Dependency;
using Jane.Runtime.Caching.Configuration;

namespace Jane.Runtime.Caching.Redis
{
    /// <summary>
    /// Used to create <see cref="RedisCache"/> instances.
    /// </summary>
    public class RedisCacheManager : CacheManagerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedisCacheManager"/> class.
        /// </summary>
        public RedisCacheManager(ICachingConfiguration configuration)
            : base(configuration)
        {
            ObjectContainer.RegisterType(typeof(RedisCache), null, DependencyLifeStyle.Transient);
        }

        protected override ICache CreateCacheImplementation(string name)
        {
            return ObjectContainer.Resolve<RedisCache.Factory>().Invoke(name);
        }
    }
}