using System;
using Simple.Wpf.Tabs.Models;
using Simple.Wpf.Tabs.ViewModels.Tabs;

namespace Simple.Wpf.Tabs.Strategies.Tabs
{
    public sealed class BlueTabStrategy : ITabStrategy
    {
        public static readonly Tab Default = new Tab(Guid.Parse("{F0F9D927-7E08-4E17-AF1B-106B5DCF1C22}"), "Blue");
        private readonly Func<Tab, IBlueTabViewModel> _factory;

        public BlueTabStrategy(Func<Tab, IBlueTabViewModel> factory)
        {
            _factory = factory;

            TypeId = Default.TypeId;
            Name = Default.Name;
        }

        public Guid TypeId { get; }

        public string Name { get; }

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

        public ITabViewModel Create() => new BlueTabViewModel(new Tab(TypeId, Name));
    }
}