using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Jane.UMeng.Push
{
    public class UMengIOSPayload : IUMengPayload
    {
        [JsonPropertyName("aps")]
        public UMengIOSPayloadAps Aps { get; set; }

        [JsonPropertyName("body")]
        public Dictionary<string, string> Body { get; set; }
    }
}