using Jane.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Jane.AspNetCore.Mvc.Results.Caching
{
    public class AllowClientCacheAttribute : IClientCacheAttribute
    {
        public AllowClientCacheAttribute()
        {
        }

        public AllowClientCacheAttribute(ClientCacheScope scope)
        {
            Scope = scope;
        }

        public ClientCacheScope? Scope { get; }

        public void Apply(ResultExecutingContext context)
        {
            if (Scope.HasValue)
            {
                context.HttpContext.Response.Headers["Cache-Control"] = Scope.ToString().ToCamelCase();
            }
        }
    }
}