namespace Simple.Wpf.Tabs.Services
{
    using System;
    using System.Reactive;

    public interface IIdleService : IService
    {
        IObservable<Unit> Idling { get; }
    }
}