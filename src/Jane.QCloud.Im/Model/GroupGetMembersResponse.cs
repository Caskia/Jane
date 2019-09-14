using System.Collections.Generic;

namespace Jane.QCloud.Im
{
    public class GroupGetMembersResponse : QCloudResponse
    {
        public List<GroupMember> MemberList { get; set; }

        public int MemberNum { get; set; }
    }
}