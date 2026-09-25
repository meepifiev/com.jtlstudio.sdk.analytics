namespace JTLStudio.SDK.Analytics
{
    public interface IAnalyticsEvent
    {
        void Report(IAnalyticsReporter reporter);
    }
}
