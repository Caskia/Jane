using Jane.Entities;
using System.Threading.Tasks;

namespace Jane.Repositories
{
    public interface IMongoDbRepository<TEntity, TPrimary> : IRepository<TEntity, TPrimary>
       where TEntity : class, IEntity<TPrimary>
    {
        #region Update

        long UpdateMany(string filterDefinition, string updateDefinition);

        Task<long> UpdateManyAsync(string filterDefinition, string updateDefinition);

        long UpdateOne(string filterDefinition, string updateDefinition);

        Task<long> UpdateOneAsync(string filterDefinition, string updateDefinition);

        #endregion Update
    }

    public interface IMongoDbRepository<TEntity> : IMongoDbRepository<TEntity, int>
        where TEntity : class, IEntity<int>
    {
    }
}