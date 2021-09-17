namespace Jane.Sms
{
    public class SmsSupplier
    {
        public string CountryCode { get; set; }

        public ISupplySmsService Service { get; set; }
    }
}