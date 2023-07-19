using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Jane.Google.Recaptcha
{
    public class SiteVerifyV2Response
    {
        public bool Success { get; set; }

        public string ChallengeTs { get; set; }

        public string Hostname { get; set; }

        [JsonPropertyName("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
}

