using Microsoft.AspNetCore.Http;
using System;

namespace Jane.AspNetCore.Mvc.Extensions
{
    public static class AjaxRequestExtensions
    {
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return request.Headers != null &&
                   request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
    }
}