using Jane.Dependency;
using Jane.Entities;
using System.Threading.Tasks;

namespace Jane.MongoDb.Indexes
{
    public interface IMongoDbCollectionIndexManager<TEntity, TPrimary> : ITransientDependency
         where TEntity : class, IEntity<TPrimary>
    {
        Task EnsureIndexAsync();
    }
}