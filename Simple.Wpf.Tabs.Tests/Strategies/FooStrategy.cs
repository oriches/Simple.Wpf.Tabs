using System;
using Simple.Wpf.Tabs.Models;
using Simple.Wpf.Tabs.Strategies.Tabs;
using Simple.Wpf.Tabs.Tests.ViewModels;
using Simple.Wpf.Tabs.ViewModels.Tabs;

namespace Simple.Wpf.Tabs.Tests.Strategies
{
    public sealed class FooStrategy : ITabStrategy
    {
        public FooStrategy()
        {
            TypeId = Guid.Parse("BD9052E4-3B39-4CD7-B53B-FA963073CCF9");
            Name = "Foo";
        }

        public Guid TypeId { get; }

        public string Name { get; }

        public bool CanHandle(Tab tab, out ITabViewModel tabViewModel)
        {
            tabViewModel = null;
            if (tab.TypeId == TypeId)
            {
                tabViewModel = new FooTabViewModel(tab);
                return true;
            }

            return false;
        }

        public ITabViewModel Create() => new FooTabViewModel(new Tab(TypeId, Name));
    }
}