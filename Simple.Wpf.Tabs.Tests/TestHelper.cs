using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Simple.Wpf.Tabs.Tests
{
    public static class TestHelper
    {
        public static IEnumerable<PropertyInfo> PropertiesImplementingInterface<T>(object instance) =>
            instance.GetType()
                .GetProperties()
                .Where(x => x.PropertyType == typeof(T) || x.PropertyType.GetInterfaces()
                    .Any(y => y == typeof(T)));
    }
}