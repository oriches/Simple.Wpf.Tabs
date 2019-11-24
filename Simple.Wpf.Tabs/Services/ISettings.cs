using System.Collections.Generic;
using Simple.Wpf.Tabs.Models;

namespace Simple.Wpf.Tabs.Services
{
    public interface ISettings : IEnumerable<Setting>
    {
        object this[string name] { get; set; }
    }
}