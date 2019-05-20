using Jane.Runtime.Caching.Redis;
using Jane.Timing;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace Jane.Limits
{
    public class RedisRateLimiter : IRateLimiter
    {
        private readonly IDatabase _database;

        public RedisRateLimiter(IRedisCacheDatabaseProvider redisCacheDatabaseProvider)
        {
            _database = redisCacheDatabaseProvider.GetDatabase();
        }

        public Task PerDayLimitAsync(string key, int limit)
        {
            key += $":Day:{Clock.Now.Date}";

            return CountBasedLimitAsync(key, limit, new TimeSpan(24, 0, 0));
        }

        public Task PerHourLimitAsync(string key, int limit)
        {
            key += $":Hour:{Clock.Now.Hour}";

            return CountBasedLimitAsync(key, limit, new TimeSpan(1, 0, 0));
        }

        public Task PerMinuteLimitAsync(string key, int limit)
        {
            key += $":Minute:{Clock.Now.Minute}";

            return CountBasedLimitAsync(key, limit, new TimeSpan(0, 1, 0));
        }

        public Task PerSecondLimitAsync(string key, int limit)
        {
            key += $":Second:{Clock.Now.Second}";

            return CountBasedLimitAsync(key, limit, new TimeSpan(0, 0, 1));
        }

        private async Task CountBasedLimitAsync(string key, int limit, TimeSpan timeSpan)
        {
            if (limit < 1)
            {
                throw new ArgumentException("limit need greater or equal than 1.", nameof(limit));
            }

            var rqs = await _database.StringIncrementAsync(key, 1);
            if (rqs > limit)
            {
                throw new RedisLimitException("Rate limit exceeded.");
            }

            await _database.KeyExpireAsync(key, timeSpan);
        }
    }
}