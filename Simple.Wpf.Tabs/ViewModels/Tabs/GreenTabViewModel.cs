namespace Simple.Wpf.Tabs.ViewModels.Tabs
{
    using Models;

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