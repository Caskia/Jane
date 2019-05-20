using Jane.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;

namespace Jane.AspNetCore.ExceptionHandling
{
    public class DefaultHttpExceptionStatusCodeFinder : IHttpExceptionStatusCodeFinder
    {
        public HttpStatusCode GetStatusCode(HttpContext httpContext, Exception exception)
        {
            if (exception is JaneAuthorizationException)
            {
                return httpContext.User.Identity.IsAuthenticated
                    ? HttpStatusCode.Forbidden
                    : HttpStatusCode.Unauthorized;
            }

            if (exception is JaneValidationException)
            {
                return HttpStatusCode.BadRequest;
            }

            if (exception is EntityNotFoundException)
            {
                return HttpStatusCode.NotFound;
            }

            if (exception is JaneRateLimitException)
            {
                return HttpStatusCode.TooManyRequests;
            }

            return HttpStatusCode.InternalServerError;
        }
    }
}