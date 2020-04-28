using Jane.Extensions;
using Refit;

namespace Jane.RongCloud.Im
{
    public class GetUserTokenInput
    {
        #region Private Fields

        private string name;
        private string portraitUri;

        #endregion Private Fields

        [AliasAs("name")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value.Truncate(128);
            }
        }

        [AliasAs("icon")]
        public string PortraitUri
        {
            get
            {
                return portraitUri;
            }
            set
            {
                portraitUri = value.Truncate(1024);
            }
        }

        [AliasAs("userId")]
        public string UserId { get; set; }
    }
}