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

        public static Type[] FindSamePathExceptions(Type pathType)
        {
            return TypeHelper.GetAllSameNamespaceTypes(pathType, typeof(IPublishableException)).ToArray();
        }
    }
}