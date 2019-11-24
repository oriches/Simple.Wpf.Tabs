using System;
using Microsoft.Reactive.Testing;
using NUnit.Framework;
using Simple.Wpf.Tabs.Services;

namespace Simple.Wpf.Tabs.Tests.Services
{
    [TestFixture]
    public sealed class SettingsServiceFixtures : BaseServiceFixtures
    {
        [SetUp]
        public void SetUp()
        {
            _testScheduler = new TestScheduler();
            _schedulerService = new MockSchedulerService(_testScheduler);
        }

        private TestScheduler _testScheduler;
        private MockSchedulerService _schedulerService;

        [Test]
        public void creates_empty_settings()
        {
            // ARRANGE
            var service = new SettingsService(_schedulerService);

            // ACT
            var settings = service.CreateOrUpdate("Settings.1");

            // ASSERT
            Assert.That(settings, Is.Empty);
        }

        [Test]
        public void returns_null_for_invalid_settings()
        {
            // ARRANGE
            var service = new SettingsService(_schedulerService);

            // ACT
            var settings1 = service.CreateOrUpdate("Settings.1");
            settings1["Test.1"] = "1";

            ISettings settings2;
            var result = service.TryGet("Settings.2", out settings2);

            _testScheduler.AdvanceBy(TimeSpan.FromSeconds(1));

            // ASSERT
            Assert.That(result, Is.False);
            Assert.That(settings2, Is.Null);
        }

        [Test]
        public void updates_settings()
        {
            // ARRANGE
            var service = new SettingsService(_schedulerService);

            // ACT
            var settings1 = service.CreateOrUpdate("Settings.1");
            settings1["Test.1"] = "1";

            ISettings settings2;
            var result = service.TryGet("Settings.1", out settings2);

            _testScheduler.AdvanceBy(TimeSpan.FromSeconds(1));

            // ASSERT
            Assert.That(result, Is.True);
            Assert.That(settings2, Is.Not.Empty);
        }
    }
}