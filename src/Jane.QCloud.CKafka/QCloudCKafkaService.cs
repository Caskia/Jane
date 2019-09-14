using Microsoft.Extensions.Options;
using Jane.QCloud.CKafka.Configurations;
using Jane.QCloud.CKafka.Models;
using System.Threading.Tasks;
using TencentCloud.Common;
using TencentCloud.Common.Profile;

namespace Jane.QCloud.CKafka
{
    public class QCloudCKafkaService : IQCloudCKafkaService
    {
        private readonly QCloudCKafkaOptions _options;
        private CKafkaClient _cKafkaClient;

        public QCloudCKafkaService(
            IOptions<QCloudCKafkaOptions> optionsAccessor
            )
        {
            _options = optionsAccessor.Value;
            InitCKafkaClient();
        }

        public async Task<CreateTopicOutput> CreateTopicAsync(CreateTopicInput input)
        {
            var response = await _cKafkaClient.CreateTopicAsync(new CreateTopicRequest()
            {
                EnableWhiteList = input.EnableWhiteList,
                InstanceId = _options.InstanceId,
                IpWhiteList = input.IpWhiteList,
                Note = input.Note,
                PartitionNum = input.PartitionNum,
                ReplicaNum = input.ReplicaNum,
                TopicName = input.TopicName
            });

            return new CreateTopicOutput()
            {
                TopicId = response?.TopicId
            };
        }

        public async Task<DeleteTopicOutput> DeleteTopicAsync(DeleteTopicInput input)
        {
            var response = await _cKafkaClient.DeleteTopicAsync(new DeleteTopicRequest()
            {
                InstanceId = _options.InstanceId,
                TopicName = input.TopicName
            });

            return new DeleteTopicOutput();
        }

        public async Task<SetTopicAttributesOutput> SetTopicAttributesAsync(SetTopicAttributesInput input)
        {
            var response = await _cKafkaClient.UpdateTopicAsync(new SetTopicAttributesRequest()
            {
                InstanceId = _options.InstanceId,
                TopicName = input.TopicName,
                EnableWhiteList = input.EnableWhiteList,
                MaxMessageBytes = input.MaxMessageBytes,
                MinInsyncReplicas = input.MinInsyncReplicas,
                Note = input.Note,
                RetentionMs = input.RetentionMs,
                SegmentMs = input.SegmentMs,
                UncleanLeaderElectionEnable = input.UncleanLeaderElectionEnable
            });

            return new SetTopicAttributesOutput();
        }

        private void InitCKafkaClient()
        {
            var credential = new Credential()
            {
                SecretId = _options.SecretId,
                SecretKey = _options.SecretKey
            };

            var httpProfile = new HttpProfile()
            {
                ReqMethod = "POST",
                Timeout = 30,
                Endpoint = "ckafka.api.qcloud.com"
            };

            var clientProfile = new ClientProfile()
            {
                SignMethod = ClientProfile.SIGN_SHA1,
                HttpProfile = httpProfile
            };

            _cKafkaClient = new CKafkaClient(credential, _options.Region, clientProfile);
            _cKafkaClient.Path = "/v2/index.php";
        }
    }
}