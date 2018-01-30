using Jane.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Buffers;
using System.Linq;

namespace Jane.AspNetCore.Mvc.Results.Wrapping
{
    public class JaneObjectActionResultWrapper : IJaneActionResultWrapper
    {
        private readonly IServiceProvider _serviceProvider;

        public JaneObjectActionResultWrapper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Wrap(ResultExecutingContext actionResult)
        {
            var objectResult = actionResult.Result as ObjectResult;
            if (objectResult == null)
            {
                throw new ArgumentException($"{nameof(actionResult)} should be ObjectResult!");
            }

            if (!(objectResult.Value is AjaxResponseBase))
            {
                objectResult.Value = new AjaxResponse(objectResult.Value);
                if (!objectResult.Formatters.Any(f => f is JsonOutputFormatter))
                {
                    objectResult.Formatters.Add(
                        new JsonOutputFormatter(
                            _serviceProvider.GetRequiredService<IOptions<MvcJsonOptions>>().Value.SerializerSettings,
                            _serviceProvider.GetRequiredService<ArrayPool<char>>()
                        )
                    );
                }
            }
        }
    }
}