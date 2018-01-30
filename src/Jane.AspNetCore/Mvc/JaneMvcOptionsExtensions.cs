using Jane.AspNetCore.Mvc.Authorization;
using Jane.AspNetCore.Mvc.ExceptionHandling;
using Jane.AspNetCore.Mvc.ModelBinding;
using Jane.AspNetCore.Mvc.Results;
using Jane.AspNetCore.Mvc.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Jane.AspNetCore.Mvc
{
    public static class JaneMvcOptionsExtensions
    {
        public static void ConfigureJaneMvcOptions(this MvcOptions options, IServiceCollection services)
        {
            AddFilters(options);
            AddModelBinders(options);
        }

        private static void AddFilters(MvcOptions options)
        {
            options.Filters.AddService(typeof(JaneAuthorizationFilter));
            options.Filters.AddService(typeof(JaneValidationActionFilter));
            options.Filters.AddService(typeof(JaneExceptionFilter));
            options.Filters.AddService(typeof(JaneResultFilter));
        }

        private static void AddModelBinders(MvcOptions options)
        {
            options.ModelBinderProviders.Insert(0, new JaneDateTimeModelBinderProvider());
        }
    }
}