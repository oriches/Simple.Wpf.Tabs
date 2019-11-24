using System.Collections.Generic;
using System.Linq;
using Simple.Wpf.Tabs.Dtos;
using Simple.Wpf.Tabs.Extensions;
using Simple.Wpf.Tabs.Strategies.Tabs;
using Simple.Wpf.Tabs.ViewModels.Tabs;

namespace Simple.Wpf.Tabs.Services
{
    public sealed class TabsService : ITabsService
    {
        private readonly ISettingsService _settingsService;
        private readonly IEnumerable<ITabStrategy> _tabStrategies;

        public TabsService(IEnumerable<ITabStrategy> tabStrategies, ISettingsService settingsService)
        {
            _tabStrategies = tabStrategies;
            _settingsService = settingsService;
        }

        public ITabViewModel[] GetTabs()
        {
            ISettings settings;

            return _settingsService.TryGet("Tabs", out settings)
                ? LoadFromSettings(settings)
                : CreateAndSaveToSettings();
        }

        private ITabViewModel[] CreateAndSaveToSettings()
        {
            var settings = _settingsService.CreateOrUpdate("Tabs");

            var viewModels = _tabStrategies.OrderBy(x => x.Name)
                .Select(x => x.Create())
                .ToArray();

            var dtoTabs = viewModels.Select(x => new Tab(x.Tab.TypeId, x.Tab.Name))
                .ToArray();

            settings["Instances"] = dtoTabs;

            return viewModels;
        }

        private ITabViewModel[] LoadFromSettings(ISettings settings)
        {
            var dtos = settings.Get<Tab[]>("Instances");

            return dtos.Select(x => new Models.Tab(x.TypeId, x.Name))
                .SelectMany(x => _tabStrategies.Select(y =>
                {
                    ITabViewModel viewModel;
                    y.CanHandle(x, out viewModel);

                    return viewModel;
                }))
                .Where(x => x != null)
                .ToArray();
        }
    }
}