using Newtonsoft.Json;

namespace Jane.QCloud.Im
{
    public class MessageContent
    {
        [JsonProperty("Text")]
        public string Text { get; set; }
    }
}