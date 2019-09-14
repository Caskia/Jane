using Newtonsoft.Json;

namespace Jane.Mob.Push
{
    public class MobPushResult
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("res")]
        public MobPushResultResponse Res { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }
    }
}