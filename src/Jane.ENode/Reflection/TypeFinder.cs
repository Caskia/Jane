using ENode.Commanding;
using ENode.Domain;
using ENode.Eventing;
using ENode.Messaging;
using Jane.Reflection;
using System;
using System.Linq;

namespace Jane.ENode.Reflection
{
    public static class TypeFinder
    {
        public static Type[] FindSamePathApplicationMessages(Type pathType)
        {
            return TypeHelper.GetAllSameNamespaceTypes(pathType, typeof(IApplicationMessage)).ToArray();
        }

        public static Type[] FindSamePathCommands(Type pathType)
        {
            return TypeHelper.GetAllSameNamespaceTypes(pathType, typeof(ICommand)).ToArray();
        }

        public static Type[] FindSamePathDomainEvents(Type pathType)
        {
            return TypeHelper.GetAllSameNamespaceTypes(pathType, typeof(IDomainEvent)).ToArray();
        }

        public static Type[] FindSamePathDomainExceptions(Type pathType)
        {
            return TypeHelper.GetAllSameNamespaceTypes(pathType, typeof(IDomainException)).ToArray();
        }

        public static Type[] FindSamePathTypes(Type pathType)
        {
            var interfaces = pathType.GetInterfaces();

            if (interfaces.Contains(typeof(IApplicationMessage)))
            {
                return FindSamePathApplicationMessages(pathType);
            }
            else if (interfaces.Contains(typeof(ICommand)))
            {
                return FindSamePathCommands(pathType);
            }
            else if (interfaces.Contains(typeof(IDomainEvent)))
            {
                return FindSamePathDomainEvents(pathType);
            }
            else if (interfaces.Contains(typeof(IDomainException)))
            {
                return FindSamePathDomainExceptions(pathType);
            }

            return null;
        }
    }
}