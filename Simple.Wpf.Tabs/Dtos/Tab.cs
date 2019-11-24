using System;

namespace Simple.Wpf.Tabs.Dtos
{
    public sealed class Tab
    {
        public Tab()
        {
        }

        public Tab(Guid typeId, string name)
        {
            TypeId = typeId;

            Name = name;
        }

        public string Name { get; set; }

        public Guid TypeId { get; set; }
    }
}