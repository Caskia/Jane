using Jane.Extensions;

namespace Jane.QCloud.Im
{
    public class GroupUpdateBaseInfoInput
    {
        #region Private Fields

        private string faceUrl;

        private string introduction;

        private string name;

        private string notification;

        #endregion Private Fields

        public string FaceUrl
        {
            get
            {
                return faceUrl;
            }
            set
            {
                faceUrl = value.Truncate(100);
            }
        }

        public string GroupId { get; set; }

        public string Introduction
        {
            get
            {
                return introduction;
            }
            set
            {
                introduction = value.Truncate(80);
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value.Truncate(30);
            }
        }

        public string Notification
        {
            get
            {
                return notification;
            }
            set
            {
                notification = value.Truncate(100);
            }
        }
    }
}