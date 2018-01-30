using Microsoft.AspNetCore.Mvc.Filters;

namespace Jane.AspNetCore.Mvc.Results.Wrapping
{
    public interface IJaneActionResultWrapper
    {
        void Wrap(ResultExecutingContext actionResult);
    }
}