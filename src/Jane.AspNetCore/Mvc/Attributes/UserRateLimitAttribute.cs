using Jane.Dependency;
using Jane.Limits;
using Jane.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace Jane.AspNetCore.Mvc.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class UserRateLimitAttribute : ActionFilterAttribute
    {
        private readonly string _alias;
        private readonly int _limit;
        private readonly LimitPeriod _period;

        public UserRateLimitAttribute(LimitPeriod period, int limit, string alias)
        {
            _period = period;
            _limit = limit;
            _alias = alias;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var rateLimiter = context.HttpContext.RequestServices.GetService(typeof(IRateLimiter)) as IRateLimiter;
            await rateLimiter.PeriodLimitAsync(_period, $"{_alias}:{ObjectContainer.Resolve<IJaneSession>().UserId}", _limit);

            await next();
        }
    }
}