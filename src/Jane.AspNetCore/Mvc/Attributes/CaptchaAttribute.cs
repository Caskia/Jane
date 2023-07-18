using System;
using System.Threading.Tasks;
using Jane.Captcha;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Jane.AspNetCore.Mvc.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CaptchaAttribute : ActionFilterAttribute
    {
        public CaptchaAttribute()
        {
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var captchaFactory = context.HttpContext.RequestServices.GetService(typeof(ICaptchaFactory)) as ICaptchaFactory;
            var captchaMethod = context.HttpContext.Request.Query["captcha_method"].ToString();
            var captchaToken = context.HttpContext.Request.Query["captcha_token"].ToString();

            var result = await captchaFactory.GetService(captchaMethod).ValidateAsync(captchaToken);
            if (!result.Success)
            {
                throw new JaneValidationException(result.Error);
            }

            await next();
        }
    }
}

