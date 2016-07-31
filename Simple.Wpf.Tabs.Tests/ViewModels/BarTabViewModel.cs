namespace Simple.Wpf.Tabs.Tests.ViewModels
{
    using Models;
    using Tabs.ViewModels;
    using Tabs.ViewModels.Tabs;

    public sealed class BarTabViewModel : BaseViewModel, ITabViewModel
    {
        public Tab Tab { get; }

        public string Name { get; }

        public BarTabViewModel(Tab tab)
        {
            Tab = tab;
            Name = tab.Name;
        }
    }
}