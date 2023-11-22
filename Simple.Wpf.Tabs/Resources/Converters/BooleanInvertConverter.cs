﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace Simple.Wpf.Tabs.Resources.Converters
{
    public sealed class BooleanInvertConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value == null) return false;

                var boolValue = (bool)value;
                return !boolValue;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            Binding.DoNothing;
    }
}