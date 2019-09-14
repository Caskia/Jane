using Newtonsoft.Json;

namespace Jane.UMeng.Push
{
    public class UMengIOSPayloadAps
    {
        [JsonProperty("alert")]
        public UMengIOSPayloadAlert Alert { get; set; }

        [JsonProperty("badge")]
        public int? Badge { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        /// <summary>
        /// 代表静默推送 1
        /// </summary>
        [JsonProperty("content-available")]
        public int? ContentAvailable { get; set; }

        [JsonProperty("sound")]
        public string Sound { get; set; } = "default";
    }
}