using System.Text.Json.Serialization;

namespace Jane.QCloud.Im
{
    public class MessageContent
    {
        [JsonPropertyName("Text")]
        public string Text { get; set; }
    }
}