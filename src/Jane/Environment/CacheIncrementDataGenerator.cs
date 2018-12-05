using Jane.Runtime.Caching;
using System.Threading.Tasks;

namespace Jane
{
    public class CacheIncrementDataGenerator : IIncrementDataGenerator
    {
        private readonly ICacheManager _cacheManager;

        public CacheIncrementDataGenerator
            (
            ICacheManager cacheManager
            )
        {
            _cacheManager = cacheManager;
        }

        public Task<long> IncrementAsync(string key, int value = 1)
        {
            return _cacheManager
                .GetCache(JaneCacheNames.ApplicationSettings)
                .IncrementAsync(key, value);
        }
    }
}