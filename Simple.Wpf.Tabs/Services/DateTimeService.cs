namespace Simple.Wpf.Tabs.Services
{
    using System;

    public sealed class DateTimeService : IDateTimeService
    {
        public DateTimeOffset Now
        {
            get { return DateTimeOffset.Now; }
        }
    }
}
