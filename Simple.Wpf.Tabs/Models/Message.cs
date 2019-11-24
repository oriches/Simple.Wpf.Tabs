using Simple.Wpf.Tabs.ViewModels;

namespace Simple.Wpf.Tabs.Models
{
    public sealed class Message
    {
        public Message(string header, ICloseableViewModel viewModel)
        {
            Header = header;
            ViewModel = viewModel;
        }

        public string Header { get; }

        public ICloseableViewModel ViewModel { get; }
    }
}