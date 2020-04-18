using System;
using System.Collections.Generic;
using System.Linq;

namespace VavilichevGD.Analytics {

    public abstract class AnalyticsSender {

        public abstract void Initialize();
        
        public void Send(AnalyticsEvent analyticsEvent) {
            var eventName = analyticsEvent.eventName;
            
            if (analyticsEvent.isPropertiesEmpty) {
                LogEvent(eventName);
            }
            else if (analyticsEvent.onlyOneProperty) {
                var propertyName = analyticsEvent.properties.Keys.First();
                var propertyValue = analyticsEvent.properties.Values.First();
                LogEvent(eventName, propertyName, propertyValue);
            }
            else {
                LogEvent(eventName, analyticsEvent.properties);
            }
        }

        protected virtual void LogEvent(string eventName) {
            throw new NotSupportedException();
        }
        
        protected virtual void LogEvent(string eventName, string propertyName, object value) {
            throw new NotSupportedException();
        }

        protected virtual void LogEvent(string eventName, Dictionary<string, object> properties) {
            throw new NotSupportedException();
        }
    }
}