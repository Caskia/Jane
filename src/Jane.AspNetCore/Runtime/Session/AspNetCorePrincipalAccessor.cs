using System.Security.Claims;
using Jane.Runtime.Session;
using Microsoft.AspNetCore.Http;

namespace Jane.AspNetCore.Runtime.Session
{
    public class AspNetCorePrincipalAccessor : DefaultPrincipalAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AspNetCorePrincipalAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override ClaimsPrincipal Principal => _httpContextAccessor.HttpContext?.User ?? base.Principal;
    }
}