using Jane.Application.Services;
using Jane.Dependency;
using Jane.Runtime.Validation.Interception;
using Jane.Utils;
using System;
using System.Collections.Generic;

namespace Jane.Aspects
{
    internal static class ComponentRegistrar
    {
        public static void RegisterType(Type implementationType, string serviceName, DependencyLifeStyle life = DependencyLifeStyle.Singleton)
        {
            ObjectContainer.RegisterTypeWithInterceptors(implementationType, GetClassInterceptors(implementationType), serviceName, life);
        }

        public static void RegisterType(Type serviceType, Type implementationType, string serviceName, DependencyLifeStyle life = DependencyLifeStyle.Singleton)
        {
            ObjectContainer.RegisterTypeWithInterceptors(serviceType, implementationType, GetClassInterceptors(implementationType), serviceName, life);
        }

        private static Type[] GetClassInterceptors(Type type)
        {
            List<Type> types = new List<Type>();
            if (TypeUtils.IsClassAssignableFrom(type, typeof(INeedValidationService)))
            {
                types.Add(typeof(ValidationInterceptor));
            }
            return types.ToArray();
        }
    }
}