using System.Threading.Tasks;
using Refit;

namespace Jane.Google.Recaptcha.Apis
{
    public interface IGoogleRecaptchaV3Api
    {
        [Post("/recaptcha/api/siteverify")]
        Task<SiteVerifyV3Response> SiteVerifyAsync([Body(BodySerializationMethod.UrlEncoded)] SiteVerifyV3Request request);
    }
}

