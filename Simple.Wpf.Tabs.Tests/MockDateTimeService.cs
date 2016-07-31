namespace Simple.Wpf.Tabs.Tests
{
    using System;
    using Microsoft.Reactive.Testing;
    using Tabs.Services;

    public sealed class MockDateTimeService : IDateTimeService
    {
        private readonly TestScheduler _testScheduler;

        public MockDateTimeService(TestScheduler testScheduler)
        {
            _testScheduler = testScheduler;
        }

        public DateTimeOffset Now => _testScheduler.Now;
    }
}