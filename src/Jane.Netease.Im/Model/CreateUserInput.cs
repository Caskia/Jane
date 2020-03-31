using Jane.Extensions;
using Refit;

namespace Jane.Netease.Im
{
    public class CreateUserInput
    {
        #region Private Fields

        private string icon;
        private string name;

        #endregion Private Fields

        [AliasAs("accid")]
        public string AccountId { get; set; }

        [AliasAs("icon")]
        public string Icon
        {
            get
            {
                return icon;
            }
            set
            {
                icon = value.Truncate(1024);
            }
        }

        [AliasAs("name")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value.Truncate(64);
            }
        }

        [AliasAs("token")]
        public string Token { get; set; }
    }
}