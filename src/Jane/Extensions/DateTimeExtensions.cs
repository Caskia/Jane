using System;
using System.Linq;

namespace Jane.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="DateTime"/>.
    /// </summary>
    public static class DateTimeExtensions
    {
        //...
        private static DateTime _epochStartDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long ConvertDateTimeToEpoch(this DateTime datetime)
        {
            if (datetime < _epochStartDateTime) return 0;

            return Convert.ToInt64((datetime.ToUniversalTime() - _epochStartDateTime).TotalSeconds);
        }

        public static DateTime ConvertEpochToDateTime(long seconds)
        {
            return _epochStartDateTime.AddSeconds(seconds);
        }

        public static DateTimeOffset CutOffHours(this DateTimeOffset dateTimeOffset)
        {
            return new DateTimeOffset(dateTimeOffset.Ticks - (dateTimeOffset.Ticks % TimeSpan.TicksPerDay), dateTimeOffset.Offset);
        }

        public static DateTime CutOffHours(this DateTime dateTime)
        {
            return new DateTime(dateTime.Ticks - (dateTime.Ticks % TimeSpan.TicksPerDay), dateTime.Kind);
        }

        public static DateTimeOffset CutOffSeconds(this DateTimeOffset dateTimeOffset)
        {
            return new DateTimeOffset(dateTimeOffset.Ticks - (dateTimeOffset.Ticks % TimeSpan.TicksPerMinute), dateTimeOffset.Offset);
        }

        public static DateTime CutOffSeconds(this DateTime dateTime)
        {
            return new DateTime(dateTime.Ticks - (dateTime.Ticks % TimeSpan.TicksPerMinute), dateTime.Kind);
        }

        public static bool IsUtcDst(this DateTime dateTimeUtc, TimeZoneInfo timeZoneInfo)
        {
            return timeZoneInfo.IsDaylightSavingTime(dateTimeUtc.AddHours(timeZoneInfo.BaseUtcOffset.TotalHours));
        }

        public static bool IsUtcDst(this DateTime dateTimeUtc, string timeZoneId)
        {
            if (string.IsNullOrEmpty(timeZoneId))
            {
                return false;
            }
            var timeZoneInfo = TimeZoneInfo.GetSystemTimeZones().FirstOrDefault(p => p.Id == timeZoneId);
            if (timeZoneInfo == null)
            {
                return false;
            }
            return dateTimeUtc.IsUtcDst(timeZoneInfo);
        }

        public static string ToDateSuffix(this DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.Day.ToOccurrenceSuffix();
        }

        public static string ToDateSuffix(this DateTime dateTime)
        {
            return dateTime.Day.ToOccurrenceSuffix();
        }

        public static DateTime ToLocalTime(this DateTime utcTime, string windowsTimeZoneId)
        {
            var tzi = TimeZoneInfo.FindSystemTimeZoneById(windowsTimeZoneId);
            var tziLocal = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi);
            return tziLocal;
        }

        public static DateTimeOffset ToLocalTime(this DateTimeOffset utcTime, string windowsTimeZoneId)
        {
            var tzi = TimeZoneInfo.FindSystemTimeZoneById(windowsTimeZoneId);
            var tziLocal = TimeZoneInfo.ConvertTime(utcTime, tzi);
            return tziLocal;
        }

        public static DateTime ToUtcTime(this DateTime localTime, string windowsTimeZoneId)
        {
            var tzi = TimeZoneInfo.FindSystemTimeZoneById(windowsTimeZoneId);
            var tziLocal = TimeZoneInfo.ConvertTimeToUtc(localTime, tzi);
            return tziLocal;
        }
    }
}