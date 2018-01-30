using Jane.Application.Services;
using Jane.Dependency;
using Jane.Utils;
using System;

namespace Jane.Runtime.Validation.Interception
{
    internal static class ValidationInterceptorRegistrar
    {
        public static Type GetInterceptor(Type type)
        {
            if (TypeUtils.IsClassAssignableFrom(type, typeof(INeedValidationService)))
            {
                return typeof(ValidationInterceptor);
            }
            return null;
        }

        public static void Initialize(IObjectContainer objectConainter)
        {
            objectConainter.RegisterType(typeof(ValidationInterceptor), null, DependencyLifeStyle.Transient);
        }
    }
}