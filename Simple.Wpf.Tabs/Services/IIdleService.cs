using System;
using System.Reactive;

namespace Simple.Wpf.Tabs.Services
{
    public interface IIdleService : IService
    {
        IObservable<Unit> Idling { get; }
    }
}