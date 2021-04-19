using System.Text.Json.Serialization;

namespace Jane.QCloud.Im
{
    public class RelationshipBlacklistGetInput
    {
        [JsonPropertyName("From_Account")]
        public string From { get; set; }

        public int LastSequence { get; set; }

        public int MaxLimited { get; set; }

        public int StartIndex { get; set; }
    }
}