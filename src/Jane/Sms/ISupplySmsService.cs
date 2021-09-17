using System.Threading.Tasks;

namespace Jane.Sms
{
    public interface ISupplySmsService
    {
        Task SendSmsAsync(SmsMessage message);
    }
}