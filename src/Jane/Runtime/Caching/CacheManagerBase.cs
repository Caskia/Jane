using Jane.Dependency;
using Jane.Runtime.Caching.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Jane.Runtime.Caching
{
    /// <summary>
    /// Base class for cache managers.
    /// </summary>
    public abstract class CacheManagerBase : ICacheManager, ISingletonDependency
    {
        protected readonly ConcurrentDictionary<string, ICache> Caches;
        protected readonly ICachingConfiguration Configuration;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="iocManager"></param>
        /// <param name="configuration"></param>
        protected CacheManagerBase(ICachingConfiguration configuration)
        {
            Configuration = configuration;
            Caches = new ConcurrentDictionary<string, ICache>();
        }

        public virtual void Dispose()
        {
            Caches.Clear();
        }

        public IReadOnlyList<ICache> GetAllCaches()
        {
            return Caches.Values.ToImmutableList();
        }

        public virtual ICache GetCache(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return Caches.GetOrAdd(name, (cacheName) =>
            {
                var cache = CreateCacheImplementation(cacheName);

                var configurators = Configuration.Configurators.Where(c => c.CacheName == null || c.CacheName == cacheName);

                foreach (var configurator in configurators)
                {
                    configurator.InitAction?.Invoke(cache);
                }

                return cache;
            });
        }

        /// <summary>
        /// Used to create actual cache implementation.
        /// </summary>
        /// <param name="name">Name of the cache</param>
        /// <returns>Cache object</returns>
        protected abstract ICache CreateCacheImplementation(string name);
    }
}