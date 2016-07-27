namespace Simple.Wpf.Tabs.Services
{
    using System;

    public interface IDateTimeService : IService
    {
        DateTimeOffset Now { get; }
    }
}