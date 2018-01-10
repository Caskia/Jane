using Jane.Dependency;
using Jane.Runtime.Security;
using System;
using System.Linq;

namespace Jane.Runtime.Session
{
    /// <summary>
    /// Implements <see cref="IJaneSession"/> to get session properties from current claims.
    /// </summary>
    public class ClaimsJaneSession : JaneSessionBase, ISingletonDependency
    {
        public ClaimsJaneSession(
            IPrincipalAccessor principalAccessor,
            IAmbientScopeProvider<SessionOverride> sessionOverrideScopeProvider)
            : base(
                  sessionOverrideScopeProvider)
        {
            PrincipalAccessor = principalAccessor;
        }

        public override long? ImpersonatorUserId
        {
            get
            {
                var impersonatorUserIdClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == BaseClaimTypes.ImpersonatorUserId);
                if (string.IsNullOrEmpty(impersonatorUserIdClaim?.Value))
                {
                    return null;
                }

                return Convert.ToInt64(impersonatorUserIdClaim.Value);
            }
        }

        public override long? UserId
        {
            get
            {
                if (OverridedValue != null)
                {
                    return OverridedValue.UserId;
                }

                var userIdClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == BaseClaimTypes.UserId);
                if (string.IsNullOrEmpty(userIdClaim?.Value))
                {
                    return null;
                }

                long userId;
                if (!long.TryParse(userIdClaim.Value, out userId))
                {
                    return null;
                }

                return userId;
            }
        }

        protected IPrincipalAccessor PrincipalAccessor { get; }
    }
}