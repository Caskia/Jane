using Jane.AspNetCore.Mvc.Extensions;
using Jane.Configurations;
using Jane.Extensions;
using Jane.Runtime.Validation.Interception;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;

namespace Jane.AspNetCore.Mvc.Validation
{
    public class MvcActionInvocationValidator : MethodInvocationValidator
    {
        private bool _isValidatedBefore;

        public MvcActionInvocationValidator(IValidationConfiguration configuration)
            : base(configuration)
        {
        }

        protected ActionExecutingContext ActionContext { get; private set; }

        public void Initialize(ActionExecutingContext actionContext)
        {
            ActionContext = actionContext;

            SetDataAnnotationAttributeErrors();

            base.Initialize(
                actionContext.ActionDescriptor.GetMethodInfo(),
                GetParameterValues(actionContext)
            );
        }

        protected virtual object[] GetParameterValues(ActionExecutingContext actionContext)
        {
            var methodInfo = actionContext.ActionDescriptor.GetMethodInfo();

            var parameters = methodInfo.GetParameters();
            var parameterValues = new object[parameters.Length];

            for (var i = 0; i < parameters.Length; i++)
            {
                parameterValues[i] = actionContext.ActionArguments.GetOrDefault(parameters[i].Name);
            }

            return parameterValues;
        }

        protected override void SetDataAnnotationAttributeErrors(object validatingObject)
        {
            SetDataAnnotationAttributeErrors();
        }

        protected virtual void SetDataAnnotationAttributeErrors()
        {
            if (_isValidatedBefore || ActionContext.ModelState.IsValid)
            {
                return;
            }

            foreach (var state in ActionContext.ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    ValidationErrors.Add(new ValidationResult(error.ErrorMessage, new[] { state.Key }));
                }
            }

            _isValidatedBefore = true;
        }
    }
}