using Jane.Configurations;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using TencentCloud.Common;
using TencentCloud.Common.Profile;
using TencentCloud.Soe.V20180724;
using TencentCloud.Soe.V20180724.Models;

namespace Jane.QCloud.Soe
{
    public class QCloudSoeService : IQCloudSoeService
    {
        private readonly IIdGenerator _idGenerator;
        private readonly QCloudSoeOptions _options;
        private SoeClient _soeClient;

        public QCloudSoeService(
            IIdGenerator idGenerator,
            IOptions<QCloudSoeOptions> optionsAccessor
            )
        {
            _idGenerator = idGenerator;
            _options = optionsAccessor.Value;
            InitSoeClient();
        }

        public async Task<EvaluateOutput> EvaluateAsync(EvaluateInput input)
        {
            var sessionId = _idGenerator.Guid().ToString("N");
            var initOralProcessResponse = await _soeClient.InitOralProcess(new InitOralProcessRequest()
            {
                EvalMode = input.EvalMode,
                IsLongLifeSession = null,
                RefText = input.RefText,
                ScoreCoeff = input.ScoreCoeff.HasValue ? input.ScoreCoeff : _options.ScoreCoeff,
                SessionId = sessionId,
                SoeAppId = _options.AppId,
                WorkMode = 1
            });

            var transmitOralProcessResponse = await _soeClient.TransmitOralProcess(new TransmitOralProcessRequest()
            {
                IsEnd = 1,
                IsLongLifeSession = null,
                SeqId = 1,
                SessionId = sessionId,
                SoeAppId = _options.AppId,
                UserVoiceData = input.VoiceData,
                VoiceEncodeType = 1,
                VoiceFileType = input.VoiceFileType
            });

            return new EvaluateOutput()
            {
                PronAccuracy = transmitOralProcessResponse.PronAccuracy,
                PronCompletion = transmitOralProcessResponse.PronCompletion,
                PronFluency = transmitOralProcessResponse.PronFluency,
                RequestId = transmitOralProcessResponse.RequestId,
                SessionId = transmitOralProcessResponse.SessionId,
                Words = transmitOralProcessResponse.Words
            };
        }

        private void InitSoeClient()
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
                Endpoint = "soe.ap-guangzhou.tencentcloudapi.com",
            };

            var clientProfile = new ClientProfile()
            {
                SignMethod = ClientProfile.SIGN_SHA1,
                HttpProfile = httpProfile
            };

            _soeClient = new SoeClient(credential, null, clientProfile);
        }
    }
}