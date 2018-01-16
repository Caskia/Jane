using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Jane.Runtime.Caching.Configuration
{
    internal class CachingConfiguration : ICachingConfiguration
    {
        private readonly List<ICacheConfigurator> _configurators;

        public CachingConfiguration()
        {
            _configurators = new List<ICacheConfigurator>();
        }

        public IReadOnlyList<ICacheConfigurator> Configurators
        {
            get { return _configurators.ToImmutableList(); }
        }

        public void Configure(string cacheName, Action<ICache> initAction)
        {
            _configurators.Add(new CacheConfigurator(cacheName, initAction));
        }

        public void ConfigureAll(Action<ICache> initAction)
        {
            _configurators.Add(new CacheConfigurator(initAction));
        }
    }
}