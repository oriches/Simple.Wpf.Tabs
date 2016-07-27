namespace Simple.Wpf.Tabs.Strategies.Tabs
{
    using System;
    using Models;
    using ViewModels.Tabs;

    public sealed class GreenTabStrategy : ITabStrategy
    {
        private readonly Func<Tab, IGreenTabViewModel> _factory;
        public static readonly Tab Default = new Tab(Guid.Parse("{463E43D5-A19A-4F2A-A8E8-48EE79B18A1F}"), "Green");

        public Guid TypeId { get; }

        public string Name { get; }

        public GreenTabStrategy(Func<Tab, IGreenTabViewModel> factory)
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
            return new GreenTabViewModel(new Tab(TypeId, Name));
        }
    }
}