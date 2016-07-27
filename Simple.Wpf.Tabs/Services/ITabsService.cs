namespace Simple.Wpf.Tabs.Services
{
    using ViewModels.Tabs;

    public interface ITabsService : IService
    {
        ITabViewModel[] GetTabs();
    }
}