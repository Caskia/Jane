using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Jane.AspNetCore.Mvc.ModelBinding
{
    public class JaneDateTimeModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(DateTime) || context.Metadata.ModelType == typeof(DateTime?))
            {
                return new JaneDateTimeModelBinder(context.Metadata.ModelType);
            }

            return null;
        }
    }
}