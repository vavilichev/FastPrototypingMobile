using System;
using UnityEngine;

namespace VavilichevGD.Meta {
    [Serializable]
    public class RewardState {
        public string rewardId;
        public bool isViewed;
        public bool isReceived;

        public RewardState(string rewardId) {
            this.rewardId = rewardId;
            this.isViewed = false;
            this.isReceived = false;
        }
        
        public virtual string ToJson() {
            return JsonUtility.ToJson(this);
        }
    }
}