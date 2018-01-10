using System.Security.Claims;

namespace Jane.Runtime.Security
{
    /// <summary>
    /// Used to get base-specific claim type names.
    /// </summary>
    public static class BaseClaimTypes
    {
        /// <summary>
        /// ImpersonatorUserId.
        /// Default: http://jane.io/identity/claims/impersonatorUserId
        /// </summary>
        public static string ImpersonatorUserId { get; set; } = "http://jane.io/identity/claims/impersonatorUserId";

        /// <summary>
        /// UserId.
        /// Default: <see cref="ClaimTypes.Role"/>
        /// </summary>
        public static string Role { get; set; } = ClaimTypes.Role;

        /// <summary>
        /// UserId.
        /// Default: <see cref="ClaimTypes.NameIdentifier"/>
        /// </summary>
        public static string UserId { get; set; } = ClaimTypes.NameIdentifier;

        /// <summary>
        /// UserId.
        /// Default: <see cref="ClaimTypes.Name"/>
        /// </summary>
        public static string UserName { get; set; } = ClaimTypes.Name;
    }
}