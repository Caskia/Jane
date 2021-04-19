using Jane.Extensions;
using System.Text.Json.Serialization;

namespace Jane.UMeng.Push
{
    public class UMengIOSPayloadAlert
    {
        #region Private Fields

        private string body;

        private string subtitle;

        private string title;

        #endregion Private Fields

        [JsonPropertyName("body")]
        public string Body
        {
            get
            {
                return body;
            }
            set
            {
                body = value.TruncateWithPostfix(160);
            }
        }

        [JsonPropertyName("subtitle")]
        public string Subtitle
        {
            get
            {
                return subtitle;
            }
            set
            {
                subtitle = value.TruncateWithPostfix(80);
            }
        }

        [JsonPropertyName("title")]
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value.TruncateWithPostfix(80);
            }
        }
    }
}