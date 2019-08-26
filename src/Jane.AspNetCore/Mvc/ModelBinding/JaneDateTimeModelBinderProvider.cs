using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Jane.AspNetCore.Mvc.ModelBinding
{
    public class JaneDateTimeModelBinderProvider : IModelBinderProvider
    {
        public JaneDateTimeModelBinderProvider()
        {
        }

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            var loggerFactory = context.Services.GetService<ILoggerFactory>();

            if (context.Metadata.ModelType == typeof(DateTime) || context.Metadata.ModelType == typeof(DateTime?))
            {
                return new JaneDateTimeModelBinder(context.Metadata.ModelType, loggerFactory);
            }

            return null;
        }
    }
}