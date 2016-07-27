namespace Simple.Wpf.Tabs.Models
{
    using System;

    public sealed class Tab
    {
        public Guid TypeId { get; private set; }

        public string Name { get; private set; }

        public Tab(Guid typeId, string name)
        {
            TypeId = typeId;

            Name = name;
        }
    }
}