using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Jane.Authorization
{
    public interface IAuthorizationHelper
    {
        Task AuthorizeAsync(IEnumerable<IJaneAuthorizeAttribute> authorizeAttributes);

        Task AuthorizeAsync(MethodInfo methodInfo, Type type);
    }
}