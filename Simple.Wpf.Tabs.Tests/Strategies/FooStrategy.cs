namespace Simple.Wpf.Tabs.Tests.Strategies
{
    using System;
    using Models;
    using Tabs.Strategies.Tabs;
    using Tabs.ViewModels.Tabs;
    using ViewModels;

    public sealed class FooStrategy : ITabStrategy
    {
        public Guid TypeId { get; }

        public string Name { get; }

        public FooStrategy()
        {
            TypeId = Guid.Parse("BD9052E4-3B39-4CD7-B53B-FA963073CCF9");
            Name = "Foo";
        }

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

        public ITabViewModel Create()
        {
            return new FooTabViewModel(new Tab(TypeId, Name));
        }
    }
}
