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
    public class GoogleRecaptchaV3Service : ICaptchaService
    {
        private readonly GoogleRecaptchaV3Options _options;
        private readonly IGoogleRecaptchaV3Api _googleRecaptchaV3Api;

        public GoogleRecaptchaV3Service(
            IOptions<GoogleRecaptchaV3Options> optionsAccessor,
            IGoogleRecaptchaV3Api googleRecaptchaV3Api
            )
        {
            _options = optionsAccessor.Value;
            _googleRecaptchaV3Api = googleRecaptchaV3Api;
        }


        public async Task<ValidateResult> ValidateAsync(string token)
        {
            var response = await _googleRecaptchaV3Api.SiteVerifyAsync(new SiteVerifyRequest
            {
                Secret = _options.AppSecret,
                Response = token
            });

            if (response.Success)
            {
                return new ValidateResult
                {
                    Success = response.Score >= _options.Threshold,
                    Error = response.Score >= _options.Threshold ? null : $"score[{response.Score}] less than threshold[{_options.Threshold}]."
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

