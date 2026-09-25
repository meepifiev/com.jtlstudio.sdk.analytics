using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace JTLStudio.SDK.Analytics
{
    public static class YandexMetrica
    {
        private static string _counter = "";

#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void JTLSDKMetricaInitialize(string counter);

        [DllImport("__Internal")]
        private static extern void JTLSDKMetricaReachGoal(string name, string dataJson);
#endif

        public static string Counter => _counter;

        public static bool IsInitialized => string.IsNullOrEmpty(_counter) == false;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void InitializeFromConfiguration()
        {
            Initialize(AnalyticsCounter.Read());
        }

        public static void Initialize(string counter)
        {
            _counter = string.IsNullOrWhiteSpace(counter) ? "" : counter.Trim();

            if (IsInitialized == false)
            {
                return;
            }

#if UNITY_WEBGL && !UNITY_EDITOR
            JTLSDKMetricaInitialize(_counter);
#else
            AnalyticsLog.Write("counter " + _counter + " is ready, goals are written to the console outside a web build.");
#endif
        }

        public static void ReachGoal(string name, Dictionary<string, string> data)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            if (IsInitialized == false)
            {
                AnalyticsLog.Missing(name);
                return;
            }

            string json = AnalyticsJson.Write(data);

#if UNITY_WEBGL && !UNITY_EDITOR
            JTLSDKMetricaReachGoal(name, json);
#else
            AnalyticsLog.Write("goal " + name + " " + json);
#endif
        }
    }
}
