using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jane.Sms
{
    public class DefaultSmsService : ISmsService
    {
        private readonly Random _random = new();
        private readonly ISmsSupplierManager _smsSupplierManager;

        public DefaultSmsService(ISmsSupplierManager smsSupplierManager)
        {
            _smsSupplierManager = smsSupplierManager;
        }

        public async Task SendSmsAsync(SmsMessage message)
        {
            var tasks = new List<Task>();
            var tos = message.To.GroupBy(t => t.CountryCode);
            foreach (var to in tos)
            {
                var suppliers = await _smsSupplierManager.GetSuppliersByCountryCode(to.Key);
                if (!suppliers.Any())
                {
                    throw new JaneException($"can not find supplier for country code[{to.Key}], need register.");
                }

                var supplier = suppliers.ElementAt(_random.Next(0, suppliers.Count()));
                tasks.Add(supplier.Service.SendSmsAsync(new SmsMessage
                {
                    Body = message.Body,
                    CallBackUri = message.CallBackUri,
                    GroupKey = message.GroupKey,
                    IdempotentKey = message.IdempotentKey,
                    TemplateCode = message.TemplateCode,
                    TemplateParameters = message.TemplateParameters,
                    To = to.ToList()
                }));
            }

            await Task.WhenAll(tasks);
        }
    }
}