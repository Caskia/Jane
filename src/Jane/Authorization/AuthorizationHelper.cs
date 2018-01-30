using Jane.Dependency;
using Jane.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Jane.Configurations;
using Jane.Runtime.Session;

namespace Jane.Authorization
{
    internal class AuthorizationHelper : IAuthorizationHelper
    {
        private readonly IAuthorizationConfiguration _authConfiguration;

        public AuthorizationHelper(IAuthorizationConfiguration authConfiguration)
        {
            _authConfiguration = authConfiguration;
        }

        public IJaneSession JaneSession { get; set; }

        public Task AuthorizeAsync(IEnumerable<IJaneAuthorizeAttribute> authorizeAttributes)
        {
            if (!_authConfiguration.IsEnabled)
            {
                return Task.CompletedTask;
            }

            if (!JaneSession.UserId.HasValue)
            {
                throw new JaneAuthorizationException(
                    "Current user did not login to the application!"
                    );
            }

            return Task.CompletedTask;
            //foreach (var authorizeAttribute in authorizeAttributes)
            //{
            //    await PermissionChecker.AuthorizeAsync(authorizeAttribute.RequireAllPermissions, authorizeAttribute.Permissions);
            //}
        }

        public async Task AuthorizeAsync(MethodInfo methodInfo, Type type)
        {
            //await CheckFeatures(methodInfo, type);
            await CheckPermissions(methodInfo, type);
        }

        private static bool AllowAnonymous(MemberInfo memberInfo, Type type)
        {
            return ReflectionHelper
                .GetAttributesOfMemberAndType(memberInfo, type)
                .OfType<IJaneAllowAnonymousAttribute>()
                .Any();
        }

        //private async Task CheckFeatures(MethodInfo methodInfo, Type type)
        //{
        //    var featureAttributes = ReflectionHelper.GetAttributesOfMemberAndType<RequiresFeatureAttribute>(methodInfo, type);

        //    if (featureAttributes.Count <= 0)
        //    {
        //        return;
        //    }

        //    foreach (var featureAttribute in featureAttributes)
        //    {
        //        await _featureChecker.CheckEnabledAsync(featureAttribute.RequiresAll, featureAttribute.Features);
        //    }
        //}

        private async Task CheckPermissions(MethodInfo methodInfo, Type type)
        {
            if (!_authConfiguration.IsEnabled)
            {
                return;
            }

            if (AllowAnonymous(methodInfo, type))
            {
                return;
            }

            var authorizeAttributes =
                ReflectionHelper
                    .GetAttributesOfMemberAndType(methodInfo, type)
                    .OfType<IJaneAuthorizeAttribute>()
                    .ToArray();

            if (!authorizeAttributes.Any())
            {
                return;
            }

            await AuthorizeAsync(authorizeAttributes);
        }
    }
}