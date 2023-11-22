using System;
using Moq;
using NUnit.Framework;
using Simple.Wpf.Tabs.Models;
using Simple.Wpf.Tabs.Services;
using Simple.Wpf.Tabs.ViewModels;
using Simple.Wpf.Tabs.ViewModels.Tabs;

namespace Simple.Wpf.Tabs.Tests.ViewModels
{
    [TestFixture]
    public sealed class MainViewModelFixtures : BaseViewModelFixtures
    {
        [SetUp]
        public void SetUp()
        {
            _diagnosticsViewModel = new Mock<IDiagnosticsViewModel>();

            _tabService = new Mock<ITabsService>();
        }

        private Mock<IDiagnosticsViewModel> _diagnosticsViewModel;
        private Mock<ITabsService> _tabService;

        [Test]
        public void tab_view_modles_populated_when_created()
        {
            // ARRANGE
            var tabViewModels = new ITabViewModel[]
            {
                new FooTabViewModel(new Tab(Guid.Empty, "Foo")),
                new BarTabViewModel(new Tab(Guid.Empty, "Bar"))
            };

            _tabService.Setup(x => x.GetTabs())
                .Returns(tabViewModels);

            // ACT
            var viewModel = new MainViewModel(_diagnosticsViewModel.Object, _tabService.Object);

            // ASSERT
            Assert.That(viewModel.Tabs, Is.EquivalentTo(tabViewModels));
        }
    }
}