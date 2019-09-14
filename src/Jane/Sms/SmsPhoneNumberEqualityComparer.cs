using System;
using System.Collections.Generic;

namespace Jane.Sms
{
    public class SmsPhoneNumberEqualityComparer : IEqualityComparer<SmsPhoneNumber>
    {
        public bool Equals(SmsPhoneNumber left, SmsPhoneNumber right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            return left.CountryCode == right.CountryCode && left.AreaNumber == right.AreaNumber;
        }

        public int GetHashCode(SmsPhoneNumber obj)
        {
            return obj.GetHashCode();
        }
    }
}