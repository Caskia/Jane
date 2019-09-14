using Jane.Configurations;
using Jane.Extensions;
using Jane.Push;
using Jane.Timing;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jane.UMeng.Push
{
    public class UMengPushService : IPushService
    {
        public static string ApiUrl = "https://msgapi.umeng.com";

        public static JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        private readonly UMengPushOptions _options;
        private readonly string _pushMessageHttpMethod = "POST";
        private readonly string _pushMessageUrl;
        private readonly IUMengPushApi _uMengPushApi;

        public UMengPushService(
            IOptions<UMengPushOptions> optionsAccessor,
            IUMengPushApi uMengPushApi
            )
        {
            _options = optionsAccessor.Value;
            _uMengPushApi = uMengPushApi;
            _pushMessageUrl = $"{ApiUrl}/api/send";
        }

        public async Task<PushMessageOutput> PushAsync(PushMessage message)
        {
            var tasks = new List<Task<PushMessageOutput>>();
            tasks.Add(PushAsync(message, PushPlatform.Android));
            tasks.Add(PushAsync(message, PushPlatform.IOS));

            var results = await Task.WhenAll(tasks);

            return new PushMessageOutput()
            {
                MessageId = string.Join(",", results.Select(r => r.MessageId)),
                TaskId = string.Join(",", results.Select(r => r.TaskId))
            };
        }

        public async Task<PushMessageOutput> PushAsync(PushMessage message, PushPlatform platform)
        {
            UMengPushMessageResult result;
            if (platform == PushPlatform.Android)
            {
                var uMengMessage = new UMengPushMessage<UMengAndroidPayload, UMengAndroidPolicy>()
                {
                    AppKey = _options.AndroidKey.AppKey,
                    TimeStamp = new DateTimeOffset(Clock.Now).ToUnixTimeSeconds().ToString(),
                    Type = "customizedcast",
                    AliasType = message.AccountType,
                    Alias = message.AccountId,
                    Payload = new UMengAndroidPayload()
                    {
                        DisplayType = "notification",
                        Body = new UMengAndroidPayloadBody()
                        {
                            Ticker = message.Title,
                            Title = message.Title,
                            Text = message.Content,
                            AfterOpen = "go_app",
                            Custom = message.CustomData
                        }
                    }
                };

                var sign = GetEncryptedSign(_pushMessageHttpMethod, _pushMessageUrl, JsonConvert.SerializeObject(uMengMessage, JsonSerializerSettings), _options.AndroidKey.AppSecret);

                try
                {
                    result = await _uMengPushApi.SendAsync(sign, uMengMessage);
                }
                catch (ApiException ex)
                {
                    var error = await ex.GetContentAsAsync<UMengPushMessageResult>();
                    return ProcessPushMessageResult(error);
                }
            }
            else
            {
                var uMengMessage = new UMengPushMessage<UMengIOSPayload, UMengIOSPolicy>()
                {
                    AppKey = _options.IOSKey.AppKey,
                    TimeStamp = new DateTimeOffset(Clock.Now).ToUnixTimeSeconds().ToString(),
                    Type = "customizedcast",
                    AliasType = message.AccountType,
                    Alias = message.AccountId,
                    Payload = new UMengIOSPayload()
                    {
                        Aps = new UMengIOSPayloadAps()
                        {
                            Alert = new UMengIOSPayloadAlert()
                            {
                                Title = message.Title,
                                Body = message.Content
                            },
                        },
                        Body = message.CustomData
                    },
                    ProductionMode = _options.IOSKey.ProductionMode
                };

                var sign = GetEncryptedSign(_pushMessageHttpMethod, _pushMessageUrl, JsonConvert.SerializeObject(uMengMessage, JsonSerializerSettings), _options.IOSKey.AppSecret);

                try
                {
                    result = await _uMengPushApi.SendAsync(sign, uMengMessage);
                }
                catch (ApiException ex)
                {
                    var error = await ex.GetContentAsAsync<UMengPushMessageResult>();
                    return ProcessPushMessageResult(error);
                }
            }

            return ProcessPushMessageResult(result);
        }

        private string GetEncryptedSign(string httpMethod, string url, string body, string secret)
        {
            return $"{httpMethod}{url}{body}{secret}".ToMd5().ToLower();
        }

        private PushMessageOutput ProcessPushMessageResult(UMengPushMessageResult result)
        {
            if (result.Ret == "SUCCESS")
            {
                return new PushMessageOutput()
                {
                    MessageId = result.Data.MsgId,
                    TaskId = result.Data.TaskId
                };
            }
            else
            {
                throw new UserFriendlyException($"umeng push message error[{result.Data.ErrorCode}][{result.Data.ErrorMessage}]");
            }
        }
    }
}