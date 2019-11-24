using Simple.Wpf.Tabs.ViewModels.Tabs;

namespace Simple.Wpf.Tabs.Services
{
    public interface ITabsService : IService
    {
        ITabViewModel[] GetTabs();
    }
}