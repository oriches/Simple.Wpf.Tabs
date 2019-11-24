using System;
using System.ComponentModel;

namespace Simple.Wpf.Tabs.ViewModels
{
    public interface IViewModel : INotifyPropertyChanged, IDisposable
    {
        IDisposable SuspendNotifications();
    }
}