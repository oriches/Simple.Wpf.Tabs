namespace Simple.Wpf.Tabs.ViewModels.Tabs
{
    using Models;

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