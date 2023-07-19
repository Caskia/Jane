using Refit;

namespace Jane.Google.Recaptcha
{
    public class SiteVerifyV3Request
    {
        [AliasAs("secret")]
        public string Secret { get; set; }

        [AliasAs("response")]
        public string Response { get; set; }

        [AliasAs("remoteip")]
        public string RemoteIp { get; set; }
    }
}

