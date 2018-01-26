using Jane.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Jane.Repositories
{
    public interface IMongoDbRepository<TEntity, TPrimary> : IRepository<TEntity, TPrimary>
       where TEntity : class, IEntity<TPrimary>
    {
        #region Select/Get/Query

        List<TEntity> GetAllList(string filter, string sorts, int? skip, int? count);

        Task<List<TEntity>> GetAllListAsync(string filter, string sorts, int? skip, int? count);

        #endregion Select/Get/Query

        #region Update

        long UpdateMany(string filterDefinition, string updateDefinition);

        Task<long> UpdateManyAsync(string filterDefinition, string updateDefinition);

        long UpdateOne(string filterDefinition, string updateDefinition);

        Task<long> UpdateOneAsync(string filterDefinition, string updateDefinition);

        #endregion Update

        #region Aggregates

        long LongCount(string filter);

        Task<long> LongCountAsync(string filter);

        #endregion Aggregates
    }

    public interface IMongoDbRepository<TEntity> : IMongoDbRepository<TEntity, int>
        where TEntity : class, IEntity<int>
    {
    }
}