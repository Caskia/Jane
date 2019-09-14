using Newtonsoft.Json;

namespace Jane.QCloud.Im
{
    public class RelationshipBlacklistGetInput
    {
        [JsonProperty("From_Account")]
        public string From { get; set; }

        public int LastSequence { get; set; }

        public int MaxLimited { get; set; }

        public int StartIndex { get; set; }
    }
}