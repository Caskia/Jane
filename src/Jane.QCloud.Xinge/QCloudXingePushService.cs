using Jane.Push;
using Jane;
using Jane.Extensions;
using Microsoft.Extensions.Options;
using Jane.QCloud.Xinge.Configurations;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jane.QCloud.Xinge
{
    public class QCloudXingePushService : IPushService
    {
        private readonly QCloudXingeOptions _options;
        private readonly IQCloudXingeApi _qCloudXingeApi;

        private string _androidAuthorizationHeader;
        private string _iosAuthorizationHeader;

        public QCloudXingePushService(
            IOptions<QCloudXingeOptions> optionsAccessor,
            IQCloudXingeApi qCloudXingeApi
            )
        {
            _options = optionsAccessor.Value;
            _qCloudXingeApi = qCloudXingeApi;
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
            var xingeMessage = new XingePushMessage()
            {
                AudienceType = "account_list",
                AccountList = new List<string>()
                {
                    $"{message.AccountType}:{message.AccountId}"
                },
                MessageType = "notify",
                Platform = platform.ToString().ToLower()
            };

            if (platform == PushPlatform.Android)
            {
                xingeMessage.Message = new XingePushAndroidMessage()
                {
                    Title = message.Title,
                    Content = message.Content,
                    Android = new XingePushAndroidPayload()
                    {
                        Action = new XingePushAndroidPayloadAction()
                        {
                            ActionType = 1
                        },
                        CustomContent = message.CustomData
                    }
                };
            }
            else
            {
                xingeMessage.Message = new XingePushIOSMessage()
                {
                    Title = message.Title,
                    Content = message.Content,
                    IOS = new XingePushIOSPayload()
                    {
                        Aps = new XingePushIOSPayloadAps()
                        {
                            Alert = new XingePushIOSPayloadAlert()
                        },
                        Body = message.CustomData
                    }
                };
                xingeMessage.Environment = _options.IOSKey.Environment;
            }

            try
            {
                var result = await _qCloudXingeApi.PushAsync(GetAuthorizationHeader(platform), xingeMessage);
                return ProcessPushMessageResult(result);
            }
            catch (ApiException ex)
            {
                var error = await ex.GetContentAsAsync<XingePushResult>();
                return ProcessPushMessageResult(error);
            }
        }

        private string GetAndroidAuthorizationHeader()
        {
            if (!_androidAuthorizationHeader.IsNullOrEmpty())
            {
                return _androidAuthorizationHeader;
            }

            _androidAuthorizationHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_options.AndroidKey.AppId}:{_options.AndroidKey.SecretKey}"));

            return $"Basic {_androidAuthorizationHeader}";
        }

        private string GetAuthorizationHeader(PushPlatform platform)
        {
            if (platform == PushPlatform.Android)
            {
                return GetAndroidAuthorizationHeader();
            }
            else
            {
                return GetIOSAuthorizationHeader();
            }
        }

        private string GetIOSAuthorizationHeader()
        {
            if (!_iosAuthorizationHeader.IsNullOrEmpty())
            {
                return _iosAuthorizationHeader;
            }

            _iosAuthorizationHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_options.IOSKey.AppId}:{_options.IOSKey.SecretKey}"));

            return $"Basic {_iosAuthorizationHeader}";
        }

        private PushMessageOutput ProcessPushMessageResult(XingePushResult result)
        {
            if (result.RetCode.HasValue && result.RetCode != 0)
            {
                throw new UserFriendlyException($"qcloud xinge push message error[{result.Environment}][{result.RetCode}]");
            }

            return new PushMessageOutput()
            {
                MessageId = result.PushId
            };
        }
    }
}