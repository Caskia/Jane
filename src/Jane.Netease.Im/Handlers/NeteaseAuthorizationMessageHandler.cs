using Jane.Configurations;
using Jane.Runtime.Security;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Jane.Netease.Im.Handlers
{
    public class NeteaseAuthorizationMessageHandler : DelegatingHandler
    {
        private readonly NeteaseImOptions _options;

        public NeteaseAuthorizationMessageHandler(IOptions<NeteaseImOptions> optionsAccessor)
        {
            _options = optionsAccessor.Value;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var nonce = Guid.NewGuid().ToString();
            var curTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
            var checkSum = StringSHA.SHA1($"{_options.AppSecret}{nonce}{curTime}");

            request.Headers.Add("AppKey", _options.AppKey);
            request.Headers.Add("Nonce", nonce);
            request.Headers.Add("CurTime", curTime);
            request.Headers.Add("CheckSum", checkSum);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}