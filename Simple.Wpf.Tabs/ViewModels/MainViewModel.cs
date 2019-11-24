// ReSharper disable ConvertClosureToMethodGroup

using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using Simple.Wpf.Tabs.Collections;
using Simple.Wpf.Tabs.Extensions;
using Simple.Wpf.Tabs.Services;
using Simple.Wpf.Tabs.ViewModels.Tabs;

namespace Simple.Wpf.Tabs.ViewModels
{
    public sealed class MainViewModel : BaseViewModel, IMainViewModel
    {
        private readonly RangeObservableCollection<ITabViewModel> _tabs;

        public MainViewModel(IDiagnosticsViewModel diagnosticsViewModel, ITabsService tabsService)
        {
            Diagnostics = diagnosticsViewModel;

            _tabs = new RangeObservableCollection<ITabViewModel>();
            _tabs.AddRange(tabsService.GetTabs());

            Disposable.Create(() => DisposeOfTabs())
                .DisposeWith(this);
        }

        public IEnumerable<ITabViewModel> Tabs => _tabs;

        public IDiagnosticsViewModel Diagnostics { get; }

        private void DisposeOfTabs()
        {
            var copy = _tabs.ToArray();

            _tabs.Clear();

            copy.ForEach(x => x.Dispose());
        }
    }
}