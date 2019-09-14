using Jane.QCloud.CKafka.Models;

namespace Jane.QCloud.CKafka
{
    public class CreateTopicOutput : CreateTopicResponse
    {
        public new string TopicId { get; set; }
    }
}