using System;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;

namespace Simple.Wpf.Tabs.Extensions
{
    public static class SchedulerExtensions
    {
        public static IDisposable Schedule(this IScheduler scheduler, TimeSpan timeSpan, Action action) =>
            scheduler.Schedule(action, timeSpan, (s1, s2) =>
            {
                s2();
                return Disposable.Empty;
            });

        public static IDisposable Schedule(this IScheduler scheduler, Action action) =>
            scheduler.Schedule(action, (s1, s2) =>
            {
                s2();
                return Disposable.Empty;
            });
    }
}