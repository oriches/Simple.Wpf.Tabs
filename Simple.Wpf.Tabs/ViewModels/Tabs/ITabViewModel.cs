namespace Simple.Wpf.Tabs.ViewModels.Tabs
{
    using Models;

    public interface ITabViewModel : ITransientViewModel
    {
        Tab Tab { get; }

        string Name { get; }
    }
}