using Jane.Dependency;
using System.Security.Claims;
using System.Threading;

namespace Jane.Runtime.Session
{
    public class DefaultPrincipalAccessor : IPrincipalAccessor, ISingletonDependency
    {
        public static DefaultPrincipalAccessor Instance => new DefaultPrincipalAccessor();
        public virtual ClaimsPrincipal Principal => Thread.CurrentPrincipal as ClaimsPrincipal;
    }
}