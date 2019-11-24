using System;

namespace Simple.Wpf.Tabs.Services
{
    public sealed class DateTimeService : IDateTimeService
    {
        public DateTimeOffset Now => DateTimeOffset.Now;
    }
}