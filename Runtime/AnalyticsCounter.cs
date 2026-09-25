using UnityEngine;

namespace JTLStudio.SDK.Analytics
{
    public static class AnalyticsCounter
    {
        public static string Read()
        {
            JTLSDKSettings settings = Resources.Load<JTLSDKSettings>(JTLSDKSettings.ResourcePath);
            SdkConfiguration configuration = settings == null ? null : settings.ActiveConfiguration;
            return configuration == null ? "" : configuration.YandexMetricaCounter;
        }
    }
}
