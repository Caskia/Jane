using Newtonsoft.Json;

namespace Jane.QCloud.Im
{
    public abstract class QCloudCallbackData
    {
        [JsonProperty("CallbackCommand")]
        public string CallbackCommand { get; set; }
    }
}