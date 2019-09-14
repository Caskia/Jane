using System.Collections.Generic;

namespace Jane.QCloud.Im
{
    public class AccountGetProfileResponse : QCloudResponse
    {
        public List<AccountProfile> UserProfileItem { get; set; }
    }
}