namespace Simple.Wpf.Tabs.Strategies.Tabs
{
    using System;
    using Models;
    using ViewModels.Tabs;

    public sealed class BlueTabStrategy : ITabStrategy
    {
        private readonly Func<Tab, IBlueTabViewModel> _factory;
        public static readonly Tab Default = new Tab(Guid.Parse("{F0F9D927-7E08-4E17-AF1B-106B5DCF1C22}"), "Blue");

        public Guid TypeId { get; }

        public string Name { get; }

        public BlueTabStrategy(Func<Tab, IBlueTabViewModel> factory)
        {
            _factory = factory;

            TypeId = Default.TypeId;
            Name = Default.Name;
        }

        public bool CanHandle(Tab tab, out ITabViewModel tabViewModel)
        {
            tabViewModel = null;
            if (tab.TypeId == Default.TypeId)
            {
                tabViewModel = _factory(tab);
                return true;
            }

            return false;
        }

        public ITabViewModel Create()
        {
            return new BlueTabViewModel(new Tab(TypeId, Name));
        }
    }
}
