using Jane.Json;
using Jane.Json.Converters;
using Jane.Json.Formatters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;

namespace Jane.AspNetCore.Mvc
{
    public static class JaneMvcJsonOptionsExtensions
    {
        public static void ConfigureJaneMvcJsonOptions(this IServiceCollection services)
        {
            services.Configure<MvcJsonOptions>(options =>
            {
                options.SerializerSettings.Converters.Insert(0, new JaneDateTimeConverter());
                options.SerializerSettings.ContractResolver = new CustomPropertyNamesContractResolver();
                options.SerializerSettings.Converters.Add(new LongConverter());
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });
        }
    }
}