namespace Jane.QCloud.Im
{
    public class GroupGetMembersInput
    {
        public string GroupId { get; set; }

        public int Limit { get; set; } = 100;

        public int Offset { get; set; } = 0;
    }
}