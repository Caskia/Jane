﻿using Jane.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Jane.Repositories
{
    public interface IMongoDbRepository<TEntity, TPrimary> : IRepository<TEntity, TPrimary>
       where TEntity : class, IEntity<TPrimary>
    {
        #region Properties

        string CollectionName { get; }

        #endregion Properties

        #region Select/Get/Query

        TEntity FirstOrDefault(TPrimary id, string projection);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, string projection);

        Task<TEntity> FirstOrDefaultAsync(TPrimary id, string projection);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, string projection);

        List<TEntity> GetAllList(string filter, string sorts, int? skip, int? count);

        List<TEntity> GetAllList(string filter, string projection, string sorts, int? skip, int? count);

        Task<List<TEntity>> GetAllListAsync(string filter, string sorts, int? skip, int? count);

        Task<List<TEntity>> GetAllListAsync(string filter, string projection, string sorts, int? skip, int? count);

        #endregion Select/Get/Query

        #region Insert

        void InsertMany(List<TEntity> entities);

        Task InsertManyAsync(List<TEntity> entities);

        #endregion Insert

        #region Update

        long UpdateMany(string filterDefinition, string updateDefinition, object options = null);

        Task<long> UpdateManyAsync(string filterDefinition, string updateDefinition, object options = null);

        long UpdateOne(string filterDefinition, string updateDefinition, object options = null);

        Task<long> UpdateOneAsync(string filterDefinition, string updateDefinition, object options = null);

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