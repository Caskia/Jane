using System.Threading.Tasks;

namespace Jane.Sms
{
    public interface ISmsService
    {
        Task SendSmsAsync(SmsMessage message);
    }
}