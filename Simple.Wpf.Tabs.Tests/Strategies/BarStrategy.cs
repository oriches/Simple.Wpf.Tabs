namespace Simple.Wpf.Tabs.Tests.Strategies
{
    using System;
    using Models;
    using Tabs.Strategies.Tabs;
    using Tabs.ViewModels.Tabs;
    using ViewModels;

    public sealed class BarStrategy : ITabStrategy
    {
        public Guid TypeId { get; }

        public string Name { get; }

        public BarStrategy()
        {
            TypeId = Guid.Parse("8F5B5229-E7A5-4C99-9026-BA9664D674CB");
            Name = "Bar";
        }

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

        public ITabViewModel Create()
        {
            return new BarTabViewModel(new Tab(TypeId, Name));
        }
    }
}