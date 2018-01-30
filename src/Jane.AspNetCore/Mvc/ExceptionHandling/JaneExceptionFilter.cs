using Jane.AspNetCore.Mvc.Extensions;
using Jane.AspNetCore.Mvc.Results;
using Jane.Configurations;
using Jane.Dependency;
using Jane.Domain.Entities;
using Jane.Logging;
using Jane.Reflection;
using Jane.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Jane.AspNetCore.Mvc.ExceptionHandling
{
    public class JaneExceptionFilter : IExceptionFilter, ITransientDependency
    {
        private readonly IAspNetCoreConfiguration _configuration;
        private readonly IErrorInfoBuilder _errorInfoBuilder;

        public JaneExceptionFilter(IErrorInfoBuilder errorInfoBuilder, IAspNetCoreConfiguration configuration)
        {
            _errorInfoBuilder = errorInfoBuilder;
            _configuration = configuration;
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
                LogHelper.LogException(context.Exception);
            }

            if (wrapResultAttribute.WrapOnError)
            {
                HandleAndWrapException(context);
            }
        }

        private int GetStatusCode(ExceptionContext context)
        {
            if (context.Exception is JaneAuthorizationException)
            {
                return context.HttpContext.User.Identity.IsAuthenticated
                    ? (int)HttpStatusCode.Forbidden
                    : (int)HttpStatusCode.Unauthorized;
            }

            if (context.Exception is JaneValidationException)
            {
                return (int)HttpStatusCode.BadRequest;
            }

            if (context.Exception is EntityNotFoundException)
            {
                return (int)HttpStatusCode.NotFound;
            }

            return (int)HttpStatusCode.InternalServerError;
        }

        private void HandleAndWrapException(ExceptionContext context)
        {
            if (!ActionResultHelper.IsObjectResult(context.ActionDescriptor.GetMethodInfo().ReturnType))
            {
                return;
            }

            context.HttpContext.Response.StatusCode = GetStatusCode(context);

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