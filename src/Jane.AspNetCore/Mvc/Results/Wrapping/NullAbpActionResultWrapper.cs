using Microsoft.AspNetCore.Mvc.Filters;

namespace Jane.AspNetCore.Mvc.Results.Wrapping
{
    public class NullAbpActionResultWrapper : IJaneActionResultWrapper
    {
        public void Wrap(ResultExecutingContext actionResult)
        {
            
        }
    }
}