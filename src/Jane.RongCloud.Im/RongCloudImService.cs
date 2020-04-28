using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Jane.RongCloud.Im
{
    public class RongCloudImService : IRongCloudImService
    {
        private readonly IRongCloudImApi _imApi;

        public RongCloudImService
        (
            IRongCloudImApi imApi
        )
        {
            _imApi = imApi;
        }

        public async Task<string> GetUserTokenAsync(GetUserTokenInput input)
        {
            var response = await _imApi.GetUserTokenAsync(input);

            ProcessRongCloudImResponse(response, input, "/user/getToken.json");

            return response.Token;
        }

        public async Task RefreshUserAsync(RefreshUserInput input)
        {
            var response = await _imApi.RefreshUserAsync(input);

            ProcessRongCloudImResponse(response, input, "/user/refresh.json");
        }

        private void ProcessRongCloudImResponse<T>(T response, object request, string method, params int[] exceptedErrorCode)
            where T : RongCloudResponse
        {
            if (response.Code != 200)
            {
                if (exceptedErrorCode != null && exceptedErrorCode.Contains(response.Code))
                {
                    return;
                }

                throw new UserFriendlyException($"request rong cloud im api[{method}] body[{JsonConvert.SerializeObject(request)}] error, error code[{response.Code}]. ");
            }
        }
    }
}