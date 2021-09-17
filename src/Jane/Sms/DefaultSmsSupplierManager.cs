using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jane.Sms
{
    public class DefaultSmsSupplierManager : ISmsSupplierManager
    {
        private readonly List<SmsSupplier> suppliers = new();

        public Task AddSupplier(SmsSupplier supplier)
        {
            if (supplier == null)
            {
                throw new ArgumentNullException(nameof(supplier));
            }

            suppliers.Add(supplier);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<SmsSupplier>> GetSuppliersByCountryCode(string countryCode)
        {
            return Task.FromResult(countryCode switch
            {
                "86" => suppliers.Where(s => s.CountryCode == "86"),
                _ => suppliers.Where(s => s.CountryCode != "86")
            });
        }
    }
}