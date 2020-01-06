﻿using Jane.Json.Newtonsoft;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;

namespace Jane.Runtime.Caching.Redis
{
    /// <summary>
    ///     Default implementation uses JSON as the underlying persistence mechanism.
    /// </summary>
    public class DefaultRedisCacheSerializer : IRedisCacheSerializer
    {
        /// <summary>
        ///     Creates an instance of the object from its serialized string representation.
        /// </summary>
        /// <param name="objbyte">String representation of the object from the Redis server.</param>
        /// <returns>Returns a newly constructed object.</returns>
        /// <seealso cref="IRedisCacheSerializer.Serialize" />
        public virtual object Deserialize(RedisValue objbyte)
        {
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.Converters.Insert(0, new JaneDateTimeConverter());

            var cacheData = JaneCacheData.Deserialize(objbyte);

            return cacheData.Payload.FromJsonString(
                Type.GetType(cacheData.Type, true, true),
                serializerSettings);
        }

        /// <summary>
        ///     Produce a string representation of the supplied object.
        /// </summary>
        /// <param name="value">Instance to serialize.</param>
        /// <param name="type">Type of the object.</param>
        /// <returns>Returns a string representing the object instance that can be placed into the Redis cache.</returns>
        /// <seealso cref="IRedisCacheSerializer.Deserialize" />
        public virtual string Serialize(object value, Type type)
        {
            return JsonConvert.SerializeObject(JaneCacheData.Serialize(value));
        }
    }
}