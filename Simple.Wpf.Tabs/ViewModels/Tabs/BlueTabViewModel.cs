using Simple.Wpf.Tabs.Models;

namespace Simple.Wpf.Tabs.ViewModels.Tabs
{
    public interface IBlueTabViewModel : ITabViewModel
    {
    }

    public sealed class BlueTabViewModel : TabBaseViewModel, IBlueTabViewModel
    {
        public BlueTabViewModel(Tab definition) : base(definition)
        {
        }
    }
}