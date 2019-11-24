using System;
using System.Reactive.Subjects;
using Simple.Wpf.Tabs.Extensions;
using Simple.Wpf.Tabs.Models;
using Simple.Wpf.Tabs.ViewModels;

namespace Simple.Wpf.Tabs.Services
{
    public sealed class OverlayService : DisposableObject, IOverlayService
    {
        private readonly Subject<OverlayViewModel> _show;

        public OverlayService()
        {
            using (Duration.Measure(Logger, "Constructor - " + GetType().Name))
            {
                _show = new Subject<OverlayViewModel>()
                    .DisposeWith(this);
            }
        }

        public void Post(string header, BaseViewModel viewModel, IDisposable lifetime)
        {
            _show.OnNext(new OverlayViewModel(header, viewModel, lifetime));
        }

        public IObservable<OverlayViewModel> Show => _show;
    }
}