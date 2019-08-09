using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using System.Collections.Generic;

namespace Jane.MongoDb.Serializers
{
    public class ObjectDictionarySerializer : DictionarySerializerBase<Dictionary<string, object>>
    {
        public ObjectDictionarySerializer()
            : this(DictionaryRepresentation.Document)
        {
        }

        public ObjectDictionarySerializer(DictionaryRepresentation dictionaryRepresentation)
            : base(dictionaryRepresentation, new ObjectSerializer(), new SimpleObjectSerializer())
        {
        }

        public ObjectDictionarySerializer(DictionaryRepresentation dictionaryRepresentation, IBsonSerializer keySerializer, IBsonSerializer valueSerializer)
            : base(dictionaryRepresentation, keySerializer, valueSerializer)
        {
        }

        protected override Dictionary<string, object> CreateInstance()
        {
            return new Dictionary<string, object>();
        }
    }
}