using Jane.Extensions;
using System.Text.Json.Serialization;

namespace Jane.QCloud.Im
{
    public class GroupInfo
    {
        #region Private Fields

        private string faceUrl;

        private string introduction;

        private string name;

        private string notification;

        #endregion Private Fields

        public string ApplyJoinOption { get; set; } = "FreeAccess";

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
                introduction = value.Truncate(240);
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
                name = value.Truncate(20);
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
                notification = value.Truncate(300);
            }
        }

        [JsonPropertyName("Owner_Account")]
        public string Owner { get; set; }

        public string Type { get; set; } = "Public";
    }
}