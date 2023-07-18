using System;
using System.Threading.Tasks;

namespace Jane.Captcha
{
    public interface ICaptchaService
    {
        Task<ValidateResult> ValidateAsync(string token);
    }
}

