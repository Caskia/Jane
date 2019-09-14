using System.Threading.Tasks;

namespace Jane.QCloud.CKafka
{
    public interface IQCloudCKafkaService
    {
        Task<CreateTopicOutput> CreateTopicAsync(CreateTopicInput input);

        Task<DeleteTopicOutput> DeleteTopicAsync(DeleteTopicInput input);

        Task<SetTopicAttributesOutput> SetTopicAttributesAsync(SetTopicAttributesInput input);
    }
}