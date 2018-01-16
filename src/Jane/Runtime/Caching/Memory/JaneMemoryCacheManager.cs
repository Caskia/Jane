using Jane.Dependency;
using Jane.Runtime.Caching.Configuration;

namespace Jane.Runtime.Caching.Memory
{
    /// <summary>
    /// Implements <see cref="ICacheManager"/> to work with MemoryCache.
    /// </summary>
    public class JaneMemoryCacheManager : CacheManagerBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public JaneMemoryCacheManager(ICachingConfiguration configuration)
            : base(configuration)
        {
            ObjectContainer.RegisterType(typeof(JaneMemoryCache), null, DependencyLifeStyle.Transient);
        }

        protected override ICache CreateCacheImplementation(string name)
        {
            return ObjectContainer.Resolve<JaneMemoryCache.Factory>().Invoke(name);
        }
    }
}