using Simple.Wpf.Tabs.Models;

namespace Simple.Wpf.Tabs.ViewModels.Tabs
{
    public abstract class TabBaseViewModel : BaseViewModel
    {
        protected TabBaseViewModel(Tab tab) => Tab = tab;

        public Tab Tab { get; }

        public string Name => Tab.Name;
    }
}