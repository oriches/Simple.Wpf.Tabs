using System;
using System.Reactive;

namespace Simple.Wpf.Tabs.Services
{
    public interface IHeartbeatService : IService
    {
        IObservable<Unit> Listen { get; }
    }
}