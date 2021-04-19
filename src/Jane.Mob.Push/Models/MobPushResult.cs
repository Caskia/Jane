using System.Text.Json.Serialization;

namespace Jane.Mob.Push
{
    public class MobPushResult
    {
        [JsonPropertyName("error")]
        public string Error { get; set; }

        [JsonPropertyName("res")]
        public MobPushResultResponse Res { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }
    }
}