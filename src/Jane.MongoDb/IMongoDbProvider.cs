using Jane.Dependency;
using MongoDB.Driver;

namespace Jane.MongoDb
{
    public interface IMongoDbProvider : ITransientDependency
    {
        IMongoClient GetClient();

        IMongoDatabase GetDatabase();
    }
}