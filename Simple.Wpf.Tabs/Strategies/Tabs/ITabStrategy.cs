using System;
using Simple.Wpf.Tabs.Models;
using Simple.Wpf.Tabs.ViewModels.Tabs;

namespace Simple.Wpf.Tabs.Strategies.Tabs
{
    public interface ITabStrategy : IStrategy
    {
        Guid TypeId { get; }

        string Name { get; }

        bool CanHandle(Tab tab, out ITabViewModel tabViewModel);

        ITabViewModel Create();
    }
}