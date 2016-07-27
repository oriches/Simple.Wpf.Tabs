namespace Simple.Wpf.Tabs.Strategies.Tabs
{
    using System;
    using Models;
    using ViewModels.Tabs;

    public interface ITabStrategy : IStrategy
    {
        Guid TypeId { get; }

        string Name { get; }

        bool CanHandle(Tab tab, out ITabViewModel tabViewModel);

        ITabViewModel Create();
    }
}
