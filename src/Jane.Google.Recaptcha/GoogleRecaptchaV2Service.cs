using System;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;
using Jane.Configurations;
using Jane.Google.Recaptcha;
using Jane.Google.Recaptcha.Apis;
using Microsoft.Extensions.Options;

namespace Jane.Captcha
{
    public class GoogleRecaptchaV2Service : ICaptchaService
    {
        private readonly GoogleRecaptchaV2Options _options;
        private readonly IGoogleRecaptchaV2Api _googleRecaptchaV2Api;

        public GoogleRecaptchaV2Service(
            IOptions<GoogleRecaptchaV2Options> optionsAccessor,
            IGoogleRecaptchaV2Api googleRecaptchaV2Api
            )
        {
            _options = optionsAccessor.Value;
            _googleRecaptchaV2Api = googleRecaptchaV2Api;
        }


        public async Task<ValidateResult> ValidateAsync(string token)
        {
            var response = await _googleRecaptchaV2Api.SiteVerifyAsync(new SiteVerifyV2Request
            {
                Secret = _options.AppSecret,
                Response = token
            });

            if (response.Success)
            {
                return new ValidateResult
                {
                    Success = response.Success
                };
            }
            else
            {
                return new ValidateResult
                {
                    Success = response.Success,
                    Error = string.Join(",", response.ErrorCodes)
                };
            }
        }
    }
}

