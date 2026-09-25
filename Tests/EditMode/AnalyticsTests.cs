using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace JTLStudio.SDK.Analytics.Tests
{
    public class AnalyticsTests
    {
        private class Reporter : IAnalyticsReporter
        {
            public readonly List<string> Names = new List<string>();
            public Dictionary<string, string> Data;

            public void Send(string name)
            {
                Names.Add(name);
                Data = null;
            }

            public void Send(string name, Dictionary<string, string> data)
            {
                Names.Add(name);
                Data = data;
            }
        }

        private class LevelCompleted : IAnalyticsEvent
        {
            private readonly int _level;

            public LevelCompleted(int level)
            {
                _level = level;
            }

            public void Report(IAnalyticsReporter reporter)
            {
                reporter.Send("level_complete", new Dictionary<string, string>
                {
                    ["level"] = _level.ToString()
                });
            }
        }

        [TearDown]
        public void TearDown()
        {
            Analytics.Reporter = null;
        }

        [Test]
        public void EventReportsItselfToTheReporter()
        {
            Reporter reporter = new Reporter();
            Analytics.Reporter = reporter;

            Analytics.Send(new LevelCompleted(7));

            CollectionAssert.AreEqual(new[] { "level_complete" }, reporter.Names);
            Assert.AreEqual("7", reporter.Data["level"]);
        }

        [Test]
        public void GoalWithoutDataReachesTheReporter()
        {
            Reporter reporter = new Reporter();
            Analytics.Reporter = reporter;

            Analytics.Send("shop_opened");

            CollectionAssert.AreEqual(new[] { "shop_opened" }, reporter.Names);
            Assert.IsNull(reporter.Data);
        }

        [Test]
        public void EmptyEventIsRefused()
        {
            Assert.Throws<ArgumentNullException>(() => Analytics.Send((IAnalyticsEvent)null));
        }

        [Test]
        public void DataBecomesJson()
        {
            string json = AnalyticsJson.Write(new Dictionary<string, string>
            {
                ["level"] = "3",
                ["mode"] = "hard"
            });

            Assert.AreEqual("{\"level\":\"3\",\"mode\":\"hard\"}", json);
        }

        [Test]
        public void EmptyDataBecomesEmptyJson()
        {
            Assert.AreEqual("{}", AnalyticsJson.Write(null));
            Assert.AreEqual("{}", AnalyticsJson.Write(new Dictionary<string, string>()));
        }

        [Test]
        public void QuotesAndBreaksAreEscaped()
        {
            string json = AnalyticsJson.Write(new Dictionary<string, string>
            {
                ["text"] = "a\"b\\c\nd"
            });

            Assert.AreEqual("{\"text\":\"a\\\"b\\\\c\\nd\"}", json);
        }

        [Test]
        public void CounterWithoutValueLeavesMetricaAsleep()
        {
            YandexMetrica.Initialize("");

            Assert.IsFalse(YandexMetrica.IsInitialized);
            Assert.AreEqual("", YandexMetrica.Counter);
        }

        [Test]
        public void CounterIsTrimmed()
        {
            YandexMetrica.Initialize("  12345  ");

            Assert.IsTrue(YandexMetrica.IsInitialized);
            Assert.AreEqual("12345", YandexMetrica.Counter);

            YandexMetrica.Initialize("");
        }
    }
}
