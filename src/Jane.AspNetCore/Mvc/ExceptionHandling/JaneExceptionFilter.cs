using Jane.AspNetCore.ExceptionHandling;
using Jane.AspNetCore.Mvc.Extensions;
using Jane.AspNetCore.Mvc.Results;
using Jane.Configurations;
using Jane.Dependency;
using Jane.Logging;
using Jane.Reflection;
using Jane.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Jane.AspNetCore.Mvc.ExceptionHandling
{
    public class JaneExceptionFilter : IExceptionFilter, ITransientDependency
    {
        private readonly IAspNetCoreConfiguration _configuration;
        private readonly IErrorInfoBuilder _errorInfoBuilder;
        private readonly ILogger _logger;
        private readonly IHttpExceptionStatusCodeFinder _statusCodeFinder;

        public JaneExceptionFilter(
            IAspNetCoreConfiguration configuration,
            IErrorInfoBuilder errorInfoBuilder,
            ILoggerFactory loggerFactory,
            IHttpExceptionStatusCodeFinder statusCodeFinder
            )
        {
            _errorInfoBuilder = errorInfoBuilder;
            _configuration = configuration;
            _logger = loggerFactory.Create(nameof(JaneExceptionFilter));
            _statusCodeFinder = statusCodeFinder;
        }

        public void OnException(ExceptionContext context)
        {
            if (!context.ActionDescriptor.IsControllerAction())
            {
                return;
            }

            var wrapResultAttribute =
                ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault(
                    context.ActionDescriptor.GetMethodInfo(),
                    _configuration.DefaultWrapResultAttribute
                );

            if (wrapResultAttribute.LogError)
            {
                var severity = (context.Exception as IHasLogSeverity)?.Severity ?? LogSeverity.Error;

                _logger.Log(severity, context.Exception?.Message, context.Exception);
            }

            if (wrapResultAttribute.WrapOnError)
            {
                HandleAndWrapException(context);
            }
        }

        private void HandleAndWrapException(ExceptionContext context)
        {
            if (!ActionResultHelper.IsObjectResult(context.ActionDescriptor.GetMethodInfo().ReturnType))
            {
                return;
            }

            context.HttpContext.Response.StatusCode = (int)_statusCodeFinder.GetStatusCode(context.HttpContext, context.Exception);
            foreach (var header in _errorInfoBuilder.BuildHeaders(context.Exception))
            {
                context.HttpContext.Response.Headers.Add(header.Key, header.Value);
            }

            context.Result = new ObjectResult(
                new AjaxResponse(
                    _errorInfoBuilder.BuildForException(context.Exception),
                    context.Exception is JaneAuthorizationException
                )
            );

            context.Exception = null; //Handled!
        }
    }
}