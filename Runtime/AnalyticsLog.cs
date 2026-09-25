using UnityEngine;

namespace JTLStudio.SDK.Analytics
{
    public static class AnalyticsLog
    {
        private const string Prefix = "[JTL SDK Analytics] ";

        private static bool _counterReported;

        public static void Write(string message)
        {
            if (JTLSDK.LogLevel == LogLevel.All)
            {
                Debug.Log(Prefix + message);
            }
        }

        public static void Missing(string name)
        {
            if (_counterReported || JTLSDK.LogLevel == LogLevel.None)
            {
                return;
            }

            _counterReported = true;
            Debug.LogWarning(Prefix + "goal " + name + " is not sent: the active configuration has no Yandex Metrica counter. Fill it in the toolkit, section Configurations.");
        }
    }
}
