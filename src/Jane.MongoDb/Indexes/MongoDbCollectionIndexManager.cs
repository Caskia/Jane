using Jane.Entities;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Jane.MongoDb.Indexes
{
    public class MongoDbCollectionIndexManager<TEntity, TPrimaryKey> : IMongoDbCollectionIndexManager<TEntity, TPrimaryKey>
         where TEntity : class, IEntity<TPrimaryKey>
    {
        private readonly IMongoDbProvider _databaseProvider;

        public MongoDbCollectionIndexManager(IMongoDbProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
            EnsureIndexAsync();
        }

        public virtual IMongoCollection<TEntity> Collection
        {
            get
            {
                return Database.GetCollection<TEntity>(typeof(TEntity).Name + "s");
            }
        }

        public virtual IMongoDatabase Database
        {
            get { return _databaseProvider.GetDatabase(); }
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