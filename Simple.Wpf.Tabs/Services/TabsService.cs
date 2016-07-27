namespace Simple.Wpf.Tabs.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;
    using Strategies.Tabs;
    using ViewModels.Tabs;

    public sealed class TabsService : ITabsService
    {
        private readonly IEnumerable<ITabStrategy> _tabStrategies;
        private readonly ISettingsService _settingsService;

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

            var dtoTabs = viewModels.Select(x => new Dtos.Tab(x.Tab.TypeId, x.Tab.Name))
                .ToArray();

            settings["Instances"] = dtoTabs;

            return viewModels;
        }

        private ITabViewModel[] LoadFromSettings(ISettings settings)
        {
            var dtos = settings.Get<Dtos.Tab[]>("Instances");

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
