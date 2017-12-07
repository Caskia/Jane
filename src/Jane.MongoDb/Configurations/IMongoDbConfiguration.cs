using Jane.Dependency;

namespace Jane.Configurations
{
    public interface IMongoDbConfiguration : ISingletonDependency
    {
        string ConnectionString { get; set; }

        string DatabaseName { get; set; }
    }
}