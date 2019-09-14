using Jane.Sms;
using System.Threading.Tasks;

namespace Jane.QCloud.Sms
{
    public class SmsMessageContext
    {
        public string GroupKey { get; set; }

        public SmsMessage Message { get; set; }

        public TaskCompletionSource<bool> TaskCompletionSource { get; set; }
    }
}