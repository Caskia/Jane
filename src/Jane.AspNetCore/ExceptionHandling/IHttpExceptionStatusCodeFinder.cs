using Jane.Dependency;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;

namespace Jane.AspNetCore.ExceptionHandling
{
    public interface IHttpExceptionStatusCodeFinder : ISingletonDependency
    {
        HttpStatusCode GetStatusCode(HttpContext httpContext, Exception exception);
    }
}