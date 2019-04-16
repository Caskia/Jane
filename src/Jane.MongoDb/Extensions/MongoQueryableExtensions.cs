using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jane.MongoDb.Extensions
{
    public static class MongoQueryableExtensions
    {
        public static Task<int> CountByQueryableAsync<TEntity>(this IQueryable<TEntity> queryable)
        {
            return queryable.UsingMongoQueryable(mq => mq.CountAsync());
        }

        public static Task<TEntity> FirstOrDefaultByQueryableAsync<TEntity>(this IQueryable<TEntity> queryable)
        {
            return queryable.UsingMongoQueryable(mq => mq.FirstOrDefaultAsync());
        }

        public static Task<long> LongCountByQueryableAsync<TEntity>(this IQueryable<TEntity> queryable)
        {
            return queryable.UsingMongoQueryable(mq => mq.LongCountAsync());
        }

        public static Task<List<TEntity>> ToListByQueryableAsync<TEntity>(this IQueryable<TEntity> queryable)
        {
            return queryable.UsingMongoQueryable(mq => mq.ToListAsync());
        }

        public static TReturn UsingMongoQueryable<TEntity, TReturn>(this IQueryable<TEntity> queryable, Func<IMongoQueryable<TEntity>, TReturn> func)
        {
            if (queryable is IMongoQueryable<TEntity>)
            {
                return func(queryable as IMongoQueryable<TEntity>);
            }
            else
            {
                throw new JaneException($"[{queryable.GetType()}] is not mongo queryable!");
            }
        }
    }
}