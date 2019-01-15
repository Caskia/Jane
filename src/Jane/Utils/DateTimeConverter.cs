using Jane.Timing;
using System;

namespace Jane.Utils
{
    public static class DateTimeConverter
    {
        public static DateTime ConvertFromTimestampMilliseconds(double timestamp)
        {
            var converted = new DateTime(1970, 1, 1, 0, 0, 0, 0);

            var newDateTime = converted.AddMilliseconds(timestamp);

            return newDateTime;
        }

        public static DateTime ConvertFromTimestampSeconds(double timestamp)
        {
            var converted = new DateTime(1970, 1, 1, 0, 0, 0, 0);

            var newDateTime = converted.AddSeconds(timestamp);

            return newDateTime;
        }

        public static double ConvertToTimestampMilliseconds(DateTime dateTimeUtc)
        {
            var span = dateTimeUtc - new DateTime(1970, 1, 1, 0, 0, 0, 0);

            return span.TotalMilliseconds;
        }

        public static double ConvertToTimestampSeconds(DateTime dateTimeUtc)
        {
            var span = dateTimeUtc - new DateTime(1970, 1, 1, 0, 0, 0, 0);

            return Math.Ceiling(span.TotalSeconds);
        }

        public static TimeSpan ConvertToTimezoneOffset(string windowsTimeZoneId)
        {
            var tzi = TimeZoneInfo.FindSystemTimeZoneById(windowsTimeZoneId);
            return tzi.GetUtcOffset(Clock.Now);
        }
    }
}