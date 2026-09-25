using System.Collections.Generic;

namespace JTLStudio.SDK.Analytics
{
    public interface IAnalyticsReporter
    {
        void Send(string name);
        void Send(string name, Dictionary<string, string> data);
    }
}
