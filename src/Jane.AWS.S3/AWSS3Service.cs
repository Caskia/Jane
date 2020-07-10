using Jane.Configurations;
using Jane.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Jane.AWS.S3
{
    public class AWSS3Service : IAWSS3Service
    {
        private readonly AWSS3Options _options;

        public AWSS3Service(
            IOptions<AWSS3Options> optionsAccessor
            )
        {
            _options = optionsAccessor.Value;
        }

        public string Algorithm
        {
            get
            {
                return "AWS4-HMAC-SHA256";
            }
        }

        private JsonSerializerOptions JsonSerializerOptions
        {
            get
            {
                var encoderSettings = new TextEncoderSettings();
                encoderSettings.AllowRanges(UnicodeRanges.All);
                var options = new JsonSerializerOptions()
                {
                    Encoder = JavaScriptEncoder.Create(encoderSettings)
                };

                return options;
            }
        }

        public string GetHost(string region = null, string bucketName = null)
        {
            return $"https://{(bucketName.IsNullOrEmpty() ? _options.BucketName : bucketName)}.s3.{(region.IsNullOrEmpty() ? _options.Region : region)}.amazonaws.com";
        }

        public string GetPathPrefix()
        {
            if (!_options.PathPrefix.IsNullOrEmpty())
            {
                return $"{_options.PathPrefix}/";
            }
            else
            {
                return string.Empty;
            }
        }

        public SignatureOut GetSignature(GetSignatureInput input)
        {
            var region = _options.Region;
            if (input.Region.IsNullOrEmpty())
            {
                region = _options.Region;
            }

            var bucketName = _options.BucketName;
            if (input.BucketName.IsNullOrEmpty())
            {
                bucketName = _options.BucketName;
            }

            var policy = BuildPolicy(input.SignTime, input.ExpirationTime, input.Key, input.ContentType, input.Acl, region, bucketName);
            var base64Policy = Convert.ToBase64String(JsonSerializer.SerializeToUtf8Bytes(policy, JsonSerializerOptions));

            var dateKey = new HMACSHA256(Encoding.UTF8.GetBytes($"AWS4{_options.SecretAccessKey}")).ComputeHash(Encoding.UTF8.GetBytes(input.SignTime.ToString("yyyyMMdd")));
            var dateRegionKey = new HMACSHA256(dateKey).ComputeHash(Encoding.UTF8.GetBytes(region));
            var dateRegionServiceKey = new HMACSHA256(dateRegionKey).ComputeHash(Encoding.UTF8.GetBytes("s3"));
            var signingKey = new HMACSHA256(dateRegionServiceKey).ComputeHash(Encoding.UTF8.GetBytes("aws4_request"));

            var signatureBuffer = new HMACSHA256(signingKey).ComputeHash(Encoding.UTF8.GetBytes(base64Policy));
            var base64Signature = Convert.ToBase64String(signatureBuffer);
            return new SignatureOut()
            {
                Signature = string.Concat(signatureBuffer.Select(k => k.ToString("x2"))),
                Policy = base64Policy
            };
        }

        private Policy BuildPolicy(DateTime signTime, DateTime expirationTime, string key, string contentType, string acl, string region, string bucketName)
        {
            return new Policy()
            {
                Expiration = expirationTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                Conditions = new List<object>()
                {
                    new Dictionary<string, string>()
                    {
                        { "bucket", bucketName.IsNullOrEmpty() ? _options.BucketName:bucketName }
                    },
                    new Dictionary<string, string>()
                    {
                        { "key", key }
                    },
                    new Dictionary<string, string>()
                    {
                        { "acl", acl }
                    },
                    new Dictionary<string, string>()
                    {
                        { "Content-Type", contentType }
                    },
                    new Dictionary<string, string>()
                    {
                        { "x-amz-credential", GetCredential(signTime,region) }
                    },
                    new Dictionary<string, string>()
                    {
                        { "x-amz-algorithm", Algorithm }
                    },
                    new Dictionary<string, string>()
                    {
                        { "x-amz-date", signTime.ToString("yyyyMMddTHHmmssZ") }
                    }
                }
            };
        }

        private string GetCredential(DateTime date, string region)
        {
            return $"{_options.AccessKeyId}/{date.ToString("yyyyMMdd")}/{region}/s3/aws4_request";
        }
    }
}