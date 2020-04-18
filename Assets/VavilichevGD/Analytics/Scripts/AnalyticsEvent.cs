using System.Collections.Generic;

namespace VavilichevGD.Analytics {
    public class AnalyticsEvent {
        
        public string eventName { get; private set; }
        public Dictionary<string, object> properties { get; private set; }
        public bool allowToSend { get; protected set; }

        public bool isPropertiesEmpty => properties == null || properties.Count == 0;
        public bool onlyOneProperty => properties != null && properties.Count == 1;

        public AnalyticsEvent(string eventName) {
            this.eventName = eventName;
            this.allowToSend = true;
        }

        public AnalyticsEvent(string eventName, string propertyName, object value) {
            this.eventName = eventName;
            this.properties = new Dictionary<string, object>();
            this.properties.Add(propertyName, value);
            this.allowToSend = true;
        }

        public AnalyticsEvent(string eventName, Dictionary<string, object> properties) {
            this.eventName = eventName;
            this.properties = properties;
            this.allowToSend = true;
        }
    }
}