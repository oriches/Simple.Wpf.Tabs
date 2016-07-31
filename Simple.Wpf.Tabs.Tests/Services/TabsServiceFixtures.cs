namespace Simple.Wpf.Tabs.Tests.Services
{
    using System.Linq;
    using System.Reactive.Subjects;
    using Models;
    using Moq;
    using NUnit.Framework;
    using Strategies;
    using Tabs.Services;
    using Tabs.Strategies.Tabs;

    [TestFixture]
    public sealed class TabsServiceFixtures : BaseServiceFixtures
    {
        private Mock<ISettingsService> _settingsService;
        private ITabStrategy[] _strategies;

        [SetUp]
        public void SetUp()
        {
            _settingsService = new Mock<ISettingsService>();

            _strategies = new ITabStrategy[] {new FooStrategy(), new BarStrategy()};
        }

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
                var dtoTab = new Dtos.Tab(viewModel.Tab.TypeId, viewModel.Tab.Name);

                return dtoTab;
            }).ToArray();

            var settings = new Mock<ISettings>();
            settings.Setup(x => x[It.IsAny<string>()]).Returns(dtos);

            ISettings settingsInstance = settings.Object;
            _settingsService.Setup(x => x.TryGet("Tabs", out settingsInstance)).Returns(true);

            var service = new TabsService(_strategies, _settingsService.Object);

            // ACT
            var tabs = service.GetTabs();

            // ASSERT
            CollectionAssert.AreEquivalent(tabs.Select(x => x.Tab), _strategies.Select(x => x.Create().Tab));
        }
    }
}