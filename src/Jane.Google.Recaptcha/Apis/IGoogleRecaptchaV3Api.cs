using System.Threading.Tasks;
using Refit;

namespace Jane.Google.Recaptcha.Apis
{
    public interface IGoogleRecaptchaV3Api
    {
        [Post("/recaptcha/api/siteverify")]
        Task<SiteVerifyResponse> SiteVerifyAsync([Body(BodySerializationMethod.UrlEncoded)] SiteVerifyRequest request);
    }
}

