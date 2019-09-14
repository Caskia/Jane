using Jane.Extensions;
using Microsoft.Extensions.Options;
using Jane.QCloud.Cos.Configurations;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Jane.QCloud.Cos
{
    public class QCloudCosService : IQCloudCosService
    {
        private readonly QCloudCosOptions _options;
        private string signAlgorithm = "sha1";

        public QCloudCosService(
            IOptions<QCloudCosOptions> optionsAccessor
            )
        {
            _options = optionsAccessor.Value;
        }

        public string BuildAuthorizationString(SignatureInput input)
        {
            var authorizationKeys = new Dictionary<string, string>()
            {
                { "q-sign-algorithm",   signAlgorithm},
                { "q-ak",               _options.SecretId },
                { "q-sign-time",        input.SignTime },
                { "q-key-time",         input.KeyTime },
                { "q-header-list",      string.Join(";",input.HttpHeaders.Select(p=>p.Key.ToLower())) },
                { "q-url-param-list",   string.Join(";",input.HttpParameters.Select(p=>p.Key.ToLower())) },
                { "q-signature",        Signature(input) }
            };

            var result = string.Join("&", authorizationKeys.Select(k => $"{k.Key}={k.Value}"));
            return result;
        }

        public string GetHost()
        {
            return $"https://{_options.BucketName}-{_options.AppId}.cos.{_options.Region}.myqcloud.com";
        }

        public string GetPathPrefix()
        {
            if (!_options.PathPrefix.IsNullOrEmpty())
            {
                return $"/{_options.PathPrefix}";
            }
            else
            {
                return string.Empty;
            }
        }

        public string Signature(SignatureInput input)
        {
            var signKey = HashHelper.HmacSha1(_options.SecretKey, input.KeyTime);

            var httpParameters = string.Join("&", input.HttpParameters.Select(p => $"{p.Key.ToLower()}={WebUtility.UrlEncode(p.Value)}"));
            var httpHeaders = string.Join("&", input.HttpHeaders.Select(p => $"{p.Key.ToLower()}={WebUtility.UrlEncode(p.Value)}"));
            var httpString = $"{input.HttpMethod}\n{input.HttpUri}\n{httpParameters}\n{httpHeaders}\n";
            var sha1edHttpString = HashHelper.Sha1(httpString);
            var stringToSign = $"{signAlgorithm}\n{input.SignTime}\n{sha1edHttpString}\n";

            return HashHelper.HmacSha1(signKey, stringToSign);
        }
    }
}