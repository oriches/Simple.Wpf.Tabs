using Simple.Wpf.Tabs.Models;

namespace Simple.Wpf.Tabs.ViewModels.Tabs
{
    public interface ITabViewModel : ITransientViewModel
    {
        Tab Tab { get; }

        string Name { get; }
    }
}