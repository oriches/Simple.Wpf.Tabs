namespace Simple.Wpf.Tabs.Tests.ViewModels
{
    using System;
    using Models;
    using Moq;
    using NUnit.Framework;
    using Strategies;
    using Tabs.Services;
    using Tabs.Strategies.Tabs;
    using Tabs.ViewModels;
    using Tabs.ViewModels.Tabs;

    [TestFixture]
    public sealed class MainViewModelFixtures : BaseViewModelFixtures
    {
        private Mock<IDiagnosticsViewModel> _diagnosticsViewModel;
        private Mock<ITabsService> _tabService;

        [SetUp]
        public void SetUp()
        {
            _diagnosticsViewModel = new Mock<IDiagnosticsViewModel>();

            _tabService = new Mock<ITabsService>();

        }

        [Test]
        public void tab_view_modles_populated_when_created()
        {
            // ARRANGE
            var tabViewModels = new ITabViewModel[]
                             {
                                 new FooTabViewModel(new Tab(Guid.Empty, "Foo")),
                                 new BarTabViewModel(new Tab(Guid.Empty, "Bar"))
                             };

            _tabService.Setup(x => x.GetTabs()).Returns(tabViewModels);

            // ACT
            var viewModel = new MainViewModel(_diagnosticsViewModel.Object, _tabService.Object);

            // ASSERT
            Assert.That(viewModel.Tabs, Is.EquivalentTo(tabViewModels));
        }
    }
}