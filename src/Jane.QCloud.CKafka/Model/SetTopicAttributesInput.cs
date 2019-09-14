namespace Jane.QCloud.CKafka
{
    public class SetTopicAttributesInput
    {
        public int EnableWhiteList { get; set; }

        public int MaxMessageBytes { get; set; }

        public int MinInsyncReplicas { get; set; }

        public string Note { get; set; }

        public int RetentionMs { get; set; }

        public int SegmentMs { get; set; }

        public string TopicName { get; set; }

        public int UncleanLeaderElectionEnable { get; set; }
    }
}