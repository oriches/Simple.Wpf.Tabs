// ReSharper disable ConvertClosureToMethodGroup

using System;
using System.Reactive.Disposables;
using Simple.Wpf.Tabs.Commands;
using Simple.Wpf.Tabs.Extensions;
using Simple.Wpf.Tabs.Services;

namespace Simple.Wpf.Tabs.ViewModels
{
    public sealed class ChromeViewModel : BaseViewModel, IChromeViewModel
    {
        private OverlayViewModel _overlay;

        public ChromeViewModel(IMainViewModel main, IOverlayService overlayService)
        {
            Main = main;

            overlayService.Show
                .Subscribe(x => UpdateOverlay(x))
                .DisposeWith(this);

            CloseOverlayCommand = ReactiveCommand<object>.Create()
                .DisposeWith(this);

            CloseOverlayCommand.Subscribe(x => ClearOverlay())
                .DisposeWith(this);

            Disposable.Create(() => CloseOverlayCommand = null)
                .DisposeWith(this);
        }

        public IMainViewModel Main { get; }

        public ReactiveCommand<object> CloseOverlayCommand { get; private set; }

        public bool HasOverlay => _overlay != null;

        public string OverlayHeader => _overlay != null ? _overlay.Header : string.Empty;

        public BaseViewModel Overlay => _overlay?.ViewModel;

        private void ClearOverlay()
        {
            using (_overlay.Lifetime)
            {
                UpdateOverlayImpl(null);
            }
        }

        private void UpdateOverlay(OverlayViewModel overlay)
        {
            using (SuspendNotifications())
            {
                if (_overlay != null) ClearOverlay();

                UpdateOverlayImpl(overlay);
            }
        }

        private void UpdateOverlayImpl(OverlayViewModel overlay)
        {
            _overlay = overlay;

            OnPropertyChanged(() => HasOverlay);
            OnPropertyChanged(() => Overlay);
            OnPropertyChanged(() => OverlayHeader);
        }
    }
}