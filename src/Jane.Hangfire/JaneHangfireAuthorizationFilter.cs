using Hangfire.Dashboard;
using Jane.Dependency;
using Jane.Runtime.Session;

namespace Jane.Hangfire
{
    public class JaneHangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        private readonly string _requiredPermissionName;

        public JaneHangfireAuthorizationFilter(string requiredPermissionName = null)
        {
            _requiredPermissionName = requiredPermissionName;
        }

        public bool Authorize(DashboardContext context)
        {
            if (!IsLoggedIn())
            {
                return false;
            }

            return true;
        }

        private bool IsLoggedIn()
        {
            return ObjectContainer.Resolve<IJaneSession>().UserId.HasValue;
        }
    }
}