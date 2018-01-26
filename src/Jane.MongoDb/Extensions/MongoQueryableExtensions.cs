using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jane.MongoDb.Extensions
{
    public static class MongoQueryableExtensions
    {
        public static async Task<int> CountAsync<TEntity>(this IQueryable<TEntity> queryable)
        {
            return await queryable.UsingMongoQueryable(async mq => await mq.CountAsync());
        }

        public static async Task<long> LongCountAsync<TEntity>(this IQueryable<TEntity> queryable)
        {
            return await queryable.UsingMongoQueryable(async mq => await mq.LongCountAsync());
        }

        public static async Task<List<TEntity>> ToListAsync<TEntity>(this IQueryable<TEntity> queryable)
        {
            return await queryable.UsingMongoQueryable(async mq => await mq.ToListAsync());
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