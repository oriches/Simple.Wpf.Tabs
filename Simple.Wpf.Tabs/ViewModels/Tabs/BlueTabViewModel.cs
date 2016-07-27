namespace Simple.Wpf.Tabs.ViewModels.Tabs
{
    using Models;

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
