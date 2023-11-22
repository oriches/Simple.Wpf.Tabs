using System;
using Simple.Wpf.Tabs.Models;
using Simple.Wpf.Tabs.Strategies.Tabs;
using Simple.Wpf.Tabs.Tests.ViewModels;
using Simple.Wpf.Tabs.ViewModels.Tabs;

namespace Simple.Wpf.Tabs.Tests.Strategies
{
    public sealed class BarStrategy : ITabStrategy
    {
        public BarStrategy()
        {
            TypeId = Guid.Parse("8F5B5229-E7A5-4C99-9026-BA9664D674CB");
            Name = "Bar";
        }

        public Guid TypeId { get; }

        public string Name { get; }

        public bool CanHandle(Tab tab, out ITabViewModel tabViewModel)
        {
            tabViewModel = null;
            if (tab.TypeId == TypeId)
            {
                tabViewModel = new BarTabViewModel(tab);
                return true;
            }

            return false;
        }

        public ITabViewModel Create() => new BarTabViewModel(new Tab(TypeId, Name));
    }
}