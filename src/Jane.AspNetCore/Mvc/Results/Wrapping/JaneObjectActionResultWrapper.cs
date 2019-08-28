using Jane.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Jane.AspNetCore.Mvc.Results.Wrapping
{
    public class JaneObjectActionResultWrapper : IJaneActionResultWrapper
    {
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
                objectResult.DeclaredType = typeof(AjaxResponse);
            }
        }
    }
}