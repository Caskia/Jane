using MongoDB.Driver;

namespace Jane.MongoDb
{
    public abstract class MongoDbBase<TEntity, TPrimaryKey>
    {
        private readonly IMongoDbProvider _databaseProvider;

        public MongoDbBase(IMongoDbProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
        }

        public virtual IMongoCollection<TEntity> Collection
        {
            get
            {
                return Database.GetCollection<TEntity>(CollectionName);
            }
        }

        public virtual string CollectionName
        {
            get
            {
                var entityName = typeof(TEntity).Name;
                return entityName.EndsWith("s") ? entityName : (entityName + "s");
            }
        }

        public virtual IMongoDatabase Database
        {
            get { return _databaseProvider.GetDatabase(); }
        }
    }
}