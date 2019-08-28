using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Jane.AspNetCore.Mvc.Results.Wrapping
{
    public class JaneActionResultWrapperFactory : IJaneActionResultWrapperFactory
    {
        public IJaneActionResultWrapper CreateFor(ResultExecutingContext actionResult)
        {
            if (actionResult == null)
            {
                throw new ArgumentNullException(nameof(actionResult));
            }

            if (actionResult.Result is ObjectResult)
            {
                return new JaneObjectActionResultWrapper();
            }

            if (actionResult.Result is JsonResult)
            {
                return new JaneJsonActionResultWrapper();
            }

            if (actionResult.Result is EmptyResult)
            {
                return new JaneEmptyActionResultWrapper();
            }

            return new NullJaneActionResultWrapper();
        }
    }
}