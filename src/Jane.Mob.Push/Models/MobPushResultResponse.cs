using System.Text.Json.Serialization;

namespace Jane.Mob.Push
{
    public class MobPushResultResponse
    {
        [JsonPropertyName("batchId")]
        public string BatchId { get; set; }
    }
}