﻿using System.Collections.Generic;
using Simple.Wpf.Tabs.Models;

namespace Simple.Wpf.Tabs.Services
{
    public interface ISettingsService : IService
    {
        ISettings CreateOrUpdate(string name);

        ISettings CreateOrUpdate(string name, IEnumerable<Setting> values);

        bool TryGet(string name, out ISettings settings);

        void Persist();
    }
}