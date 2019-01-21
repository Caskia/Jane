using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Jane.Reflection
{
    public class PropertyCache<T>
    {
        static PropertyCache()
        {
            Properties = typeof(T).GetProperties();
        }

        public static IEnumerable<PropertyInfo> Properties { get; }

        public static void ValidateProperties(IList<string> properties)
        {
            foreach (var @property in properties)
            {
                var prop = Properties.FirstOrDefault(item => item.Name == @property);
                if (prop == null)
                {
                    throw new ArgumentException($"{@property} is not a recognizable property.");
                }
            }
        }

        public IDictionary<string, object> ToDictionary(IList<string> properties)
        {
            var dictionary = new Dictionary<string, object>();

            foreach (var @property in properties)
            {
                var prop = Properties.FirstOrDefault(item => item.Name == @property);
                if (prop == null)
                {
                    throw new ArgumentException($"{@property} is not a recognizable property.");
                }
                dictionary.Add(@property, prop.GetValue(this));
            }

            return dictionary;
        }
    }
}