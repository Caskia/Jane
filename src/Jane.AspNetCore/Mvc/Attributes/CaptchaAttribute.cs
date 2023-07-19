using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Jane.Captcha;
using Jane.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Jane.AspNetCore.Mvc.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CaptchaAttribute : ActionFilterAttribute
    {
        private const string CaptchaMethodParameterName = "captcha_method";
        private const string CaptchaTokenParameterName = "captcha_token";

        public CaptchaAttribute()
        {
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var captchaFactory = context.HttpContext.RequestServices.GetService(typeof(ICaptchaFactory)) as ICaptchaFactory;


            var validationMemberNames = new List<string>();
            var captchaMethod = context.HttpContext.Request.Query[CaptchaMethodParameterName].ToString();
            if (captchaMethod.IsNullOrEmpty())
            {
                validationMemberNames.Add(CaptchaMethodParameterName);
            }

            var captchaToken = context.HttpContext.Request.Query[CaptchaTokenParameterName].ToString();
            if (captchaToken.IsNullOrEmpty())
            {
                validationMemberNames.Add(CaptchaTokenParameterName);
            }


            if (validationMemberNames.Any())
            {
                throw new JaneValidationException("need captcha query parameters.", new List<ValidationResult>
                {
                    new ValidationResult("need captcha query parameters.", validationMemberNames)
                });
            }

            var result = await captchaFactory.GetService(captchaMethod).ValidateAsync(captchaToken);
            if (!result.Success)
            {
                throw new JaneValidationException(result.Error, new List<ValidationResult>
                {
                    new ValidationResult(result.Error)
                });
            }

            await next();
        }
    }
}

