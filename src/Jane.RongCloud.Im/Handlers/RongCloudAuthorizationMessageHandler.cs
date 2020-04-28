using Jane.Configurations;
using Jane.Runtime.Security;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Jane.RongCloud.Im.Handlers
{
    public class RongCloudAuthorizationMessageHandler : DelegatingHandler
    {
        private readonly RongCloudImOptions _options;

        public RongCloudAuthorizationMessageHandler(IOptions<RongCloudImOptions> optionsAccessor)
        {
            _options = optionsAccessor.Value;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var appKey = _options.AppKey;
            var nonce = new Random().Next().ToString();
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            var signature = StringSHA.SHA1($"{_options.AppSecret}{nonce}{timestamp}");

            request.Headers.Add("RC-App-Key", appKey);
            request.Headers.Add("RC-Nonce", nonce);
            request.Headers.Add("RC-Timestamp", timestamp);
            request.Headers.Add("RC-Signature", signature);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}