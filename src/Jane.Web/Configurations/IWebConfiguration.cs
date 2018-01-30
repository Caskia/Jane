using Jane.Dependency;

namespace Jane.Configurations
{
    public interface IWebConfiguration : ISingletonDependency
    {
        /// <summary>
        /// If this is set to true, all exception and details are sent directly to clients on an error.
        /// Default: false (hides exception details from clients except special exceptions.)
        /// </summary>
        bool SendAllExceptionsToClients { get; set; }
    }
}