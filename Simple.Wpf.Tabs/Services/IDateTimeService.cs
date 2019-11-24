using System;

namespace Simple.Wpf.Tabs.Services
{
    public interface IDateTimeService : IService
    {
        DateTimeOffset Now { get; }
    }
}