using System;
using Jane.Dependency;
using Jane.Events.Bus.Handlers;

namespace Jane.Events.Bus.Factories
{
    /// <summary>
    /// This <see cref="IEventHandlerFactory"/> implementation is used to get/release
    /// handlers using Ioc.
    /// </summary>
    public class IocHandlerFactory : IEventHandlerFactory
    {
        private readonly IObjectContainer _objectContainer;

        /// <summary>
        /// Creates a new instance of <see cref="IocHandlerFactory"/> class.
        /// </summary>
        /// <param name="objectContainer"></param>
        /// <param name="handlerType">Type of the handler</param>
        public IocHandlerFactory(IObjectContainer objectContainer, Type handlerType)
        {
            _objectContainer = objectContainer;
            HandlerType = handlerType;
        }

        /// <summary>
        /// Type of the handler.
        /// </summary>
        public Type HandlerType { get; }

        /// <summary>
        /// Resolves handler object from Ioc container.
        /// </summary>
        /// <returns>Resolved handler object</returns>
        public IEventHandler GetHandler()
        {
            return (IEventHandler)_objectContainer.Resolve(HandlerType);
        }

        public Type GetHandlerType()
        {
            return HandlerType;
        }

        /// <summary>
        /// Releases handler object using Ioc container.
        /// </summary>
        /// <param name="handler">Handler to be released</param>
        public void ReleaseHandler(IEventHandler handler)
        {
            _objectContainer.Release(handler);
        }
    }
}