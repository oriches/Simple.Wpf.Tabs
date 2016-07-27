namespace Simple.Wpf.Tabs.ViewModels
{
    public interface IMainViewModel : IViewModel
    {
        IDiagnosticsViewModel Diagnostics { get; }
    }
}