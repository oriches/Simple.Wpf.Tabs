using Simple.Wpf.Tabs.Models;

namespace Simple.Wpf.Tabs.ViewModels.Tabs
{
    public interface IGreenTabViewModel : ITabViewModel
    {
    }

    public sealed class GreenTabViewModel : TabBaseViewModel, IGreenTabViewModel
    {
        public GreenTabViewModel(Tab definition) : base(definition)
        {
        }
    }
}