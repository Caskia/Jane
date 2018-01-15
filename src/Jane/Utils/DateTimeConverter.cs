using Jane.Timing;
using System;

namespace Jane.Utils
{
    public static class DateTimeConverter
    {
        public static DateTime ConvertFromTimestampSeconds(double timestamp)
        {
            DateTime converted = new DateTime(1970, 1, 1, 0, 0, 0, 0);

            DateTime newDateTime = converted.AddSeconds(timestamp);

            return newDateTime;
        }

        public static double ConvertToTimestampSeconds(DateTime dateTimeUtc)
        {
            TimeSpan span = dateTimeUtc - new DateTime(1970, 1, 1, 0, 0, 0, 0);

            return (double)span.TotalSeconds;
        }

        public static TimeSpan ConvertToTimezoneOffset(string windowsTimeZoneId)
        {
            var tzi = TimeZoneInfo.FindSystemTimeZoneById(windowsTimeZoneId);
            return tzi.GetUtcOffset(Clock.Now);
        }
    }
}