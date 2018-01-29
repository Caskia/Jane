using Jane.Entities;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Jane.MongoDb.Indexes
{
    public class MongoDbCollectionIndexManager<TEntity, TPrimaryKey> : MongoDbBase<TEntity, TPrimaryKey>, IMongoDbCollectionIndexManager<TEntity, TPrimaryKey>
         where TEntity : class, IEntity<TPrimaryKey>
    {
        public MongoDbCollectionIndexManager(IMongoDbProvider databaseProvider)
            : base(databaseProvider)
        {
            EnsureIndexAsync();
        }

        public virtual IndexKeysDefinitionBuilder<TEntity> IndexKeys
        {
            get
            {
                return Builders<TEntity>.IndexKeys;
            }
        }

        public virtual Task EnsureIndexAsync()
        {
            return Task.FromResult(0);
        }
    }
}