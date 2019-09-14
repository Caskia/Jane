using Jane.Extensions;

namespace Jane.QCloud.Xinge
{
    public class XingePushAndroidMessage
    {
        #region Private Fields

        private string content;

        private string title;

        #endregion Private Fields

        public XingePushAndroidPayload Android { get; set; }

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

        public string XgMediaResources { get; set; }
    }
}