using System;
using System.Threading.Tasks;

namespace Jane.Captcha
{
    public class GoogleRecaptchaV3Service : ICaptchaService
    {
        public Task<ValidateResult> ValidateAsync(string token)
        {
            throw new NotImplementedException();
        }
    }
}

