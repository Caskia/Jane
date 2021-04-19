using System.Text.Json.Serialization;

namespace Jane.UMeng.Push
{
    public class UMengIOSPayloadAps
    {
        [JsonPropertyName("alert")]
        public UMengIOSPayloadAlert Alert { get; set; }

        [JsonPropertyName("badge")]
        public int? Badge { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        /// <summary>
        /// 代表静默推送 1
        /// </summary>
        [JsonPropertyName("content-available")]
        public int? ContentAvailable { get; set; }

        [JsonPropertyName("sound")]
        public string Sound { get; set; } = "default";
    }
}