using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Simple.Wpf.Tabs.Extensions;
using Simple.Wpf.Tabs.Models;

namespace Simple.Wpf.Tabs.Services
{
    public sealed class HeartbeatService : DisposableObject, IHeartbeatService
    {
        private readonly IConnectableObservable<Unit> _listen;

        public HeartbeatService(ISchedulerService schedulerService)
            : this(Constants.UI.Diagnostics.Heartbeat, schedulerService)
        {
        }

        public HeartbeatService(TimeSpan interval, ISchedulerService schedulerService)
        {
            using (Duration.Measure(Logger, "Constructor - " + GetType()
                       .Name))
            {
                _listen = Observable.Interval(interval, schedulerService.TaskPool)
                    .AsUnit()
                    .Publish();

                _listen.Connect()
                    .DisposeWith(this);
            }
        }

        public IObservable<Unit> Listen => _listen;
    }
}