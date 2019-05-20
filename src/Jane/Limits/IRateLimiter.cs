using System.Threading.Tasks;

namespace Jane.Limits
{
    public interface IRateLimiter
    {
        Task PerDayLimitAsync(string key, int limit);

        Task PerHourLimitAsync(string key, int limit);

        Task PeriodLimitAsync(LimitPeriod period, string key, int limit);

        Task PerMinuteLimitAsync(string key, int limit);

        Task PerSecondLimitAsync(string key, int limit);
    }
}