using System.Collections.Generic;

namespace Jane.QCloud.Im
{
    public class GroupAddMemberResponse : QCloudResponse
    {
        public List<GroupMemberResponse> MemberList { get; set; } = new List<GroupMemberResponse>();
    }
}