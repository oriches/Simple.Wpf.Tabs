using System.Reactive.Concurrency;

namespace Simple.Wpf.Tabs.Services
{
    public interface ISchedulerService : IService
    {
        IScheduler Dispatcher { get; }

        IScheduler Current { get; }

        IScheduler TaskPool { get; }

        IScheduler EventLoop { get; }

        IScheduler NewThread { get; }

        IScheduler StaThread { get; }
    }
}