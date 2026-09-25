using System.Collections.Generic;

namespace JTLStudio.SDK.Analytics
{
    public class YandexMetricaReporter : IAnalyticsReporter
    {
        public void Send(string name)
        {
            YandexMetrica.ReachGoal(name, null);
        }

        public void Send(string name, Dictionary<string, string> data)
        {
            YandexMetrica.ReachGoal(name, data);
        }
    }
}
