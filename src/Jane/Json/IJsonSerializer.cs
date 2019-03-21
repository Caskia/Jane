using System;

namespace Jane.Json
{
    public interface IJsonSerializer
    {
        T Deserialize<T>(string jsonString, NamingStrategyType namingStrategyType = NamingStrategyType.SnakeCase);

        object Deserialize(Type type, string jsonString, NamingStrategyType namingStrategyType = NamingStrategyType.SnakeCase);

        string Serialize(object obj, NamingStrategyType namingStrategyType = NamingStrategyType.SnakeCase, bool indented = false);
    }
}