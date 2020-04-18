using System.Collections.Generic;
using UnityEngine;
using VavilichevGD.Tools;

namespace VavilichevGD.Analytics {
    public class AnalyticsSenderFirebase : AnalyticsSender {
        
        public override void Initialize() {
            Debug.Log("AnalyticsSender Firebase: Initialize");
        }

        protected override void LogEvent(string eventName) {
            Debug.Log($"AnalyticsSender Firebase: log event ({eventName})");
//            FirebaseAnalytics.LogEvent(eventName);
        }

        protected override void LogEvent(string eventName, string propertyName, object value) {
            Logging.Log($"AnalyticsSender Firebase: log event ({eventName}, {propertyName}, {value})");
//            Parameter parameter = new Parameter(propertyName, value);
//            FirebaseAnalytics.LogEvent(eventName, parameter);
        }

        protected override void LogEvent(string eventName, Dictionary<string, object> properties) {
            Logging.Log($"AnalyticsSender Firebase: log event ({eventName}, dictionary lentgh: {properties.Count})");
//            Parameter[] parameters = new Parameter[properties.Count];
//            int index = 0;
//            foreach (KeyValuePair<string,object> keyValuePair in properties) {
//                parameters[index] = new Parameter(keyValuePair.Key, keyValuePair.Value);
//                index++;
//            }
//            FirebaseAnalytics.LogEvent(eventName, parameters);
        }
    }
}