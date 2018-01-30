using Jane.Aspects;
using Jane.Dependency;
using Castle.DynamicProxy;

namespace Jane.Runtime.Validation.Interception
{
    /// <summary>
    /// This interceptor is used intercept method calls for classes which's methods must be validated.
    /// </summary>
    public class ValidationInterceptor : IInterceptor
    {
        public ValidationInterceptor()
        {
        }

        public void Intercept(IInvocation invocation)
        {
            if (JaneCrossCuttingConcerns.IsApplied(invocation.InvocationTarget, JaneCrossCuttingConcerns.Validation))
            {
                invocation.Proceed();
                return;
            }

            var validator = ObjectContainer.Resolve<MethodInvocationValidator>();
            validator.Initialize(invocation.MethodInvocationTarget, invocation.Arguments);
            validator.Validate();

            invocation.Proceed();
        }
    }
}