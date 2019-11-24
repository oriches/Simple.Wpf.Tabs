using Simple.Wpf.Tabs.Models;
using Simple.Wpf.Tabs.ViewModels;
using Simple.Wpf.Tabs.ViewModels.Tabs;

namespace Simple.Wpf.Tabs.Tests.ViewModels
{
    public sealed class FooTabViewModel : BaseViewModel, ITabViewModel
    {
        public FooTabViewModel(Tab tab)
        {
            Tab = tab;
            Name = tab.Name;
        }

        public Tab Tab { get; }

        public string Name { get; }
    }
}