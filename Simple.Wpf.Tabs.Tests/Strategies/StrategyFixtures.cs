using System.Linq;
using Moq;
using NUnit.Framework;
using Simple.Wpf.Tabs.Extensions;
using Simple.Wpf.Tabs.Services;
using Simple.Wpf.Tabs.Strategies.Tabs;
using Simple.Wpf.Tabs.ViewModels.Tabs;

namespace Simple.Wpf.Tabs.Tests.Strategies
{
    [TestFixture]
    public sealed class StrategyFixtures
    {
        [SetUp]
        public void SetUp()
        {
            var gestureService = new Mock<IGestureService>();
            gestureService.Setup(x => x.SetBusy());

            ObservableExtensions.GestureService = gestureService.Object;

            // We test all strategies in one go...

            _strategies = new ITabStrategy[]
            {
                new BlueTabStrategy(x => new BlueTabViewModel(x)),
                new RedTabStrategy(x => new RedTabViewModel(x)),
                new GreenTabStrategy(x => new GreenTabViewModel(x))
            };
        }

        private ITabStrategy[] _strategies;

        [Test]
        public void strategies_can_handle_created_type_id()
        {
            // ARRANGE
            // ACT

            var result = _strategies.All(x =>
            {
                var vm1 = x.Create();
                ITabViewModel vm2;

                var canHandle = x.CanHandle(vm1.Tab, out vm2);

                return canHandle && vm1.Tab.Equals(vm2.Tab);
            });


            // ASSERT
            Assert.That(result, Is.True);
        }
    }
}