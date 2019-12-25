using Refit;
using System;

namespace Jane.QCloud.Im
{
    public class DefaultQCloudMessagingParameter
    {
        [AliasAs("sdkappid")]
        public int AppId { get; set; }

        [AliasAs("contenttype")]
        public string ContentType
        {
            get
            {
                return "json";
            }
        }

        [AliasAs("identifier")]
        public string Identifier { get; set; }

        [AliasAs("random")]
        public int Random
        {
            get
            {
                return new Random().Next(0, int.MaxValue);
            }
        }

        [AliasAs("usersig")]
        public string Signature { get; set; }
    }
}