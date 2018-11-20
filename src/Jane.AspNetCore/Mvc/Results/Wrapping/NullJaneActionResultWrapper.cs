using Microsoft.AspNetCore.Mvc.Filters;

namespace Jane.AspNetCore.Mvc.Results.Wrapping
{
    public class NullJaneActionResultWrapper : IJaneActionResultWrapper
    {
        public void Wrap(ResultExecutingContext actionResult)
        {
        }
    }
}