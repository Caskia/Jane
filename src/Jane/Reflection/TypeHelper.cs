using Jane.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Jane.Reflection
{
    /// <summary>
    /// Some simple type-checking methods used internally.
    /// </summary>
    public static class TypeHelper
    {
        public static IEnumerable<Type> GetAllSameNamespaceTypes(Type pathType, Type mappingInterface)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                 .SelectMany(x => x.GetMappingTypes(mappingInterface))
                 .Where(x => x.Namespace == pathType.Namespace);
        }

        public static IEnumerable<Type> GetMappingTypes(this Assembly assembly, Type mappingInterface)
        {
            return assembly.GetTypes().Where(x => TypeUtils.IsClassAssignableFrom(x, mappingInterface));
        }

        public static bool IsFunc(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var type = obj.GetType();
            if (!type.GetTypeInfo().IsGenericType)
            {
                return false;
            }

            return type.GetGenericTypeDefinition() == typeof(Func<>);
        }

        public static bool IsFunc<TReturn>(object obj)
        {
            return obj != null && obj.GetType() == typeof(Func<TReturn>);
        }

        public static bool IsPrimitiveExtendedIncludingNullable(Type type, bool includeEnums = false)
        {
            if (IsPrimitiveExtended(type, includeEnums))
            {
                return true;
            }

            if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return IsPrimitiveExtended(type.GenericTypeArguments[0], includeEnums);
            }

            return false;
        }

        private static bool IsPrimitiveExtended(Type type, bool includeEnums)
        {
            if (type.GetTypeInfo().IsPrimitive)
            {
                return true;
            }

            if (includeEnums && type.GetTypeInfo().IsEnum)
            {
                return true;
            }

            return type == typeof(string) ||
                   type == typeof(decimal) ||
                   type == typeof(DateTime) ||
                   type == typeof(DateTimeOffset) ||
                   type == typeof(TimeSpan) ||
                   type == typeof(Guid);
        }
    }
}