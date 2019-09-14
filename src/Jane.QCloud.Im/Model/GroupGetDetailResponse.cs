using System.Collections.Generic;

namespace Jane.QCloud.Im
{
    public class GroupGetDetailResponse : QCloudResponse
    {
        public List<GroupInfo> GroupInfo { get; set; }
    }
}