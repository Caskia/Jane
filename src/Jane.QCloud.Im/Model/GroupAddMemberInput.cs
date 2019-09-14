using System.Collections.Generic;

namespace Jane.QCloud.Im
{
    public class GroupAddMemberInput
    {
        public string GroupId { get; set; }

        public List<GroupMember> MemberList { get; set; } = new List<GroupMember>();

        public int Silence { get; set; } = 1;
    }
}