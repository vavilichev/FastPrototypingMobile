using System;
using UnityEngine;
using VavilichevGD.Tools.GameTime;

namespace VavilichevGD.Meta.DefferedRewards {
    [Serializable]
    public class DefferedRewardState {
        public string id;
        public bool isReady;
        public bool isReceived;
        public DateTimeSerialized timeStartSerialized;

        private DateTime m_timeStart = DateTime.MinValue;

        public DateTime timeStart {
            get {
                if (this.m_timeStart == DateTime.MinValue)
                    this.m_timeStart = this.timeStartSerialized.GetDateTime();
                return m_timeStart;
            }
        }

        public DefferedRewardState(string id, DateTime timeStart) {
            this.id = id;
            this.isReceived = false;
            this.isReady = false;
            this.m_timeStart = timeStart;
            this.timeStartSerialized = new DateTimeSerialized(timeStart);
        }
        
        public string ToJson() {
            return JsonUtility.ToJson(this);
        }

        public override string ToString() {
            return this.ToJson();
        }
    }
}