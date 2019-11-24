using System.Linq;
using System.Reactive.Subjects;
using Moq;
using NUnit.Framework;
using Simple.Wpf.Tabs.Models;
using Simple.Wpf.Tabs.Services;
using Simple.Wpf.Tabs.Strategies.Tabs;
using Simple.Wpf.Tabs.Tests.Strategies;
using Tab = Simple.Wpf.Tabs.Dtos.Tab;

namespace Simple.Wpf.Tabs.Tests.Services
{
    [TestFixture]
    public sealed class TabsServiceFixtures : BaseServiceFixtures
    {
        [SetUp]
        public void SetUp()
        {
            _settingsService = new Mock<ISettingsService>();

            _strategies = new ITabStrategy[] {new FooStrategy(), new BarStrategy()};
        }

        private Mock<ISettingsService> _settingsService;
        private ITabStrategy[] _strategies;

        [Test]
        public void creates_when_tabs_have_not_persisted_to_settings_previously()
        {
            // ARRANGE
            ISettings settings = new Settings(Enumerable.Empty<Setting>(), new Subject<bool>());
            _settingsService.Setup(x => x.TryGet("Tabs", out settings)).Returns(false);
            _settingsService.Setup(x => x.CreateOrUpdate("Tabs")).Returns(settings);

            var service = new TabsService(_strategies, _settingsService.Object);

            // ACT
            var tabs = service.GetTabs();

            // ASSERT
            CollectionAssert.AreEquivalent(tabs.Select(x => x.Tab), _strategies.Select(x => x.Create().Tab));
        }

        [Test]
        public void loads_when_tabs_have_been_persisted_to_settings_previously()
        {
            // ARRANGE
            var dtos = _strategies.Select(x =>
            {
                var viewModel = x.Create();
                var dtoTab = new Tab(viewModel.Tab.TypeId, viewModel.Tab.Name);

                return dtoTab;
            }).ToArray();

            var settings = new Mock<ISettings>();
            settings.Setup(x => x[It.IsAny<string>()]).Returns(dtos);

            var settingsInstance = settings.Object;
            _settingsService.Setup(x => x.TryGet("Tabs", out settingsInstance)).Returns(true);

            var service = new TabsService(_strategies, _settingsService.Object);

            // ACT
            var tabs = service.GetTabs();

            // ASSERT
            CollectionAssert.AreEquivalent(tabs.Select(x => x.Tab), _strategies.Select(x => x.Create().Tab));
        }
    }
}