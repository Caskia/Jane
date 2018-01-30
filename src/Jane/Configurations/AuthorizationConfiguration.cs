using Jane.Authorization;
using Jane.Collections;

namespace Jane.Configurations
{
    internal class AuthorizationConfiguration : IAuthorizationConfiguration
    {
        public AuthorizationConfiguration()
        {
            Providers = new TypeList<AuthorizationProvider>();
            IsEnabled = true;
        }

        public bool IsEnabled { get; set; }

        public ITypeList<AuthorizationProvider> Providers { get; }
    }
}