using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jane.Sms
{
    public interface ISmsSupplierManager
    {
        Task AddSupplier(SmsSupplier supplier);

        Task<IEnumerable<SmsSupplier>> GetSuppliersByCountryCode(string countryCode);
    }
}