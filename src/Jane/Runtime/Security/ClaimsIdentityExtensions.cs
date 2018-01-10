using Jane.Extensions;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace Jane.Runtime.Security
{
    public static class ClaimsIdentityExtensions
    {
        public static long? GetImpersonatorUserId(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }

            var claimsIdentity = identity as ClaimsIdentity;

            var userIdOrNull = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == BaseClaimTypes.ImpersonatorUserId);
            if (userIdOrNull == null || userIdOrNull.Value.IsNullOrWhiteSpace())
            {
                return null;
            }

            return Convert.ToInt64(userIdOrNull.Value);
        }

        public static long? GetUserId(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }

            var claimsIdentity = identity as ClaimsIdentity;

            var userIdOrNull = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == BaseClaimTypes.UserId);
            if (userIdOrNull == null || userIdOrNull.Value.IsNullOrWhiteSpace())
            {
                return null;
            }

            return Convert.ToInt64(userIdOrNull.Value);
        }
    }
}