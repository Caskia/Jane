using Jane.Dependency;

namespace Jane.ObjectMapping
{
    public sealed class NullObjectMapper : IObjectMapper, ISingletonDependency
    {
        private static readonly NullObjectMapper SingletonInstance = new NullObjectMapper();

        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static NullObjectMapper Instance { get { return SingletonInstance; } }

        public TDestination Map<TDestination>(object source)
        {
            throw new BaseException("Jane.ObjectMapping.IObjectMapper should be implemented in order to map objects.");
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            throw new BaseException("Jane.ObjectMapping.IObjectMapper should be implemented in order to map objects.");
        }
    }
}