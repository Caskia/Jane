using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Jane.Netease.Im
{
    public class NeteaseImService : INeteaseImService
    {
        private readonly INeteaseImApi _imApi;

        public NeteaseImService
        (
            INeteaseImApi imApi
        )
        {
            _imApi = imApi;
        }

        public async Task CreateUserAsync(CreateUserInput input)
        {
            var response = await _imApi.CreateUserAsync(input);

            ProcessNeteaseImResponse(response, input, "/user/create.action");
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