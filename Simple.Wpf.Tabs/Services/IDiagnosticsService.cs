using System;
using Simple.Wpf.Tabs.Models;

namespace Simple.Wpf.Tabs.Services
{
    public interface IDiagnosticsService : IService
    {
        IObservable<Memory> Memory { get; }

        IObservable<int> Cpu { get; }
    }
}