using Jane.Dependency;
using MongoDB.Driver;

namespace Jane.MongoDb
{
    public interface IMongoDbProvider : ISingletonDependency
    {
        IMongoClient GetClient();

        IMongoDatabase GetDatabase();
    }
}