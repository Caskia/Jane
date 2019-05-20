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
            key += ":Day";

            return TimeBasedLimitAsync(key, limit, new TimeSpan(24, 0, 0));
        }

        public Task PerHourLimitAsync(string key, int limit)
        {
            key += ":Hour";

            return TimeBasedLimitAsync(key, limit, new TimeSpan(1, 0, 0));
        }

        public Task PerMinuteLimitAsync(string key, int limit)
        {
            key += ":Minute";

            return TimeBasedLimitAsync(key, limit, new TimeSpan(0, 1, 0));
        }

        public Task PerSecondLimitAsync(string key, int limit)
        {
            key += ":Second";

            return TimeBasedLimitAsync(key, limit, new TimeSpan(0, 0, 1));
        }

        private async Task TimeBasedLimitAsync(string key, int limit, TimeSpan timeSpan)
        {
            var rqs = await _database.ListLengthAsync(key);
            if (rqs <= limit)
            {
                await _database.ListRightPushAsync(key, BitConverter.GetBytes(Clock.Now.Ticks));
            }
            else
            {
                var time = new DateTime(BitConverter.ToInt64(await _database.ListGetByIndexAsync(key, -1), 0), DateTimeKind.Utc);
                if (Clock.Now - time < timeSpan)
                {
                    throw new RedisLimitException("Rate limit exceeded.");
                }
                else
                {
                    await _database.ListRightPushAsync(key, BitConverter.GetBytes(Clock.Now.Ticks));
                }
            }

            await _database.ListTrimAsync(key, 0, limit);
        }
    }
}