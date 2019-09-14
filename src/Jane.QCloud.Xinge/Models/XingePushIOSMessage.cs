using Jane.Extensions;
using Newtonsoft.Json;

namespace Jane.QCloud.Xinge
{
    public class XingePushIOSMessage
    {
        #region Private Fields

        private string content;

        private string title;

        #endregion Private Fields

        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value.TruncateWithPostfix(160);
            }
        }

        [JsonProperty("ios")]
        public XingePushIOSPayload IOS { get; set; }

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