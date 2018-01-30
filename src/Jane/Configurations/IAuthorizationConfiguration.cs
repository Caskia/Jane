using Jane.Authorization;
using Jane.Collections;

namespace Jane.Configurations
{
    /// <summary>
    /// Used to configure authorization system.
    /// </summary>
    public interface IAuthorizationConfiguration
    {
        /// <summary>
        /// Enables/Disables attribute based authentication and authorization.
        /// Default: true.
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// List of authorization providers.
        /// </summary>
        ITypeList<AuthorizationProvider> Providers { get; }
    }
}