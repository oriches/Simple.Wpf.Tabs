using System;

namespace Simple.Wpf.Tabs.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime Truncate(this DateTime date, long resolution) =>
            new DateTime(date.Ticks - date.Ticks % resolution, date.Kind);
    }
}