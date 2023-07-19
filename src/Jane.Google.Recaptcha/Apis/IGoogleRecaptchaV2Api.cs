using System.Threading.Tasks;
using Refit;

namespace Jane.Google.Recaptcha.Apis
{
    public interface IGoogleRecaptchaV2Api
    {
        [Post("/recaptcha/api/siteverify")]
        Task<SiteVerifyV2Response> SiteVerifyAsync([Body(BodySerializationMethod.UrlEncoded)] SiteVerifyV2Request request);
    }
}

