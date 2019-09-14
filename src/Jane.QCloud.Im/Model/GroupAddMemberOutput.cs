using System.Collections.Generic;

namespace Jane.QCloud.Im
{
    public class GroupAddMemberOutput : QCloudOutput
    {
        public List<string> FailedMembers { get; set; } = new List<string>();
    }
}