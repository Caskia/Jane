using Jane.AspNetCore.Mvc.Extensions;
using Jane.AspNetCore.Mvc.Results.Wrapping;
using Jane.Dependency;
using Microsoft.AspNetCore.Mvc.Filters;
using Jane.Reflection;
using Jane.Configurations;

namespace Jane.AspNetCore.Mvc.Results
{
    public class JaneResultFilter : IResultFilter, ITransientDependency
    {
        private readonly IJaneActionResultWrapperFactory _actionResultWrapperFactory;
        private readonly IAspNetCoreConfiguration _configuration;

        public JaneResultFilter(IAspNetCoreConfiguration configuration,
            IJaneActionResultWrapperFactory actionResultWrapper)
        {
            _configuration = configuration;
            _actionResultWrapperFactory = actionResultWrapper;
        }

        public virtual void OnResultExecuted(ResultExecutedContext context)
        {
            //no action
        }

        public virtual void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ActionDescriptor.IsControllerAction())
            {
                return;
            }

            var methodInfo = context.ActionDescriptor.GetMethodInfo();

            //var clientCacheAttribute = ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault(
            //    methodInfo,
            //    _configuration.DefaultClientCacheAttribute
            //);

            //clientCacheAttribute?.Apply(context);

            var wrapResultAttribute =
                ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault(
                    methodInfo,
                    _configuration.DefaultWrapResultAttribute
                );

            if (!wrapResultAttribute.WrapOnSuccess)
            {
                return;
            }

            _actionResultWrapperFactory.CreateFor(context).Wrap(context);
        }
    }
}