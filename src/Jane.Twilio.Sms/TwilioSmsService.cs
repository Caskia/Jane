using Jane.Extensions;
using Jane.Sms;
using Jane.Twilio.Sms.Configurations;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Jane.Twilio.Sms
{
    public class TwilioSmsService : ISmsService
    {
        #region Fields

        private readonly TwilioSmsOptions _options;
        private bool _hasInitClient = false;

        #endregion Fields

        #region Ctor

        public TwilioSmsService(
            IOptions<TwilioSmsOptions> optionsAccessor
            )
        {
            _options = optionsAccessor.Value;
        }

        #endregion Ctor

        public async Task SendSmsAsync(SmsMessage message)
        {
            TryInitClient();

            var phoneNumbers = message.To.Select(t => new PhoneNumber($"+{t.CountryCode}{t.AreaNumber}"));
            var tasks = new List<Task<MessageResource>>();
            foreach (var phoneNumber in phoneNumbers)
            {
                tasks.Add(MessageResource.CreateAsync(to: phoneNumber, messagingServiceSid: _options.MessageServiceSid, body: message.Body));
            }
            await Task.WhenAll(tasks);
        }

        private void TryInitClient()
        {
            if (_options.AccountSid.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(_options.AccountSid));
            }
            if (_options.AuthToken.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(_options.AuthToken));
            }
            if (_options.MessageServiceSid.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(_options.MessageServiceSid));
            }

            if (!_hasInitClient)
            {
                TwilioClient.Init(_options.AccountSid, _options.AuthToken);
            }
        }
    }
}