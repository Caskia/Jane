using Jane.Runtime.Caching.Redis;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Jane.HealthChecks
{
    public class RedisHealthCheck : IHealthCheck
    {
        private readonly IRedisCacheDatabaseProvider _redisCacheDatabaseProvider;

        public RedisHealthCheck(IRedisCacheDatabaseProvider redisCacheDatabaseProvider)
        {
            _redisCacheDatabaseProvider = redisCacheDatabaseProvider;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var ping = await _redisCacheDatabaseProvider.GetDatabase().PingAsync();

                return HealthCheckResult.Healthy($"{context.Registration.Name}: Redis.Connected");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy(
                   $"{context.Registration.Name}: Exception {ex.GetType().FullName}",
                   ex);
            }
        }
    }
}