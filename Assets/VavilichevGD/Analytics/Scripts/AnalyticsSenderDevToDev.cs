using System.Collections.Generic;
using UnityEngine;
using VavilichevGD.Tools;

namespace VavilichevGD.Analytics {
    public class AnalyticsSenderDevToDev : AnalyticsSender {
        public override void Initialize() {
            Debug.Log("AnalyticsSender DevToDev: Initialize");
        }

        protected override void LogEvent(string eventName) {
            Debug.Log($"AnalyticsSender DevToDev: log event ({eventName})");
//            DevToDev.Analytics.CustomEvent(eventName);
        }

        protected override void LogEvent(string eventName, string propertyName, object value) {
            Logging.Log($"AnalyticsSender DevToDev: log event ({eventName}, {propertyName}, {value})");
//            DevToDev.CustomEventParams customEventParams = new DevToDev.CustomEventParams();
//            customEventParams.AddParam(propertyName, value);
//            DevToDev.Analytics.CustomEvent(eventName, customEventParams);
        }

        protected override void LogEvent(string eventName, Dictionary<string, object> properties) {
            Logging.Log($"AnalyticsSender DevToDev: log event ({eventName}, dictionary lentgh: {properties.Count})");
//            DevToDev.CustomEventParams customEventParams = new DevToDev.CustomEventParams();
//            foreach (KeyValuePair<string,object> keyValuePair in properties)
//                customEventParams.AddParam(keyValuePair.Key, keyValuePair.Value);
//            DevToDev.Analytics.CustomEvent(eventName, customEventParams);
        }
    }
}