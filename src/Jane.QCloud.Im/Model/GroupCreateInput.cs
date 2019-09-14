using Newtonsoft.Json;
using System.Collections.Generic;

namespace Jane.QCloud.Im
{
    public class GroupCreateInput : GroupInfo
    {
        public List<GroupMember> MemberList { get; set; } = new List<GroupMember>();
    }
}