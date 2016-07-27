namespace Simple.Wpf.Tabs.ViewModels.Tabs
{
    using Models;

    public abstract class TabBaseViewModel : BaseViewModel
    {
        protected TabBaseViewModel(Tab tab)
        {
            Tab = tab;
        }

        public Tab Tab { get; }

        public string Name { get { return Tab.Name; } }
    }
}