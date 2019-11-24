using System;
using Simple.Wpf.Tabs.ViewModels;

namespace Simple.Wpf.Tabs.Services
{
    public interface IOverlayService : IService
    {
        IObservable<OverlayViewModel> Show { get; }

        void Post(string header, BaseViewModel viewModel, IDisposable lifetime);
    }
}