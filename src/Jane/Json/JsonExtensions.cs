using Jane.Json.Formatters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Jane.Json
{
    public static class JsonExtensions
    {
        /// <summary>
        /// Converts given object to JSON string.
        /// </summary>
        /// <returns></returns>
        public static string ToJsonString(this object obj, bool camelCase = false, bool indented = false, bool referenceLoopIgnore = false, bool retainProperties = false, bool ignoreProperties = false, string[] properties = null)
        {
            var options = new JsonSerializerSettings();

            if (camelCase)
            {
                options.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }

            if (indented)
            {
                options.Formatting = Formatting.Indented;
            }

            if (referenceLoopIgnore)
            {
                options.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }

            if (ignoreProperties)
            {
                options.ContractResolver = new LimitPropertyContractResolver(properties);
            }

            if (retainProperties)
            {
                options.ContractResolver = new LimitPropertyContractResolver(properties, true);
            }

            return JsonConvert.SerializeObject(obj, options);
        }
    }
}