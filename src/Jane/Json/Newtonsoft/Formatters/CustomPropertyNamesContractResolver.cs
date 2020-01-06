using Jane.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace Jane.Json.Newtonsoft
{
    public class CustomPropertyNamesContractResolver : DefaultContractResolver
    {
        public CustomPropertyNamesContractResolver()
        { }

        protected override JsonProperty CreateProperty(MemberInfo member,
                                      MemberSerialization memberSerialization)
        {
            return base.CreateProperty(member, memberSerialization);
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            return StringUtils.ToCustomSeparatedCase(propertyName);
        }
    }
}