using Jane.Dependency;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Jane.AspNetCore.Mvc.Results.Wrapping
{
    public interface IJaneActionResultWrapperFactory : ITransientDependency
    {
        IJaneActionResultWrapper CreateFor(ResultExecutingContext actionResult);
    }
}