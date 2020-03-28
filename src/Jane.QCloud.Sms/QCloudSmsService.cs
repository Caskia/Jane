using Jane.Configurations;
using Jane.Extensions;
using Jane.Sms;
using Jane.Timing;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Jane.QCloud.Sms
{
    public class QCloudSmsService
    {
        #region Fields

        private readonly IDataGenerator _dataGenerator;
        private readonly QCloudSmsOptions _options;
        private readonly IQCloudSmsApi _qCloudSmsApi;
        private readonly IQCloudSmsTemplateService _qCloudSmsTemplateService;

        #endregion Fields

        #region Ctor

        public QCloudSmsService(
            IOptions<QCloudSmsOptions> optionsAccessor,
            IDataGenerator dataGenerator,
            IQCloudSmsApi qCloudSmsApi,
            IQCloudSmsTemplateService qCloudSmsTemplateService
            )
        {
            _options = optionsAccessor.Value;
            _dataGenerator = dataGenerator;
            _qCloudSmsApi = qCloudSmsApi;
            _qCloudSmsTemplateService = qCloudSmsTemplateService;
        }

        #endregion Ctor

        public async Task SendSmsAsync(SmsMessage message)
        {
            if (_options.AppId.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(_options.AppId));
            }

            if (_options.AppSecret.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(_options.AppSecret));
            }

            if (_options.Sign.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(_options.Sign));
            }

            var needSign = message.To.Any(t => t.CountryCode == "86");
            var random = _dataGenerator.GetRandomString(10, true, false, false);

            message.To = message.To.Distinct().ToList();
            if (message.To.Count == 1)
            {
                var smsSingleMessage = BuildQCloudSmsSingleMessage(message, random, needSign);
                var result = await _qCloudSmsApi.SendSingleMessageAsync(_options.AppId, random, smsSingleMessage);
                if (result.Result != 0)
                {
                    throw new Exception($"QCloud send sms message result[{result.Result}] error[{result.ErrorMessage}].");
                }
            }
            else if (message.To.Count > 1)
            {
                var smsMultiMessages = BuildQCloudSmsMultiMessages(message, random, needSign);

                foreach (var smsMultiMessage in smsMultiMessages)
                {
                    var result = await _qCloudSmsApi.SendMultiMessageAsync(_options.AppId, random, smsMultiMessage);
                    if (result.Result != 0)
                    {
                        throw new Exception($"QCloud send sms message result[{result.Result}] error[{result.ErrorMessage}].");
                    }
                }
            }
        }

        private QCloudPhone BuildQCloudPhone(SmsPhoneNumber smsPhoneNumber)
        {
            return new QCloudPhone()
            {
                NationCode = smsPhoneNumber.CountryCode,
                PhoneNumber = smsPhoneNumber.AreaNumber
            };
        }

        private List<QCloudSmsMultiMessage> BuildQCloudSmsMultiMessages(SmsMessage smsMessage, string random, bool needSign = true)
        {
            if (smsMessage.To.Count < 1)
            {
                throw new ArgumentException("QCloud sms multi message do not have enough phone numbers.", nameof(smsMessage.To));
            }

            var messages = new List<QCloudSmsMultiMessage>();

            foreach (var countryGroup in smsMessage.To.GroupBy(t => t.CountryCode))
            {
                var groupPhoneNumbers = countryGroup.Select(g => g);
                int skip = 0;
                int maxCount = 200;
                while (true)
                {
                    var phoneNumbers = groupPhoneNumbers.Skip(skip).Take(maxCount);
                    if (phoneNumbers.Count() == 0)
                    {
                        break;
                    }
                    var qcloudPhoneNumebers = phoneNumbers.Select(p => BuildQCloudPhone(p)).ToList();

                    var timestamp = ((DateTimeOffset)Clock.Now).ToUnixTimeSeconds();
                    messages.Add(new QCloudSmsMultiMessage()
                    {
                        Params = smsMessage.TemplateParameters,
                        Sig = GetSign(string.Join(",", qcloudPhoneNumebers.Select(p => p.PhoneNumber)), _options.AppSecret, random, timestamp),
                        Sign = needSign ? _options.Sign : null,
                        Telphones = qcloudPhoneNumebers,
                        TemplateId = _qCloudSmsTemplateService.GetTemplateId(smsMessage.TemplateCode),
                        Timestamp = timestamp
                    });

                    skip += phoneNumbers.Count();
                }
            }

            return messages;
        }

        private QCloudSmsSingleMessage BuildQCloudSmsSingleMessage(SmsMessage smsMessage, string random, bool needSign = true)
        {
            if (smsMessage.To.Count != 1)
            {
                throw new ArgumentException("QCloud sms single message meet error numbers of phone.", nameof(smsMessage.To));
            }
            var phoneNumber = smsMessage.To.Select(t => BuildQCloudPhone(t)).FirstOrDefault();
            long timestamp = ((DateTimeOffset)Clock.Now).ToUnixTimeSeconds();

            return new QCloudSmsSingleMessage()
            {
                Params = smsMessage.TemplateParameters,
                Sig = GetSign($"{phoneNumber.PhoneNumber}", _options.AppSecret, random, timestamp),
                Sign = needSign ? _options.Sign : null,
                Telphone = phoneNumber,
                TemplateId = _qCloudSmsTemplateService.GetTemplateId(smsMessage.TemplateCode),
                Timestamp = timestamp
            };
        }

        private string GetSign(string phoneNumber, string appKey, string random, long timestamp)
        {
            var data = $"appkey={appKey}&random={random}&time={timestamp.ToString()}&mobile={phoneNumber}";
            using (var hashAlgorithm = SHA256.Create())
            {
                var hash = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }
}