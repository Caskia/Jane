using System;
using System.Collections.Generic;
using System.Linq;

namespace Jane.Sms
{
    public class DefaultSmsSupplierManager : ISmsSupplierManager
    {
        private readonly List<SmsSupplier> suppliers = new();

        public void AddSupplier(SmsSupplier supplier)
        {
            if (supplier == null)
            {
                throw new ArgumentNullException(nameof(supplier));
            }

            suppliers.Add(supplier);
        }

        public IEnumerable<SmsSupplier> GetSuppliersByCountryCode(string countryCode)
        {
            return countryCode switch
            {
                "86" => suppliers.Where(s => s.CountryCode == "86"),
                _ => suppliers.Where(s => s.CountryCode != "86")
            };
        }
    }
}