using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jane.Json.Formatters
{
    public class LimitPropertyContractResolver : DefaultContractResolver
    {
        private string[] _properties = null;

        private bool _retain;

        public LimitPropertyContractResolver(string[] properties, bool retain = false)
        {
            _properties = properties;
            _retain = retain;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> list = base.CreateProperties(type, memberSerialization);
            return list.Where(p =>
            {
                if (_retain)
                {
                    return _properties.Contains(p.PropertyName);
                }
                else
                {
                    return !_properties.Contains(p.PropertyName);
                }
            }).ToList();
        }
    }
}