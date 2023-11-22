using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Simple.Wpf.Tabs.Models;

namespace Simple.Wpf.Tabs.Extensions
{
    public static class MemoryExtensions
    {
        private static readonly IDictionary<MemoryUnits, string> UnitsAsString = new Dictionary<MemoryUnits, string>();

        private static readonly IDictionary<MemoryUnits, decimal> UnitsMulitpler =
            new Dictionary<MemoryUnits, decimal>();

        private static readonly Type MemoryUnitsType = typeof(MemoryUnits);

        public static string WorkingSetPrivateAsString(this Memory memory)
        {
            var valueAsString = decimal.Round(memory.WorkingSetPrivate * GetMultipler(MemoryUnits.Mega), 2)
                .ToString(CultureInfo.InvariantCulture);

            return valueAsString + " " + GetUnitString(MemoryUnits.Mega);
        }

        public static string ManagedAsString(this Memory memory)
        {
            var valueAsString = decimal.Round(memory.Managed * GetMultipler(MemoryUnits.Mega), 2)
                .ToString(CultureInfo.InvariantCulture);

            return valueAsString + " " + GetUnitString(MemoryUnits.Mega);
        }

        private static decimal GetMultipler(MemoryUnits units)
        {
            decimal unitsMulitpler;
            if (UnitsMulitpler.TryGetValue(units, out unitsMulitpler)) return unitsMulitpler;

            unitsMulitpler = 1 / Convert.ToDecimal((int)units);

            UnitsMulitpler.Add(units, unitsMulitpler);
            return unitsMulitpler;
        }

        private static string GetUnitString(MemoryUnits units)
        {
            string unitsString;
            if (UnitsAsString.TryGetValue(units, out unitsString)) return unitsString;

            string unitAsString;
            switch (units)
            {
                case MemoryUnits.Bytes:
                    unitAsString = "Bytes";
                    break;
                case MemoryUnits.Kilo:
                    unitAsString = "Kilo";
                    break;
                case MemoryUnits.Mega:
                    unitAsString = "Mega";
                    break;
                case MemoryUnits.Giga:
                    unitAsString = "Giga";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("units", @"Unknown units of memory!");
            }

            var memInfo = MemoryUnitsType.GetMember(unitAsString);
            var attributes = memInfo[0]
                .GetCustomAttributes(typeof(DescriptionAttribute), false);
            unitsString = ((DescriptionAttribute)attributes[0]).Description;

            UnitsAsString.Add(units, unitsString);
            return unitsString;
        }
    }
}