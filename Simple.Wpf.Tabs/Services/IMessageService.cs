using System;
using Simple.Wpf.Tabs.Models;
using Simple.Wpf.Tabs.ViewModels;

namespace Simple.Wpf.Tabs.Services
{
    public interface IMessageService : IService
    {
        IObservable<Message> Show { get; }

        void Post(string header, ICloseableViewModel viewModel);
    }
}