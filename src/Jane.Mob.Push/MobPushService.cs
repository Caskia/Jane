﻿using Jane.Configurations;
using Jane.Extensions;
using Jane.Logging;
using Jane.Push;
using Microsoft.Extensions.Options;
using Refit;
using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Jane.Mob.Push
{
    public class MobPushService
    {
        public static JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Encoder = JavaScriptEncoder.Create(new TextEncoderSettings(UnicodeRanges.All))
        };

        private readonly ILogger _logger;
        private readonly IMobPushApi _MobPushApi;
        private readonly MobPushOptions _options;

        public MobPushService(
            ILoggerFactory loggerFactory,
            IOptions<MobPushOptions> optionsAccessor,
            IMobPushApi mobPushApi
            )
        {
            _logger = loggerFactory.Create(nameof(MobPushService));
            _options = optionsAccessor.Value;
            _MobPushApi = mobPushApi;
        }

        public async Task<PushMessageOutput> PushAsync(PushMessage input, List<string> tags, List<PushPlatform> pushPlatforms)
        {
            if (tags == null || tags.Count == 0)
            {
                throw new ArgumentException(nameof(tags));
            }

            if (pushPlatforms == null || pushPlatforms.Count == 0)
            {
                throw new ArgumentException(nameof(pushPlatforms));
            }

            var message = new MobPushMessage()
            {
                AppKey = _options.AppKey,
                WorkNo = input.IdempotentKey,
                Scheme = input.Forward?.Scheme,
                Target = 3,
                Tags = tags,
                Type = 1,
                Content = input.Content,
                Extras = JsonSerializer.Serialize(input.CustomData, JsonSerializerOptions)
            };

            if (pushPlatforms.Contains(PushPlatform.Android))
            {
                message.Plats.Add(1);
                message.AndroidTitle = input.Title;
            }

            if (pushPlatforms.Contains(PushPlatform.IOS))
            {
                message.Plats.Add(2);
                message.IOSProduction = Convert.ToInt32(_options.IsIOSProduction);
                message.IOSTitle = input.Title;
            }

            try
            {
                var sign = GetEncryptedSign(message, _options.AppSecret);
                var result = await _MobPushApi.PushAsync(_options.AppKey, sign, message);
                return ProcessPushMessageResult(result);
            }
            catch (ApiException ex)
            {
                var error = await ex.GetContentAsAsync<MobPushResult>();
                return ProcessPushMessageResult(error);
            }
        }

        private string GetEncryptedSign(MobPushMessage message, string secret)
        {
            var jsonContent = JsonSerializer.Serialize(message, JsonSerializerOptions);
            return $"{jsonContent}{secret}".ToMd5();
        }

        private PushMessageOutput ProcessPushMessageResult(MobPushResult result)
        {
            switch (result.Status)
            {
                case 200:
                    return new PushMessageOutput()
                    {
                        MessageId = result.Res.BatchId
                    };

                case 5803:
                case 5408:
                    _logger.Error($"Mob push message error[{result.Status}][{result.Error}]");
                    return null;

                default:
                    throw new UserFriendlyException($"Mob push message error[{result.Status}][{result.Error}]");
            }
        }
    }
}