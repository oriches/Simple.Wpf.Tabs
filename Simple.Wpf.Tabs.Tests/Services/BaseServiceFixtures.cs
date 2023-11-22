using Moq;
using Simple.Wpf.Tabs.Extensions;
using Simple.Wpf.Tabs.Services;

namespace Simple.Wpf.Tabs.Tests.Services
{
    public abstract class BaseServiceFixtures
    {
        protected BaseServiceFixtures()
        {
            GestureService = new Mock<IGestureService>();
            GestureService.Setup(x => x.SetBusy())
                .Verifiable();

            ObservableExtensions.GestureService = GestureService.Object;
        }

        public Mock<IGestureService> GestureService { get; }
    }
}