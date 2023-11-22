using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Autofac;
using Autofac.Core;
using NLog;
using Simple.Wpf.Tabs.Extensions;
using Simple.Wpf.Tabs.Helpers;
using Simple.Wpf.Tabs.Models;
using Simple.Wpf.Tabs.Resources.Views;
using Simple.Wpf.Tabs.Services;
using Simple.Wpf.Tabs.ViewModels;
using Duration = Simple.Wpf.Tabs.Services.Duration;
using ObservableExtensions = Simple.Wpf.Tabs.Extensions.ObservableExtensions;

namespace Simple.Wpf.Tabs
{
    public partial class App
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly CompositeDisposable _disposable;

        public App()
        {
#if DEBUG
            LogHelper.ReconfigureLoggerToLevel(LogLevel.Debug);
#endif
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            Current.DispatcherUnhandledException += DispatcherOnUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;

            _disposable = new CompositeDisposable();
        }

        protected override void OnStartup(StartupEventArgs args)
        {
            using (Duration.Measure(Logger, "OnStartup - " + GetType()
                       .Name))
            {
                Logger.Info("Starting");

                // ReSharper disable once RedundantToStringCallForValueType
                var dispatcherMessage =
                    $"Dispatcher managed thread identifier = {Thread.CurrentThread.ManagedThreadId.ToString()}";

                Logger.Info(dispatcherMessage);
                Debug.WriteLine(dispatcherMessage);

                GetRenderCapabilityAsString();
                Logger.Info("WPF rendering capability (tier) = {0}", GetRenderCapabilityAsString());
                RenderCapability.TierChanged += (s, a) =>
                    Logger.Info("WPF rendering capability (tier) = {0}", GetRenderCapabilityAsString());

                base.OnStartup(args);

                BootStrapper.Start();

                var schedulerService = BootStrapper.Resolve<ISchedulerService>();
                var messageService = BootStrapper.Resolve<IMessageService>();
                var gestureService = BootStrapper.Resolve<IGestureService>();

                ObservableExtensions.GestureService = gestureService;

                // Load the application settings asynchronously
                LoadSettingsAsync(schedulerService)
                    .Wait();

                var window = new MainWindow(messageService, schedulerService);

                // The window has to be created before the root visual - all to do with the idling service initialising correctly...
                window.DataContext = BootStrapper.RootVisual;

                window.Closed += HandleClosed;
                Current.Exit += HandleExit;

                // Let's go...
                window.Show();


                if (Logger.IsInfoEnabled)
                    // Monitoring heartbeat only when info level is enabled...
                    ObserveHeartbeat(schedulerService)
                        .DisposeWith(_disposable);

#if DEBUG
                ObserveUiFreeze()
                    .DisposeWith(_disposable);
#endif
                ObserveCultureChanges()
                    .DisposeWith(_disposable);

                Logger.Info("Started");
            }
        }

        private static string GetRenderCapabilityAsString() => (RenderCapability.Tier / 0x10000).ToString();

        private static void HandleExit(object sender, ExitEventArgs e)
        {
            Logger.Info("Bye Bye!");
            LogManager.Flush();
        }

        private void HandleClosed(object sender, EventArgs e)
        {
            _disposable.Dispose();
            BootStrapper.Stop();
        }

        private static IDisposable ObserveCultureChanges() =>
            CultureService.CultureChanged
                .Subscribe(x =>
                {
                    Current.Windows
                        .Cast<Window>()
                        .ForEach(y => y.InvalidateVisual());
                });

        private static IDisposable ObserveHeartbeat(ISchedulerService schedulerService)
        {
            var dianosticsService = BootStrapper.Resolve<IDiagnosticsService>();

            return BootStrapper.Resolve<IHeartbeatService>()
                .Listen
                .SelectMany(x => dianosticsService.Memory.Take(1), (x, y) => y)
                .SelectMany(x => dianosticsService.Cpu.Take(1), (x, y) => new Tuple<Memory, int>(x, y))
                .Select(x => $"Heartbeat (Memory={x.Item1.WorkingSetPrivateAsString()}, CPU={x.Item2.ToString()}%)")
                .ObserveOn(schedulerService.Dispatcher)
                .ResilentSubscribe(x =>
                {
                    Debug.WriteLine(x);
                    Logger.Info(x);
                }, schedulerService.Dispatcher);
        }


        private static IObservable<Unit> LoadSettingsAsync(ISchedulerService schedulerService) =>
            Observable.Create<Unit>(x =>
                {
                    BootStrapper.Resolve<ISettingsService>();

                    x.OnNext(Unit.Default);
                    x.OnCompleted();

                    return Disposable.Empty;
                })
                .SubscribeOn(schedulerService.TaskPool);

        private static IDisposable ObserveUiFreeze()
        {
            var timer = new DispatcherTimer(DispatcherPriority.Normal)
            {
                Interval = Constants.UI.Diagnostics.UiFreezeTimer
            };

            var previous = DateTime.Now;
            timer.Tick += (sender, args) =>
            {
                var current = DateTime.Now;
                var delta = current - previous;
                previous = current;

                if (delta > Constants.UI.Diagnostics.UiFreeze)
                {
                    var message =
                        $"UI Freeze = {delta.TotalMilliseconds.ToString(CultureInfo.InvariantCulture)} ms";
                    Debug.WriteLine(message);
                }
            };

            timer.Start();
            return Disposable.Create(() => timer.Stop());
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            Logger.Info("Unhandled app domain exception");
            HandleException(args.ExceptionObject as Exception);
        }

        private static void DispatcherOnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs args)
        {
            Logger.Info("Unhandled dispatcher thread exception");
            args.Handled = true;

            HandleException(args.Exception);
        }

        private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs args)
        {
            Logger.Info("Unhandled task exception");
            args.SetObserved();

            HandleException(args.Exception.GetBaseException());
        }

        private static void HandleException(Exception exception)
        {
            Logger.Error(exception);

            BootStrapper.Resolve<ISchedulerService>()
                .Dispatcher
                .Schedule(exception, (scheduler, state) =>
                {
                    var messageService = BootStrapper.Resolve<IMessageService>();

                    var parameters = new Parameter[]
                    {
                        new NamedParameter("exception", state)
                    };

                    var viewModel = BootStrapper.Resolve<IExceptionViewModel>(parameters);

                    Observable.Return(viewModel)
                        .SelectMany(x => x.Closed, (x, y) => x)
                        .Take(1)
                        .Subscribe(x => x.Dispose());

                    messageService.Post(Constants.UI.ExceptionTitle, viewModel);

                    return Disposable.Empty;
                });
        }
    }
}