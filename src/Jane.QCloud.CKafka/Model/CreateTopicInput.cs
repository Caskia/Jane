namespace Jane.QCloud.CKafka
{
    public class CreateTopicInput
    {
        public int EnableWhiteList { get; set; }

        public string IpWhiteList { get; set; }

        public string Note { get; set; }

        public int PartitionNum { get; set; }

        public int ReplicaNum { get; set; }

        public string TopicName { get; set; }
    }
}