using Microsoft.AspNetCore.Mvc.Filters;

namespace Jane.AspNetCore.Mvc.Results.Caching
{
    public interface IClientCacheAttribute
    {
        void Apply(ResultExecutingContext context);
    }
}