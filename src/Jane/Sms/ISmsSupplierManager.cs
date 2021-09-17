using System.Collections.Generic;

namespace Jane.Sms
{
    public interface ISmsSupplierManager
    {
        void AddSupplier(SmsSupplier supplier);

        IEnumerable<SmsSupplier> GetSuppliersByCountryCode(string countryCode);
    }
}