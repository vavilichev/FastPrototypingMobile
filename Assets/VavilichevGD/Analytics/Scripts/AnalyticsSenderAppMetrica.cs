using System.Collections.Generic;
using VavilichevGD.Tools;

namespace VavilichevGD.Analytics {
    public class AnalyticsSenderAppMetrica : AnalyticsSender {
        public override void Initialize() {
            // AppMetrica has own prefab.
        }

        protected override void LogEvent(string eventName) {
            Logging.Log($"AnalyticsSender AppMetrica: log event ({eventName})");
            //AppMetrica.Instance.ReportEvent(EVENT_KEY);
        }

        protected override void LogEvent(string eventName, string propertyName, object value) {
            Logging.Log($"AnalyticsSender AppMetrica: log event ({eventName}, {propertyName}, {value})");
            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties[propertyName] = value;
            LogEvent(eventName, properties);
        }

        protected override void LogEvent(string eventName, Dictionary<string, object> properties) {
            Logging.Log($"AnalyticsSender AppMetrica: log event ({eventName}, dictionary lentgh: {properties.Count})");
            //AppMetrica.Instance.ReportEvent(eventName, properties);
        }
    }
}