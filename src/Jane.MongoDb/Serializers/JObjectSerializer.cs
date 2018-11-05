using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Newtonsoft.Json.Linq;

namespace Jane.MongoDb.Serializers
{
    public class JObjectSerializer : SerializerBase<JObject>
    {
        public override JObject Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var bsonDocument = BsonDocumentSerializer.Instance.Deserialize(context);
            return JObject.Parse(bsonDocument.ToString());
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, JObject value)
        {
            if (value == null)
            {
                value = JObject.FromObject(new { });
            }

            var bsonDocument = BsonDocument.Parse(value.ToString());
            BsonDocumentSerializer.Instance.Serialize(context, bsonDocument);
        }
    }
}