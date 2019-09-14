using System.Collections.Generic;

namespace Jane.QCloud.Im
{
    public class RelationshipBlacklistGetResponse : QCloudResponse
    {
        public List<RelationshipBlacklistItem> BlackListItem { get; set; } = new List<RelationshipBlacklistItem>();

        public int CurruentSequence { get; set; }

        public int StartIndex { get; set; }
    }
}