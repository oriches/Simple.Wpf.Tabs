using Microsoft.Reactive.Testing;
using Moq;
using NUnit.Framework;
using Simple.Wpf.Tabs.Extensions;
using Simple.Wpf.Tabs.Services;

namespace Simple.Wpf.Tabs.Tests.ViewModels
{
    public abstract class BaseViewModelFixtures
    {
        public MockSchedulerService SchedulerService { get; private set; }

        public TestScheduler TestScheduler { get; private set; }

        public Mock<IGestureService> GestureService { get; private set; }

        public MockDateTimeService DateTimeService { get; private set; }

        [OneTimeSetUp]
        public void BaseOneTimeSetup()
        {
            GestureService = new Mock<IGestureService>();
            GestureService.Setup(x => x.SetBusy())
                .Verifiable();

            ObservableExtensions.GestureService = GestureService.Object;
        }

        [SetUp]
        public void BaseSetup()
        {
            TestScheduler = new TestScheduler();

            SchedulerService = new MockSchedulerService(TestScheduler);

            DateTimeService = new MockDateTimeService(TestScheduler);
        }
    }
}