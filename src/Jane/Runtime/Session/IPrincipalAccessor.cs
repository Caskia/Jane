using System.Security.Claims;

namespace Jane.Runtime.Session
{
    public interface IPrincipalAccessor
    {
        ClaimsPrincipal Principal { get; }
    }
}
