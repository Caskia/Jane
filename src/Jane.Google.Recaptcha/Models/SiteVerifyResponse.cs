using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Jane.Google.Recaptcha
{
    public class SiteVerifyOutput
    {
        public bool Success { get; set; }

        public long ChallengeTs { get; set; }

        public string Hostname { get; set; }

        [JsonPropertyName("error-codes")]
        public List<string> ErrorCodes { get; set; }

        public float Score { get; set; }

        public string Action { get; set; }
    }
}

