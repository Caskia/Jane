using Jane.Configurations;
using Jane.Runtime.Security;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Jane.Netease.Im
{
    public class NeteaseImService : INeteaseImService
    {
        private readonly INeteaseImApi _imApi;
        private readonly NeteaseImOptions _options;

        public NeteaseImService
        (
            INeteaseImApi imApi,
            IOptions<NeteaseImOptions> optionsAccessor
        )
        {
            _imApi = imApi;
            _options = optionsAccessor.Value;
        }

        public async Task CreateUserAsync(CreateUserInput input)
        {
            var secret = GenerateSecret();

            var response = await _imApi.CreateUserAsync(_options.AppKey, secret.Nonce, secret.CurTime, secret.CheckSum, input);

            ProcessNeteaseImResponse(response, input, "/user/create.action");
        }

        private (string Nonce, string CurTime, string CheckSum) GenerateSecret()
        {
            var nonce = Guid.NewGuid().ToString();
            var curTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
            var checkSum = StringSHA.SHA1($"{_options.AppSecret}{nonce}{curTime}");

            return (nonce, curTime, checkSum);
        }

        private void ProcessNeteaseImResponse<T>(T response, object request, string method, params int[] exceptedErrorCode)
            where T : NeteaseResponse
        {
            if (response.Code != 200)
            {
                if (exceptedErrorCode != null && exceptedErrorCode.Contains(response.Code))
                {
                    return;
                }

                throw new UserFriendlyException($"request netease im api[{method}] body[{JsonConvert.SerializeObject(request)}] error, error code[{response.Code}]. ");
            }
        }
    }
}