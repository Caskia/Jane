using Jane.Entities;
using Jane.Extensions;
using Jane.MongoDb.Indexes;
using Jane.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Jane.MongoDb.Repositories
{
    /// <summary>
    /// Implements IRepository for MongoDB.
    /// </summary>
    /// <typeparam name="TEntity">Type of the Entity for this repository</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key of the entity</typeparam>
    public class MongoDbRepository<TEntity, TPrimaryKey> : MongoDbBase<TEntity, TPrimaryKey>, IMongoDbRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        private readonly IIdGenerator _idGenerator;

        public MongoDbRepository(
            IMongoDbProvider databaseProvider,
            IMongoDbCollectionIndexManager<TEntity, TPrimaryKey> mongoDbCollectionIndexManager,
            IIdGenerator idGenerator
            )
            : base(databaseProvider)
        {
            _idGenerator = idGenerator;
        }

        #region Select/Get/Query

        public TEntity FirstOrDefault(TPrimaryKey id)
        {
            return Collection.Find(CreateEqualityExpressionForId(id)).FirstOrDefault();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Collection.Find(predicate).FirstOrDefault();
        }

        public async Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
        {
            return await Collection.Find(CreateEqualityExpressionForId(id)).FirstOrDefaultAsync();
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Collection.Find(predicate).FirstOrDefaultAsync();
        }

        public TEntity Get(TPrimaryKey id)
        {
            var entity = FirstOrDefault(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"{typeof(TEntity).Name}[{id}] not found! ");
            }

            return entity;
        }

        public IQueryable<TEntity> GetAll()
        {
            return Collection.AsQueryable();
        }

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> GetAllList()
        {
            return GetAll().ToList();
        }

        public List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return Collection.Find(predicate).ToList();
        }

        public List<TEntity> GetAllList(string filter, string sorts, int? skip, int? count)
        {
            return BuildFinder(filter, sorts, skip, count).ToList();
        }

        public async Task<List<TEntity>> GetAllListAsync(string filter, string sorts, int? skip, int? count)
        {
            return await BuildFinder(filter, sorts, skip, count).ToListAsync();
        }

        public async Task<List<TEntity>> GetAllListAsync()
        {
            return await Collection.AsQueryable().ToListAsync();
        }

        public async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Collection.Find(predicate).ToListAsync();
        }

        public async Task<TEntity> GetAsync(TPrimaryKey id)
        {
            var entity = await FirstOrDefaultAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"{typeof(TEntity).Name}[{id}] not found! ");
            }

            return entity;
        }

        public TEntity Load(TPrimaryKey id)
        {
            return Get(id);
        }

        public T Query<T>(Func<IQueryable<TEntity>, T> queryMethod)
        {
            return queryMethod(GetAll());
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Single(predicate);
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Collection.Find(predicate).SingleAsync();
        }

        #endregion Select/Get/Query

        #region Insert

        public TEntity Insert(TEntity entity)
        {
            SetId(entity);

            Collection.InsertOne(entity);
            return entity;
        }

        public TPrimaryKey InsertAndGetId(TEntity entity)
        {
            return Insert(entity).Id;
        }

        public async Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
        {
            return (await InsertAsync(entity)).Id;
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            SetId(entity);

            await Collection.InsertOneAsync(entity);
            return entity;
        }

        public TEntity InsertOrUpdate(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TPrimaryKey InsertOrUpdateAndGetId(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TPrimaryKey> InsertOrUpdateAndGetIdAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> InsertOrUpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        #endregion Insert

        #region Update

        public TEntity Update(TEntity entity)
        {
            Collection.ReplaceOne(CreateEqualityExpressionForId(entity.Id), entity);
            return entity;
        }

        public TEntity Update(TPrimaryKey id, Action<TEntity> updateAction)
        {
            var entity = Get(id);
            updateAction(entity);
            Collection.ReplaceOne(CreateEqualityExpressionForId(id), entity);
            return entity;
        }

        public TEntity Update(TPrimaryKey id, Func<TEntity, TEntity> updateAction)
        {
            var entity = Get(id);
            entity = updateAction(entity);
            Collection.ReplaceOne(CreateEqualityExpressionForId(id), entity);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await Collection.ReplaceOneAsync(CreateEqualityExpressionForId(entity.Id), entity);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction)
        {
            var entity = Get(id);
            await updateAction(entity);
            await Collection.ReplaceOneAsync(CreateEqualityExpressionForId(id), entity);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task<TEntity>> updateAction)
        {
            var entity = await GetAsync(id);
            entity = await updateAction(entity);
            await Collection.ReplaceOneAsync(CreateEqualityExpressionForId(id), entity);
            return entity;
        }

        public long UpdateMany(string filterDefinition, string updateDefinition)
        {
            var result = Collection.UpdateMany(filterDefinition, updateDefinition);
            return result.ModifiedCount;
        }

        public async Task<long> UpdateManyAsync(string filterDefinition, string updateDefinition)
        {
            var result = await Collection.UpdateManyAsync(filterDefinition, updateDefinition);
            return result.ModifiedCount;
        }

        public long UpdateOne(string filterDefinition, string updateDefinition)
        {
            var result = Collection.UpdateOne(filterDefinition, updateDefinition);
            return result.ModifiedCount;
        }

        public async Task<long> UpdateOneAsync(string filterDefinition, string updateDefinition)
        {
            var result = await Collection.UpdateOneAsync(filterDefinition, updateDefinition);
            return result.ModifiedCount;
        }

        #endregion Update

        #region Delete

        public void Delete(TEntity entity)
        {
            Delete(entity.Id);
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            Collection.DeleteMany(predicate);
        }

        public void Delete(TPrimaryKey id)
        {
            Collection.DeleteOne(CreateEqualityExpressionForId(id));
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await DeleteAsync(entity.Id);
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            await Collection.DeleteManyAsync(predicate);
        }

        public async Task DeleteAsync(TPrimaryKey id)
        {
            await Collection.DeleteOneAsync(CreateEqualityExpressionForId(id));
        }

        #endregion Delete

        #region Aggregates

        public int Count()
        {
            return Collection.AsQueryable().Count();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return Collection.AsQueryable().Count(predicate);
        }

        public Task<int> CountAsync()
        {
            return Task.FromResult(Count());
        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(Count(predicate));
        }

        public long LongCount(string filter)
        {
            return Collection.Find(filter).Count();
        }

        public long LongCount()
        {
            return Collection.AsQueryable().LongCount();
        }

        public long LongCount(Expression<Func<TEntity, bool>> predicate)
        {
            return Collection.AsQueryable().LongCount(predicate);
        }

        public async Task<long> LongCountAsync(string filter)
        {
            return await Collection.Find(filter).CountAsync();
        }

        public async Task<long> LongCountAsync()
        {
            var filter = Builders<TEntity>.Filter.Empty;
            return await Collection.CountAsync(filter);
        }

        public async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Collection.CountAsync(predicate);
        }

        #endregion Aggregates

        protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(TPrimaryKey))
                );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }

        protected IFindFluent<TEntity, TEntity> BuildFinder(string filter, string sorts, int? skip, int? count)
        {
            var finder = Collection.Find(filter);

            if (!sorts.IsNullOrEmpty())
            {
                var sortDetail = ConvertSorts(sorts);

                var sorter = Builders<TEntity>.Sort.Descending("_id");

                SortDefinition<TEntity> sortDefinition = null;

                foreach (var sortForAscending in sortDetail.sortsForAscending)
                {
                    if (sortDefinition == null)
                    {
                        sortDefinition = sorter.Ascending(sortForAscending);
                    }
                    else
                    {
                        sortDefinition = sortDefinition.Ascending(sortForAscending);
                    }
                }

                foreach (var sortForDescending in sortDetail.sortsForDescending)
                {
                    if (sortDefinition == null)
                    {
                        sortDefinition = sorter.Descending(sortForDescending);
                    }
                    else
                    {
                        sortDefinition = sortDefinition.Descending(sortForDescending);
                    }
                }

                finder = finder.Sort(sorter);
            }

            if (skip.HasValue)
            {
                finder = finder.Skip(skip);
            }
            if (count.HasValue)
            {
                finder = finder.Limit(count);
            }

            return finder;
        }

        protected (List<string> sortsForAscending, List<string> sortsForDescending) ConvertSorts(string sortFormat)
        {
            var sortsForAscending = new List<string>();
            var sortsForDescending = new List<string>();

            var sorts = sortFormat.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var sortPair in sorts)
            {
                var pairs = sortPair.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (pairs.Length > 2)
                {
                    throw new ArgumentException(nameof(sortFormat));
                }

                if (pairs.Length == 2)
                {
                    if (pairs[1].ToLower() == "desc")
                    {
                        sortsForDescending.Add(pairs[0]);
                    }
                    else if (pairs[1].ToLower() == "asc")
                    {
                        sortsForAscending.Add(pairs[0]);
                    }
                    else
                    {
                        throw new ArgumentException(nameof(sortFormat));
                    }
                }
                else
                {
                    sortsForAscending.Add(pairs[0]);
                }
            }

            return (sortsForAscending, sortsForDescending);
        }

        protected void SetId(TEntity entityAsObject)
        {
            var entity = entityAsObject as IEntity<long>;
            if (entity != null && entity.Id <= 0)
            {
                entity.Id = _idGenerator.NextId();
            }
        }
    }

    /// <summary>
    /// Implements IRepository for MongoDB.
    /// </summary>
    /// <typeparam name="TEntity">Type of the Entity for this repository</typeparam>
    public class MongoDbRepository<TEntity> : MongoDbRepository<TEntity, int>, IMongoDbRepository<TEntity>
        where TEntity : class, IEntity<int>
    {
        public MongoDbRepository(
            IMongoDbProvider databaseProvider,
            IMongoDbCollectionIndexManager<TEntity, int> mongoDbCollectionIndexManager,
            IIdGenerator idGenerator)
            : base(databaseProvider, mongoDbCollectionIndexManager, idGenerator)
        {
        }
    }
}