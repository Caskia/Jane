using System;

namespace Jane.Authorization
{
    /// <summary>
    /// Used to allow a method to be accessed by any user.
    /// Suppress <see cref="JaneAuthorizeAttribute"/> defined in the class containing that method.
    /// </summary>
    public class JaneAllowAnonymousAttribute : Attribute, IJaneAllowAnonymousAttribute
    {
    }
}