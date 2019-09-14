using Newtonsoft.Json;

namespace Jane.Mob.Push
{
    public class MobPushResultResponse
    {
        [JsonProperty("batchId")]
        public string BatchId { get; set; }
    }
}