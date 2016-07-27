namespace Simple.Wpf.Tabs.Services
{
    using System;
    using Models;

    public interface IDiagnosticsService : IService
    {
        IObservable<Memory> Memory { get; }

        IObservable<int> Cpu { get; }
    }

}