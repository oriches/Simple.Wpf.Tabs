namespace Simple.Wpf.Tabs.Services
{
    using System.Collections.Generic;
    using Models;

    public interface ISettings : IEnumerable<Setting>
    {
        object this[string name] { get; set; }
    }
}