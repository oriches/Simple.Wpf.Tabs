namespace Simple.Wpf.Tabs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Constants
    {
        public static class StartsWith
        {
            public static class Unit
            {
                public static IEnumerable<System.Reactive.Unit> Default = new[] { System.Reactive.Unit.Default }
                    .ToArray();

                public static IEnumerable<object> DefaultBoxed = new[] { System.Reactive.Unit.Default }
                    .Cast<object>()
                    .ToArray();
            }

            public static class Boolean
            {
                public static IEnumerable<bool> False = new[] { false }
                    .ToArray();

                public static IEnumerable<bool> True = new[] { true }
                   .ToArray();
            }
        }

        // ReSharper disable once InconsistentNaming
        public static class UI
        {
            public const string ExceptionTitle = "whoops - something's gone wrong!";

            public static class Diagnostics
            {
                public const string DefaultCpuString = "CPU: 00 %";
                public const string DefaultManagedMemoryString = "Managed Memory: 00 Mb";
                public const string DefaultTotalMemoryString = "Total Memory: 00 Mb";

                public static readonly TimeSpan Heartbeat = TimeSpan.FromSeconds(5);
                public static readonly TimeSpan UiFreeze = TimeSpan.FromMilliseconds(500);
                public static readonly TimeSpan UiFreezeTimer = TimeSpan.FromMilliseconds(333);

                public static readonly TimeSpan DiagnosticsLogInterval = TimeSpan.FromSeconds(1);
                public static readonly TimeSpan DiagnosticsIdleBuffer = TimeSpan.FromMilliseconds(666);
                public static readonly TimeSpan DiagnosticsCpuBuffer = TimeSpan.FromMilliseconds(666);
                public static readonly TimeSpan DiagnosticsSubscriptionDelay = TimeSpan.FromMilliseconds(1000);
            }

            public static readonly TimeSpan MessageDelay = TimeSpan.FromMilliseconds(250);
        }
    }
}