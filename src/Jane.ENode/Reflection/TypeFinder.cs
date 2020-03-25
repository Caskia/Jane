using ENode.Commanding;
using ENode.Eventing;
using ENode.Infrastructure;
using Jane.Reflection;
using System;
using System.Linq;

namespace Jane.ENode.Reflection
{
    public static class TypeFinder
    {
        public static Type[] GetSamePathApplicationMessages(Type pathType)
        {
            return TypeHelper.GetAllSameNamespaceTypes(pathType, typeof(IApplicationMessage)).ToArray();
        }

        public static Type[] GetSamePathCommands(Type pathType)
        {
            return TypeHelper.GetAllSameNamespaceTypes(pathType, typeof(ICommand)).ToArray();
        }

        public static Type[] GetSamePathDomainEvents(Type pathType)
        {
            return TypeHelper.GetAllSameNamespaceTypes(pathType, typeof(IDomainEvent)).ToArray();
        }

        public static Type[] GetSamePathExceptions(Type pathType)
        {
            return TypeHelper.GetAllSameNamespaceTypes(pathType, typeof(IPublishableException)).ToArray();
        }
    }
}