using Simple.Wpf.Tabs.Models;

namespace Simple.Wpf.Tabs.ViewModels.Tabs
{
    public interface IRedTabViewModel : ITabViewModel
    {
    }

    public sealed class RedTabViewModel : TabBaseViewModel, IRedTabViewModel
    {
        public RedTabViewModel(Tab definition) : base(definition)
        {
        }
    }
}