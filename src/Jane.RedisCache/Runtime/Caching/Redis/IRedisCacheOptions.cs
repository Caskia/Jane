using Jane.Dependency;

namespace Jane.Runtime.Caching.Redis
{
    public interface IRedisCacheOptions : ISingletonDependency
    {
        string ConnectionString { get; set; }

        int DatabaseId { get; set; }
    }
}