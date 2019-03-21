using Jane.Json;
using Jane.Logging;
using Jane.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Jane.AspNetCore.ExceptionHandling
{
    public class JaneExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public JaneExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                // We can't do anything if the response has already started, just abort.
                if (httpContext.Response.HasStarted)
                {
                    LogHelper.Logger.Warn("An exception occured, but response has already started!");
                    throw;
                }

                await HandleAndWrapException(httpContext, ex);
                return;
            }
        }

        private async Task HandleAndWrapException(HttpContext httpContext, Exception exception)
        {
            var errorInfoBuilder = httpContext.RequestServices.GetRequiredService<IErrorInfoBuilder>();
            var statusCodeFinder = httpContext.RequestServices.GetRequiredService<IHttpExceptionStatusCodeFinder>();
            var jsonSerializer = httpContext.RequestServices.GetRequiredService<IJsonSerializer>();

            httpContext.Response.Clear();
            httpContext.Response.StatusCode = (int)statusCodeFinder.GetStatusCode(httpContext, exception);

            await httpContext.Response.WriteAsync(
                jsonSerializer.Serialize(
                     new AjaxResponse(errorInfoBuilder.BuildForException(exception), exception is JaneAuthorizationException)
                )
            );
        }
    }
}