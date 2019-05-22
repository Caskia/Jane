using Jane.Runtime.Caching;
using Jane.Threading;
using Jane.Timing;
using System;
using System.Threading.Tasks;

namespace Jane.Limits
{
    public class MemoryRateLimiter : IRateLimiter
    {
        private readonly AsyncLock _asyncLock;
        private readonly ICacheManager _cacheManager;

        public MemoryRateLimiter(ICacheManager cacheManager)
        {
            _asyncLock = new AsyncLock();
            _cacheManager = cacheManager;
        }

        public Task PerDayLimitAsync(string key, int limit)
        {
            var now = GetChinaLocalTime();
            var temp = now.AddDays(1);
            var resetTime = new DateTimeOffset(temp.Year, temp.Month, temp.Day, 0, 0, 0, GetChinaOffset());

            key += $":Day:{now.Day}";

            return CountBasedLimitAsync(key, limit, new TimeSpan(24, 0, 0), resetTime);
        }

        public Task PerHourLimitAsync(string key, int limit)
        {
            var now = GetChinaLocalTime();
            var temp = now.AddHours(1);
            var resetTime = new DateTimeOffset(temp.Year, temp.Month, temp.Day, temp.Hour, 0, 0, GetChinaOffset());

            key += $":Hour:{Clock.Now.Hour}";

            return CountBasedLimitAsync(key, limit, new TimeSpan(1, 0, 0), resetTime);
        }

        public Task PeriodLimitAsync(LimitPeriod period, string key, int limit)
        {
            switch (period)
            {
                case LimitPeriod.Second:
                    return PerSecondLimitAsync(key, limit);

                case LimitPeriod.Minute:
                    return PerMinuteLimitAsync(key, limit);

                case LimitPeriod.Hour:
                    return PerHourLimitAsync(key, limit);

                case LimitPeriod.Day:
                    return PerDayLimitAsync(key, limit);

                default:
                    throw new NotSupportedException($"{period} not support.");
            }
        }

        public Task PerMinuteLimitAsync(string key, int limit)
        {
            var now = GetChinaLocalTime();
            var temp = now.AddMinutes(1);
            var resetTime = new DateTimeOffset(temp.Year, temp.Month, temp.Day, temp.Hour, temp.Minute, 0, GetChinaOffset());

            key += $":Minute:{Clock.Now.Minute}";

            return CountBasedLimitAsync(key, limit, new TimeSpan(0, 1, 0), resetTime);
        }

        public Task PerSecondLimitAsync(string key, int limit)
        {
            var now = GetChinaLocalTime();
            var temp = now.AddSeconds(1);
            var resetTime = new DateTimeOffset(temp.Year, temp.Month, temp.Day, temp.Hour, temp.Minute, temp.Second, GetChinaOffset());

            key += $":Second:{Clock.Now.Second}";

            return CountBasedLimitAsync(key, limit, new TimeSpan(0, 0, 1), resetTime);
        }

        private async Task CountBasedLimitAsync(string key, int limit, TimeSpan timeSpan, DateTimeOffset resetTime)
        {
            if (limit < 1)
            {
                throw new ArgumentException("limit need greater or equal than 1.", nameof(limit));
            }

            using (await _asyncLock.LockAsync())
            {
                var rqs = await _cacheManager.GetCache("JaneRateLimit").IncrementAsync(key, 1);
                if (rqs > limit)
                {
                    throw new JaneRateLimitException(limit, resetTime, "Rate limit exceeded.");
                }

                await _cacheManager.GetCache("JaneRateLimit").SetAsync(key, rqs, absoluteExpireTime: timeSpan);
            }
        }

        private DateTime GetChinaLocalTime()
        {
            return Clock.Now.AddHours(8);
        }

        private TimeSpan GetChinaOffset()
        {
            return new TimeSpan(0, 8, 0);
        }
    }
}