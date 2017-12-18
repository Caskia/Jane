using Jane.Dependency;
using Jane.Events.Bus.Handlers;
using System;

namespace Jane.Utils
{
    public class TypeUtils
    {
        public static bool IsClassAssignableFrom(Type classType, Type type)
        {
            return classType.IsClass && !classType.IsAbstract && type.IsAssignableFrom(classType);
        }

        /// <summary>
        /// Check whether a type is EventHandler
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsEventHandler(Type type)
        {
            return IsClassAssignableFrom(type, typeof(IEventHandler));
        }

        /// <summary>
        /// Check whether a type is a singleton.
        /// </summary>
        public static bool IsSingleton(Type type)
        {
            return IsClassAssignableFrom(type, typeof(ISingletonDependency));
        }

        /// <summary>
        /// Check whether a type is a transient.
        /// </summary>
        public static bool IsTransient(Type type)
        {
            return IsClassAssignableFrom(type, typeof(ITransientDependency));
        }
    }
}