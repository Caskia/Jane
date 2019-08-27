using Autofac;
using Jane.Dependency;
using System;

namespace Jane.Autofac
{
    public static class ObjectContainerExtensions
    {
        public static void SetContainer(this IObjectContainer objectContainer, ILifetimeScope container)
        {
            if (!(objectContainer is AutofacObjectContainer autofacObjectContainer))
                throw new InvalidOperationException($"Instance of {nameof(ObjectContainer)} is not of type {nameof(AutofacObjectContainer)}");
            autofacObjectContainer.SetContainer(container);
        }
    }
}