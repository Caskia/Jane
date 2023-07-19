using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Jane.Google.Recaptcha
{
    public class SiteVerifyV3Response : SiteVerifyV2Response
    {
        public float Score { get; set; }

        public string Action { get; set; }
    }
}

