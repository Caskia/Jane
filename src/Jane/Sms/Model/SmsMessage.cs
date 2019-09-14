using System.Collections.Generic;

namespace Jane.Sms
{
    public class SmsMessage
    {
        public string Body { get; set; }

        public string CallBackUri { get; set; }

        public string GroupKey { get; set; }

        public string IdempotentKey { get; set; }

        public string TemplateCode { get; set; }

        public List<string> TemplateParameters { get; set; }

        public List<SmsPhoneNumber> To { get; set; }
    }
}