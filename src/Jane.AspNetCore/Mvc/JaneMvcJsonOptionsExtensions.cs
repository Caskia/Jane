using Jane.Json.Microsoft;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace Jane.AspNetCore.Mvc
{
    public static class JaneMvcJsonOptionsExtensions
    {
        public static IMvcBuilder AddJaneJsonOptions(this IMvcBuilder builder, Action<JsonOptions> action = null)
        {
            if (action == null)
            {
                action = options =>
                {
                    var encoderSettings = new TextEncoderSettings();
                    encoderSettings.AllowRanges(UnicodeRanges.All);

                    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(encoderSettings);
                    options.JsonSerializerOptions.PropertyNamingPolicy = new JsonSnakeCaseNamingPolicy();
                    options.JsonSerializerOptions.Converters.Insert(0, new JaneDateTimeConverter());
                    options.JsonSerializerOptions.Converters.Add(new StringLongConverter());
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                };
            }

            builder.AddJsonOptions(action);

            return builder;
        }
    }
}