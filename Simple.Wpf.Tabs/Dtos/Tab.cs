namespace Simple.Wpf.Tabs.Dtos
{
    using System;

    public sealed class Tab
    {
        public string Name { get; set; }

        public Guid TypeId { get; set; }
        
        public Tab()
        {
        }

        public Tab(Guid typeId, string name)
        {
            TypeId = typeId;
        
            Name = name;
        }
    }
}