using Jane.MongoDb;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jane.Environment
{
    public class MongoDbIncrementDataGenerator : MongoDbBase<JaneIncrementField, ObjectId>, IIncrementDataGenerator
    {
        public MongoDbIncrementDataGenerator(IMongoDbProvider databaseProvider)
            : base(databaseProvider)
        {
            Task.Factory.StartNew(async () => await EnsureIndexAsync());
        }

        public async Task<long> IncrementAsync(string key, int value = 1)
        {
            var filter = Builders<JaneIncrementField>
                .Filter
                .Eq(i => i.Key, key);

            var update = Builders<JaneIncrementField>
                .Update
                .Inc(i => i.Value, value)
                .SetOnInsert(i => i.Key, key);

            var projection = Builders<JaneIncrementField>
                .Projection;

            var options = new FindOneAndUpdateOptions<JaneIncrementField>()
            {
                IsUpsert = true
            };

            var incrementField = await Collection.FindOneAndUpdateAsync(
                filter,
                update,
                options
                );

            if (incrementField == null)
            {
                return value;
            }
            else
            {
                return incrementField.Value + value;
            }
        }

        private async Task EnsureIndexAsync()
        {
            await Collection.Indexes.CreateManyAsync(new List<CreateIndexModel<JaneIncrementField>>()
            {
                new CreateIndexModel<JaneIncrementField>(Builders<JaneIncrementField>.IndexKeys.Ascending(f=>f.Key)),
            });
        }
    }
}