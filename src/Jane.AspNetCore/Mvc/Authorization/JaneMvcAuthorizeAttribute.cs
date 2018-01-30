using Jane.Authorization;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Jane.AspNetCore.Mvc.Authorization
{
    /// <summary>
    /// This attribute is used on an action of an MVC <see cref="Controller"/>
    /// to make that action usable only by authorized users.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class JaneMvcAuthorizeAttribute : AuthorizeAttribute, IJaneAuthorizeAttribute
    {
        /// <summary>
        /// Creates a new instance of <see cref="JaneMvcAuthorizeAttribute"/> class.
        /// </summary>
        /// <param name="permissions">A list of permissions to authorize</param>
        public JaneMvcAuthorizeAttribute(params string[] permissions)
        {
            Permissions = permissions;
        }

        /// <inheritdoc/>
        public string[] Permissions { get; set; }

        /// <inheritdoc/>
        public bool RequireAllPermissions { get; set; }
    }
}