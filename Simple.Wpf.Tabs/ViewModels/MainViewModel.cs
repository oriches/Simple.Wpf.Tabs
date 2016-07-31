// ReSharper disable ConvertClosureToMethodGroup
namespace Simple.Wpf.Tabs.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Disposables;
    using Collections;
    using Extensions;
    using Services;
    using Tabs;

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
        
        private void DisposeOfTabs()
        {
            var copy = _tabs.ToArray();

            _tabs.Clear();

            copy.ForEach(x => x.Dispose());
        }

        public IDiagnosticsViewModel Diagnostics { get; }

        public IEnumerable<ITabViewModel> Tabs => _tabs;
    }
}