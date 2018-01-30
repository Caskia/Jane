using Jane.Aspects;
using Jane.AspNetCore.Mvc.Extensions;
using Jane.Configurations;
using Jane.Dependency;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Jane.AspNetCore.Mvc.Validation
{
    public class JaneValidationActionFilter : IAsyncActionFilter, ITransientDependency
    {
        private readonly IAspNetCoreConfiguration _configuration;

        public JaneValidationActionFilter(IAspNetCoreConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!_configuration.IsValidationEnabledForControllers || !context.ActionDescriptor.IsControllerAction())
            {
                await next();
                return;
            }

            using (JaneCrossCuttingConcerns.Applying(context.Controller, JaneCrossCuttingConcerns.Validation))
            {
                var validator = ObjectContainer.Resolve<MvcActionInvocationValidator>();
                validator.Initialize(context);
                validator.Validate();

                await next();
            }
        }
    }
}