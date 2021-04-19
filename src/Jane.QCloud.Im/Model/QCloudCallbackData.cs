using System.Text.Json.Serialization;

namespace Jane.QCloud.Im
{
    public abstract class QCloudCallbackData
    {
        [JsonPropertyName("CallbackCommand")]
        public string CallbackCommand { get; set; }
    }
}