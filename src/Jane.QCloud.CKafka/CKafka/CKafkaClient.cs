using Newtonsoft.Json;
using Jane.QCloud.CKafka.Models;
using System.Threading.Tasks;
using TencentCloud.Common;
using TencentCloud.Common.Profile;

namespace Jane.QCloud.CKafka
{
    public class CKafkaClient : AbstractClient
    {
        private const string endpoint = "ckafka.api.qcloud.com";
        private const string version = "2018-07-24";

        public CKafkaClient(Credential credential, string region)
            : this(credential, region, new ClientProfile())
        {
        }

        public CKafkaClient(Credential credential, string region, ClientProfile profile)
            : base(endpoint, version, credential, region, profile)
        {
        }

        public async Task<CreateTopicResponse> CreateTopicAsync(CreateTopicRequest req)
        {
            JsonResponseModel<CreateTopicResponse> rsp = null;
            try
            {
                var strResp = await this.InternalRequest(req, "CreateTopic");
                rsp = JsonConvert.DeserializeObject<JsonResponseModel<CreateTopicResponse>>(strResp);
            }
            catch (JsonSerializationException e)
            {
                throw new TencentCloudSDKException(e.Message);
            }
            return rsp.Response;
        }

        public async Task<DeleteTopicResponse> DeleteTopicAsync(DeleteTopicRequest req)
        {
            JsonResponseModel<DeleteTopicResponse> rsp = null;
            try
            {
                var strResp = await this.InternalRequest(req, "DeleteTopic");
                rsp = JsonConvert.DeserializeObject<JsonResponseModel<DeleteTopicResponse>>(strResp);
            }
            catch (JsonSerializationException e)
            {
                throw new TencentCloudSDKException(e.Message);
            }
            return rsp.Response;
        }

        public async Task<SetTopicAttributesResponse> UpdateTopicAsync(SetTopicAttributesRequest req)
        {
            JsonResponseModel<SetTopicAttributesResponse> rsp = null;
            try
            {
                var strResp = await this.InternalRequest(req, "SetTopicAttributes");
                rsp = JsonConvert.DeserializeObject<JsonResponseModel<SetTopicAttributesResponse>>(strResp);
            }
            catch (JsonSerializationException e)
            {
                throw new TencentCloudSDKException(e.Message);
            }
            return rsp.Response;
        }
    }
}