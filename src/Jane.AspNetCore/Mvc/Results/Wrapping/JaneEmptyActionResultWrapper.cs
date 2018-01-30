using Jane.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Jane.AspNetCore.Mvc.Results.Wrapping
{
    public class JaneEmptyActionResultWrapper : IJaneActionResultWrapper
    {
        public void Wrap(ResultExecutingContext actionResult)
        {
            actionResult.Result = new ObjectResult(new AjaxResponse());
        }
    }
}