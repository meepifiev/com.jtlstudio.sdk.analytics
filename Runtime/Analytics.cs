using System;
using System.Collections.Generic;

namespace JTLStudio.SDK.Analytics
{
    public static class Analytics
    {
        private static IAnalyticsReporter _reporter;

        public static IAnalyticsReporter Reporter
        {
            get => _reporter ?? (_reporter = new YandexMetricaReporter());
            set => _reporter = value;
        }

        public static bool IsReady => YandexMetrica.IsInitialized;

        public static void Send(IAnalyticsEvent analyticsEvent)
        {
            if (analyticsEvent == null)
            {
                throw new ArgumentNullException(nameof(analyticsEvent));
            }

            analyticsEvent.Report(Reporter);
        }

        public static void Send(string name)
        {
            Reporter.Send(name);
        }

        public static void Send(string name, Dictionary<string, string> data)
        {
            Reporter.Send(name, data);
        }
    }
}
