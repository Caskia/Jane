using System;

namespace Jane.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="DateTime"/>.
    /// </summary>
    public static class DateTimeExtensions
    {
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

        public static DateTimeOffset CutOffMilliseconds(this DateTimeOffset dateTimeOffset)
        {
            return new DateTimeOffset(dateTimeOffset.Ticks - (dateTimeOffset.Ticks % TimeSpan.TicksPerSecond), dateTimeOffset.Offset);
        }

        public static DateTime CutOffMilliseconds(this DateTime dateTime)
        {
            return new DateTime(dateTime.Ticks - (dateTime.Ticks % TimeSpan.TicksPerSecond), dateTime.Kind);
        }

        public static string ToDateSuffix(this DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.Day.ToOccurrenceSuffix();
        }

        public static string ToDateSuffix(this DateTime dateTime)
        {
            return dateTime.Day.ToOccurrenceSuffix();
        }
    }
}