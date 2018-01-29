using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;

namespace Jane.MongoDb.Extensions
{
    public static class BsonDocumentExtensions
    {
        public static string ToBsonDocumentString<TNominalType>(this TNominalType obj, IBsonSerializer<TNominalType> serializer = null, Action<BsonSerializationContext.Builder> configurator = null, BsonSerializationArgs args = default(BsonSerializationArgs))
        {
            return obj.ToBsonDocument<TNominalType>(serializer, configurator, args).ToString();
        }

        public static string ToBsonDocumentString<T>(this FilterDefinition<T> filter)
        {
            var serializerRegistry = BsonSerializer.SerializerRegistry;
            var documentSerializer = serializerRegistry.GetSerializer<T>();
            return filter.Render(documentSerializer, serializerRegistry).ToString();
        }

        public static string ToBsonDocumentString(this object obj, Type nominalType, IBsonSerializer serializer = null, Action<BsonSerializationContext.Builder> configurator = null, BsonSerializationArgs args = default(BsonSerializationArgs))
        {
            return obj.ToBsonDocument(nominalType, serializer, configurator, args).ToString();
        }

        public static BsonDocument ToUpdateBsonDocument<TNominalType>(this TNominalType obj, IBsonSerializer<TNominalType> serializer = null, Action<BsonSerializationContext.Builder> configurator = null, BsonSerializationArgs args = default(BsonSerializationArgs), params string[] removeNames)
        {
            return obj.ToBsonDocument<TNominalType>(serializer, configurator, args).ToUpdateBsonDocument(removeNames);
        }

        public static BsonDocument ToUpdateBsonDocument(this object obj, Type nominalType, IBsonSerializer serializer = null, Action<BsonSerializationContext.Builder> configurator = null, BsonSerializationArgs args = default(BsonSerializationArgs), params string[] removeNames)
        {
            return obj.ToBsonDocument(nominalType, serializer, configurator, args).ToUpdateBsonDocument(removeNames);
        }

        public static BsonDocument ToUpdateBsonDocument(this BsonDocument doc, params string[] removeNames)
        {
            doc.Remove("_id");
            doc.Remove("CreationTime");
            doc.Remove("CreatorUserId");

            foreach (var name in removeNames)
            {
                doc.Remove(name);
            }

            return doc;
        }

        public static string ToUpdateBsonDocumentString<TNominalType>(this TNominalType obj, IBsonSerializer<TNominalType> serializer = null, Action<BsonSerializationContext.Builder> configurator = null, BsonSerializationArgs args = default(BsonSerializationArgs), params string[] removeNames)
        {
            return obj.ToUpdateBsonDocument(serializer, configurator, args, removeNames).ToString();
        }

        public static string ToUpdateBsonDocumentString(this object obj, Type nominalType, IBsonSerializer serializer = null, Action<BsonSerializationContext.Builder> configurator = null, BsonSerializationArgs args = default(BsonSerializationArgs), params string[] removeNames)
        {
            return obj.ToUpdateBsonDocument(nominalType, serializer, configurator, args, removeNames).ToString();
        }

        public static string ToUpdateBsonDocumentString(this BsonDocument doc, params string[] removeNames)
        {
            return ToUpdateBsonDocument(doc, removeNames).ToString();
        }
    }
}