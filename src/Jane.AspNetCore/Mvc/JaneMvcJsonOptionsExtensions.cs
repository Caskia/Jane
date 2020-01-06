﻿using Jane.Json.Newtonsoft;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Jane.AspNetCore.Mvc
{
    public static class JaneMvcJsonOptionsExtensions
    {
        public static void ConfigureJaneMvcJsonOptions(this IServiceCollection services)
        {
            services.Configure<MvcNewtonsoftJsonOptions>(options =>
            {
                options.SerializerSettings.Converters.Insert(0, new JaneDateTimeConverter());
                var contractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };
                options.SerializerSettings.ContractResolver = contractResolver;
                options.SerializerSettings.Converters.Add(new StringLongConverter());
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });
        }
    }
}