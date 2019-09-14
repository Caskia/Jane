using Jane.Extensions;

namespace Jane.QCloud.Xinge
{
    public class XingePushIOSPayloadAlert
    {
        #region Private Fields

        //private string body;

        private string subtitle;

        //private string title;

        #endregion Private Fields

        //public string Body
        //{
        //    get
        //    {
        //        return body;
        //    }
        //    set
        //    {
        //        body = value.TruncateWithPostfix(160);
        //    }
        //}

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

        //public string Title
        //{
        //    get
        //    {
        //        return title;
        //    }
        //    set
        //    {
        //        title = value.TruncateWithPostfix(80);
        //    }
        //}
    }
}